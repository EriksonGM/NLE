using NLE.Data;

namespace NLE.Application.Services;

public interface IService
{
    int Save();
    Task<int> SaveAsync(CancellationToken cancellation = default);
}
public class Service : IService
{
    public readonly DataContext Db;
    
    protected Service(DataContext dataContext)
    {
        Db = dataContext;
    }
    
    public int Save()
    {
        return Db.SaveChanges();
    }

    public Task<int> SaveAsync(CancellationToken cancellation = default)
    {
        return Db.SaveChangesAsync(cancellation);
    }
}