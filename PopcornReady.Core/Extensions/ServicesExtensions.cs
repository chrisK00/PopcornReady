using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PopcornReady.Core.ApiServices;
using PopcornReady.Core.Data;
using PopcornReady.Core.Options;
using PopcornReady.Core.Services;
using System;

namespace PopcornReady.Core.Extensions
{
    public static class ServicesExtensions
    {
        public static void AddAppServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<DataContext>(opt =>
            {
                opt.UseSqlite(config.GetConnectionString("Default"));
                opt.EnableSensitiveDataLogging();
            });

            services.AddScoped<ITvShowsApiService, EpisodateApiService>();
            services.AddScoped<ITvShowsService, TvShowsService>();

            services.AddHttpClients(config);
        }

        public static void AddHttpClients(this IServiceCollection services, IConfiguration config)
        {
            var options = config.GetSection(ApiOptions.SectionName).Get<ApiOptions>();

            services.AddHttpClient(ApiOptions.EpisodateClientName, config =>
            {
                config.BaseAddress = new Uri(options.Episodate);
            });
        }
    }
}
