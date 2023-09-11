using NLE.Data;
using NLE.Shared.Contracts;

namespace NLE.Application.Services;

public interface IExporterService : IService
{
    Task ExportAccess(CancellationToken token = default);

    Task<AccessDTO> ParseLogLine(string line, CancellationToken token = default);
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

    public Task<AccessDTO> ParseLogLine(string line, CancellationToken token = default)
    {
        throw new NotImplementedException();
    }
}