using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PopcornReady.Razor.ApiServices;
using PopcornReady.Razor.Data;
using PopcornReady.Razor.Services;

namespace PopcornReady.Razor.Extensions
{
    public static class ServicesExtensions
    {
        public static void AddAppServices(this IServiceCollection services)
        {
            services.AddDbContext<DataContext>(opt => opt.UseInMemoryDatabase("PopcornReady"));

            services.AddScoped<ITvShowsApiService, EpisodateApiService>();
            services.AddScoped<ITvShowsService, TvShowsService>();
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
