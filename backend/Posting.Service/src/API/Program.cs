global using Microsoft.EntityFrameworkCore;
global using Application.Services.PostService;
using Application.Core;
using Persistence;
using FluentValidation.AspNetCore;
using Application.Validators;
using FluentValidation;
using System.Reflection;
using API.Middleware;
using Microsoft.Extensions.FileProviders;
using File.Package.FileService;
using Application.Services.LikesService;
using Application.Services.CommentService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();

Assembly postDtoAssembly = typeof(PostDTOValidator).Assembly;
builder.Services.AddValidatorsFromAssembly(postDtoAssembly);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IPostService, PostService>();
builder.Services.AddTransient<IFileService, FileService>();
builder.Services.AddScoped<ILikesService, LikesService>();
builder.Services.AddScoped<ICommentService, CommentService>();
builder.Services.AddHttpClient();

builder.Services.AddAutoMapper(typeof(MappingProfiles).Assembly);

builder.Services.AddDbContext<DataContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseMiddleware<ExceptionMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(options =>
{
    options
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader();
});

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
            Path.Combine(builder.Environment.ContentRootPath, "Uploads")),
    RequestPath = "/Resources"
});

app.UseAuthorization();

app.MapControllers();

using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;

try
{
    var context = services.GetRequiredService<DataContext>();
    await context.Database.MigrateAsync();
    await Seed.SeedData(context);
}
catch (Exception ex)
{
    var logger = services.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "An error occured during migration");
}

app.Run();
