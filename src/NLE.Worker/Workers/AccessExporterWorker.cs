using NLE.Data;
using NLE.Shared.Contracts;

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
        
        using (var scope = _serviceProvider.CreateScope())
        {
            db = scope.ServiceProvider.GetRequiredService<DataContext>();
        }

        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            
            var path = Path.Combine(Environment.CurrentDirectory, "Data");

            var access = new List<AccessDTO>();
            
            foreach (var file in Directory.GetFiles(path, "*_access.log"))
            {
                var lines = await File.ReadAllLinesAsync(path, stoppingToken);

                foreach (var line in lines)
                {
                    
                }
            }
            
        }
    }

    private AccessDTO ParseLogLine(string line)
    {
        var res = new AccessDTO();

        return res;
    } 
    
}