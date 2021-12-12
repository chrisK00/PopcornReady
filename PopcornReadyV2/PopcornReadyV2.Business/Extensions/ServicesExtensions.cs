using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PopcornReady.Core.ApiServices;
using PopcornReady.Core.Services;
using PopcornReadyV2.Business.Data;
using PopcornReadyV2.Business.Data.Entities;
using PopcornReadyV2.Business.Options;
using System;
using PopcornReadyV2.Business.Helpers;

namespace PopcornReadyV2.Business.Extensions
{
    public static class ServicesExtensions
    {
        public static void AddAppServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddScoped<ITvShowsApiService, EpisodateApiService>();
            services.AddScoped<ITvShowsService, TvShowsService>();

            services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);

            services.AddHttpClients(config);
            services.AddDatabase(config);
        }

        private static void AddDatabase(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<AppDbContext>(options =>
              options.UseSqlServer(
                  config.GetConnectionString("DefaultConnection")));

            // enables Identity UI
            services.AddDefaultIdentity<AppUser>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 2;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
            }).AddEntityFrameworkStores<AppDbContext>();
        }

        private static void AddHttpClients(this IServiceCollection services, IConfiguration config)
        {
            var options = config.GetSection(ApiOptions.SectionName).Get<ApiOptions>();

            services.AddHttpClient(ApiOptions.EpisodateClientName, config =>
            {
                config.BaseAddress = new Uri(options.Episodate);
            });
        }
    }
}
