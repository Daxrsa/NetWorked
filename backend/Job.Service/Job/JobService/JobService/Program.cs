using JobService.Clients;
using JobService.Core.AutoMapperConfig;
using JobService.Data;
using JobService.Services;
using JobService.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
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


            /*builder.Services.AddHttpClient<UserClient>(client => 
            {
                client.BaseAddress = new Uri("http://localhost:5116/");
            });*/
            builder.Services.AddHttpContextAccessor();

            builder.Services.AddHttpClient();
           /* builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.Authority = "http://localhost:5116/"; // URL of the token issuer
                options.Audience = "http://localhost:33364/"; // Audience of the token
            });*/

            builder.Services.AddAuthorization();
            
            //var algoliaConfig = new SearchConfig("ATO7HNMOJI", "8bc946dbb7800988993b963f146f6cdf");
            //var client = new SearchClient(algoliaConfig);

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
                        Path.Combine(builder.Environment.ContentRootPath, "Documents\\Images")),
                        RequestPath = "/Resources"
                        });
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseRouting();


            app.MapControllers();

            app.Run();
        }
    }
}