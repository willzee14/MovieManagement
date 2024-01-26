using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using MovieManagement.Application.Abstraction;
using MovieManagement.Application.Dtos;
using MovieManagement.Application.Implementation;
using MovieManagement.Domain.ApplicationDbConteext;
using MovieManagement.Infrastructure.Abstraction;
using MovieManagement.Infrastructure.Implementation;
using Serilog;
using System.Text.Json;

namespace MovieManagement.Presentation.Extensions
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, ConfigurationManager configuration)
        {
            // service registrations
            services.AddScoped<IMovieService, MovieService>();
            services.AddScoped<IMovieRepository, MovieRepository>();            
            

            var appSettingsSection = configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            var respSettings = configuration.GetSection("ServiceResponseSettings");
            services.Configure<ServiceResponseSettings>(respSettings);


            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("dbConnection"));
            });
            services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            });

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();


            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                builder.WithOrigins("")
                .AllowAnyHeader()
                .WithMethods("POST", "GET", "PUT", "DELETE")
                .AllowCredentials()
                .Build());
            });
            var appSettings = appSettingsSection.Get<AppSettings>();

            Log.Logger = new LoggerConfiguration()
              .MinimumLevel.Debug()
              .MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Warning)
              .Enrich.FromLogContext()
              .WriteTo.File(appSettings.LogPath, rollingInterval: RollingInterval.Hour)
              .WriteTo.Console(restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Information)
              .CreateLogger();

            return services;
        }
    }
}
