using Microsoft.EntityFrameworkCore;
using NLE.Domain.Entities;
using HttpMethod = NLE.Domain.Entities.HttpMethod;

namespace NLE.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
        //options
    }

    protected override void OnModelCreating(ModelBuilder mb)
    {
        mb.Entity<HttpMethod>().HasData(
            new HttpMethod { HttpMethodId = 1, Name = "HEAD" },
            new HttpMethod { HttpMethodId = 2, Name = "OPTIONS" },
            new HttpMethod { HttpMethodId = 3, Name = "GET" },
            new HttpMethod { HttpMethodId = 4, Name = "POST" },
            new HttpMethod { HttpMethodId = 5, Name = "PUT" },
            new HttpMethod { HttpMethodId = 6, Name = "DELETE" }
        );

        mb.Entity<Client>().HasData(
            new Client{ClientId = 1, Name = "Not defined"},
            new Client{ClientId = 2, Name = "Chrome"},
            new Client{ClientId = 3, Name = "Safari"},
            new Client{ClientId = 4, Name = "Edge"},
            new Client{ClientId = 5, Name = "Firefox"}
            );
    }

    public DbSet<Host> Hosts { get; set; }
    public DbSet<Access> Accesses { get; set; }
    public DbSet<Client> Clients { get; set; }
    public DbSet<HttpMethod> HttpMethods { get; set; }
    public DbSet<FileLog> FileLogs { get; set; }
}