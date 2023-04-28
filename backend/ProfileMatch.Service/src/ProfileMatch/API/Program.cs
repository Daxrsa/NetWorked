using Application.Core.InterfaceRepos;
using Application.Services;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System.Windows.Input;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<IRecommendationsRepo, RecommendationService>();
builder.Services.AddScoped<IResultsRepo, ResultsService>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//var dbHost = Environment.GetEnvironmentVariable("DB_HOST");
//var dbName = Environment.GetEnvironmentVariable("DB-NAME");

//var connectionString = $"Server={dbHost};Database={dbName};Trusted_Connection=True;Encrypt=False";

builder.Services.AddDbContext<ProfileMatchDbContext>(option =>
    option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
