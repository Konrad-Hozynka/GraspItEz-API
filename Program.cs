using System.Reflection;
using GraspItEz;
using GraspItEz.Database;
using GraspItEz.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<GraspItEzContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection")));
builder.Services.AddScoped<GraspItEzSeeder>();
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddScoped<IStudySetsService, StudySetsService>();
builder.Services.AddScoped<ILearnService, LearnService>();
builder.Services.AddScoped<ILearnLogicService, LearnLogicService>();


var app = builder.Build();

var scope = app.Services.CreateScope();
var seeder = scope.ServiceProvider.GetRequiredService<GraspItEzSeeder>();
seeder.Seed();

// Configure the HTTP request pipeline.


    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Grasp it ez");
    });


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
