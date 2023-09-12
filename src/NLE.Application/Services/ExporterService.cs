using System.Text.RegularExpressions;
using NLE.Data;
using NLE.Shared.Contracts;

namespace NLE.Application.Services;

public interface IExporterService : IService
{
    Task ExportAccess(CancellationToken token = default);

    AccessDTO ParseLogLine(string line);
}
public class ExporterService : Service, IExporterService
{
    public ExporterService(DataContext dataContext) : base(dataContext)
    {
    }

    public Task ExportAccess(CancellationToken token = default)
    {
        throw new NotImplementedException();
    }

    public AccessDTO ParseLogLine(string line)
    {
        var access = new AccessDTO();
        
        var data = line.Split(" ");

        access.EventDate = Convert.ToDateTime($"{data[0][1..11]} {data[0][12..20]}");
        access.StatusCode = Convert.ToInt32(data[3]);
        access.HttpMethod = data[6];
        access.Length = Convert.ToInt64(data[13][..^1]);
        access.RemoteAddress = data[11][..^1];
        access.SentTo = data[17][..^1];
        access.Referer = data[^1].Replace("\"","");
        access.Host = data[8];
        access.Agent = string.Join(string.Empty, data[18..^1]);

        return access;
    }
}