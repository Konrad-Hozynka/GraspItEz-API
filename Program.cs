using System.Reflection;
using GraspItEz;
using GraspItEz.Database;
using GraspItEz.Services;
using Microsoft.EntityFrameworkCore;
using NLog;
using NLog.Web;
using NLog.Config;
using NLog.Fluent;
using NLog.Extensions.Logging;
using GraspItEz.Middleware;

var builder = WebApplication.CreateBuilder(args);
    builder.Logging.ClearProviders();
    builder.Logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
    builder.Host.UseNLog();

    // Add services to the container.

    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    var conectionString = builder.Configuration.GetConnectionString("DbConnection");

    builder.Services.AddDbContext<GraspItEzContext>(options => options.UseSqlServer(conectionString));
    NLog.LogManager.Setup().LoadConfigurationFromAppSettings()
    .LogFactory.Configuration.Variables["DbConnection"] = conectionString;
    var logger = NLog.LogManager.GetCurrentClassLogger();



    builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
    builder.Services.AddScoped<IStudySetsService, StudySetsService>();
    builder.Services.AddScoped<IOperationService, OperationService>();
    builder.Services.AddScoped<IQueryService, QueryService>();
    builder.Services.AddScoped<Seeder>();
    builder.Services.AddScoped<ErrorHandlingMiddleware>();



    var app = builder.Build();

    app.UseMiddleware<ErrorHandlingMiddleware>();
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


