using System.Security.Cryptography.Xml;
using Microsoft.EntityFrameworkCore;
using NLE.Application.Services;
using NLE.Data;
using NLE.Domain.Entities;
using NLE.Shared.Contracts;
using Host = NLE.Domain.Entities.Host;

namespace NLE.Worker.Workers;

public class AccessExporterWorker : BackgroundService
{
    private readonly ILogger<AccessExporterWorker> _logger;
    private readonly IServiceProvider _serviceProvider;

    public AccessExporterWorker(ILogger<AccessExporterWorker> logger, IServiceProvider serviceProvider)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        DataContext db;
        IExporterService _exporter;

        using (var scope = _serviceProvider.CreateScope())
        {
            db = scope.ServiceProvider.GetRequiredService<DataContext>();
            _exporter = scope.ServiceProvider.GetRequiredService<IExporterService>();
        }

        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

            var path = Path.Combine(Environment.CurrentDirectory, "Data");

            var files = Directory.GetFiles(path, "*_access.log");
            
            _logger.LogInformation("{Files} files found, starting exportation", files.Length);
            
            foreach (var file in files)
            {
                _logger.LogInformation("Exporting {File}", file);
                
                var lines = await File.ReadAllLinesAsync(path, stoppingToken);

                var last = await db.FileLogs
                    .OrderByDescending(x => x.ReadedAt)
                    .FirstOrDefaultAsync(x => x.FileName == file, cancellationToken: stoppingToken);

                var access = new List<AccessDTO>();
                
                access.AddRange(last is null
                    ? lines.Select(line => _exporter.ParseLogLine(line))
                    : lines[last.LastLine..].Select(line => _exporter.ParseLogLine(line)));

                await db.AddAsync(new FileLog
                {
                    FileName = file,
                    LastLine = lines.Length,
                    ReadedAt = DateTime.Now,
                    FileLogId = Guid.NewGuid()
                }, stoppingToken);

                var hostId = Guid.NewGuid();

                var hostName = access.First().Host;
                
                if (!await db.Hosts.AnyAsync(x => x.Address == hostName, cancellationToken: stoppingToken))
                {
                    await db.AddAsync(new Host
                    {
                        HostId = hostId,
                        Name =hostName,
                        Address = hostName
                    }, stoppingToken);
                }
                else
                {
                    hostId = db.Hosts.FirstAsync(x => x.Address == hostName, stoppingToken).Result.HostId;
                }

                var accessFinal = access.Select(a => new Access
                    {
                        AccessId = Guid.NewGuid(),
                        Length = a.Length,
                        EventDate = a.EventDate,
                        StatusCode = a.StatusCode,
                        HttpMethodId = db.HttpMethods.FirstOrDefault(x => x.Name == a.HttpMethod)!.HttpMethodId,
                        Referer = a.Referer,
                        Url = a.Url,
                        HostId = hostId,
                        RemoteAddress = a.RemoteAddress,
                        SentTo = a.SentTo,
                        Agent = a.Agent
                    })
                    .ToList();

                await db.AddRangeAsync(accessFinal, stoppingToken);

                await db.SaveChangesAsync(stoppingToken);
                
                _logger.LogInformation("{File} exported successfully", file);
                
                await Task.Delay(10000, stoppingToken);
            }
        }
    }

    private AccessDTO ParseLogLine(string line)
    {
        var res = new AccessDTO();

        return res;
    }
}