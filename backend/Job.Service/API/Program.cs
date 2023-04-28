using Microsoft.EntityFrameworkCore;
using Persistence.Database;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//var dbHost = Environment.GetEnvironmentVariable("");
//var dbName = Environment.GetEnvironmentVariable("");

//var connectionString = $"Data Source={dbHost};Database={dbName};Trusted_Connection=True;TrustServerCertificate=True;";

builder.Services.AddDbContext<JobDbContext>(option =>
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
