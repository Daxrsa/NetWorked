using Algolia.Search;
using Algolia.Search.Clients;
using JobService.Clients;
using JobService.Core.AutoMapperConfig;
using JobService.Data;
using JobService.Services;
using JobService.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
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


            builder.Services.AddHttpClient<UserClient>(client => 
            {
                client.BaseAddress = new Uri("http://localhost:5116/");
            });

            
            //var algoliaConfig = new SearchConfig("ATO7HNMOJI", "8bc946dbb7800988993b963f146f6cdf");
            //var client = new SearchClient(algoliaConfig);

            builder.Services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                });
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

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
                        Path.Combine(builder.Environment.ContentRootPath, "Documents\\Images")),
                        RequestPath = "/Resources"
                        });
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}