using Microsoft.EntityFrameworkCore;
using NLE.Application.Services;
using NLE.Worker.Workers;
using NLE.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var conn = builder.Configuration.GetValue("CONN", builder.Configuration.GetConnectionString("Conn"));

if (string.IsNullOrEmpty(conn))
    throw new Exception("Invalid Database Connexion");

builder.Services.AddTransient<IExporterService, ExporterService>();

builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(conn);
#if DEBUG
    options.EnableSensitiveDataLogging();
#endif
});

builder.Services.AddHostedService<AccessExporterWorker>();
//builder.Services.AddHostedService<ErrorExporterWorker>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<DataContext>();

    db.Database.Migrate();
}

if (!Directory.Exists(Path.Combine(Environment.CurrentDirectory, "Data")))
    Directory.CreateDirectory(Path.Combine(Environment.CurrentDirectory, "Data"));

app.UseAuthorization();

app.MapControllers();

app.Run();