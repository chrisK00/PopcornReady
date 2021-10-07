using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PopcornReady.Core.ApiServices;
using PopcornReady.Core.Data;
using PopcornReady.Core.Services;
using System;

namespace PopcornReady.Core.Extensions
{
    public static class ServicesExtensions
    {
        public static void AddAppServices(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<DataContext>(opt => opt.UseSqlite(connectionString));

            services.AddScoped<ITvShowsApiService, EpisodateApiService>();
            services.AddScoped<ITvShowsService, TvShowsService>();

            services.AddHttpClients();
        }

        public static void AddHttpClients(this IServiceCollection services)
        {
            services.AddHttpClient("episodate", config =>
            {
                config.BaseAddress = new Uri("https://www.episodate.com/api/");
            });
        }
    }
}
