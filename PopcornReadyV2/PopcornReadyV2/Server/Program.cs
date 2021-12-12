using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PopcornReady.Core.Services;
using PopcornReadyV2.Business.Data;
using PopcornReadyV2.Business.Data.Entities;
using System;
using System.Threading.Tasks;

namespace PopcornReadyV2.Server
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            using var scope = host.Services.CreateScope();
            var services = scope.ServiceProvider;

            try
            {
                var userManager = services.GetRequiredService<UserManager<AppUser>>();
                var context = services.GetRequiredService<AppDbContext>();
                var tvShowsService = services.GetRequiredService<ITvShowsService>();
                var mapper = services.GetRequiredService<IMapper>();
                await DataSeed.SeedAsync(userManager, context, tvShowsService, mapper);
            }
            catch (Exception ex)
            {
                var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
                logger.LogError(ex, "An error occured while seeding the database");
            }

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
