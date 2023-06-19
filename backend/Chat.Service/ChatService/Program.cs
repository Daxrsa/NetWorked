using ChatService;
using ChatService.Hubs;
using dotenv.net;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MongoDB.Driver;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;

var builder = WebApplication.CreateBuilder(args);

// Load environment variables from .env file
DotEnv.Load();

builder.Services.AddSignalR();
builder.Services.AddSingleton<ConnectionFactory>();
builder.Services.AddSingleton<IMongoCollection<UserMessage>>(sp =>
{
    var mongoClient = sp.GetService<IMongoClient>();
    var mongoDatabase = mongoClient.GetDatabase("NetWorked");
    return mongoDatabase.GetCollection<UserMessage>("UserMessages");
});
builder.Services.AddSingleton<IMongoDatabase>(sp =>
{
    var mongoClient = sp.GetService<IMongoClient>();
    return mongoClient.GetDatabase("NetWorked");
});
builder.Services.AddHostedService<MessageConsumerService>();
builder.Services.AddControllers();
var mongoConnectionString = Environment.GetEnvironmentVariable("DATABASE");
builder.Services.AddSingleton<IMongoClient>(new MongoClient(mongoConnectionString));
builder.Services.AddSingleton<IDictionary<string, UserConnection>>(opts => new Dictionary<string, UserConnection>());

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.WithOrigins("http://127.0.0.1:5173", "https://localhost:7212")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});

var app = builder.Build();

app.UseRouting();

app.UseCors();

app.UseEndpoints(endpoints =>
{
    endpoints.MapHub<ChatHub>("/chat");
    endpoints.MapControllers();
});

app.Run();
