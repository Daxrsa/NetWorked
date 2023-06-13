using File.Package.FileService;
using JobService.Core.AutoMapperConfig;
using JobService.Data;
using JobService.Services;
using JobService.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Text.Json.Serialization;

namespace JobService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //DB Configuration
            builder.Services.AddDbContext<JobDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("local"));
            });

            //Automapper Configuration
            builder.Services.AddAutoMapper(typeof(MappingProfile));


            builder.Services.AddScoped<ICompany, CompanyService>();
            builder.Services.AddScoped<IJobPosition, JobPositionService>();
            builder.Services.AddScoped<IApplication, ApplicationService>();
            builder.Services.AddScoped<ISearch, SearchService>();
            builder.Services.AddTransient<IFileService, FileService>();

            builder.Services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                });
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            //builder.Services.AddSwaggerGen();

            builder.Services.AddSwaggerGen(c => {
                c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Description = """Standard Authorization header using the Bearer scheme. Example: "bearer {token}" """,
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });
                c.OperationFilter<SecurityRequirementsOperationFilter>();
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

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
                        Path.Combine(builder.Environment.ContentRootPath, "Uploads\\Images")),
                        RequestPath = "/Resources"
                        });

            app.UseRouting();


            app.MapControllers();

            app.Run();
        }
    }
}