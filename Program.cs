using System.Reflection;
using GraspItEz;
using GraspItEz.Database;
using GraspItEz.Services;
using Microsoft.EntityFrameworkCore;
using NLog.Web;

var builder = WebApplication.CreateBuilder(args);
builder.Logging.ClearProviders();
builder.Logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
builder.Host.UseNLog();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<GraspItEzContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection")));

builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddScoped<IStudySetsService, StudySetsService>();
builder.Services.AddScoped<IOperationService, OperationService>();
builder.Services.AddScoped<Seeder>();



var app = builder.Build();

var scope = app.Services.CreateScope();
var seeder = scope.ServiceProvider.GetService<Seeder>();
seeder.Seed();


// Configure the HTTP request pipeline.


    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Grasp it easy");
    });


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
