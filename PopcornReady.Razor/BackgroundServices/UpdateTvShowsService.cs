using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PopcornReady.Core.ApiServices;
using PopcornReady.Core.Data;
using PopcornReady.Core.Extensions;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PopcornReady.Razor.BackgroundServices
{
    public class UpdateTvShowsService : BackgroundService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly ILogger<UpdateTvShowsService> _logger;

        public UpdateTvShowsService(IServiceScopeFactory serviceScopeFactory, ILogger<UpdateTvShowsService> logger)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            int minLastUpdateSpanInHours = 5;

            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInfoWithTime($"Updating Tv Shows information from the API");
                using var scope = _serviceScopeFactory.CreateScope();

                try
                {
                    var context = scope.ServiceProvider.GetRequiredService<DataContext>();
                    var tvShowsApiService = scope.ServiceProvider.GetRequiredService<ITvShowsApiService>();


                    var tvShows =  await context.TvShows.Where(x => x.LastUpdateDate < DateTime.UtcNow.AddHours(-minLastUpdateSpanInHours))
                        .ToListAsync(stoppingToken);

                    foreach (var tvShow in tvShows)
                    {
                        var tvShowFromApi = await tvShowsApiService.GetTvShowAsync(tvShow.ApiId.ToString());

                        tvShow.ImageUrl = tvShowFromApi.ImageUrl;
                        tvShow.LastUpdateDate = tvShowFromApi.LastUpdateDate;
                        tvShow.Name = tvShowFromApi.Name;
                        tvShow.Description = tvShowFromApi.Description;
                        tvShow.DescriptionUrl = tvShowFromApi.DescriptionUrl;
                        tvShow.EndDate = tvShowFromApi.EndDate;
                        tvShow.Status = tvShowFromApi.Status;
                        tvShow.NextEpisode = tvShowFromApi.NextEpisode;
                    }

                    await context.SaveChangesAsync(stoppingToken);

                    _logger.LogInfoWithTime($"Updated {tvShows.Count} Tv Shows information");
                    await Task.Delay(TimeSpan.FromHours(8), stoppingToken);
                }
                catch (OperationCanceledException)
                {
                    return;
                }
            }
        }
    }
}