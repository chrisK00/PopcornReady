using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PopcornReady.Core.Data;
using PopcornReady.Core.Extensions;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PopcornReady.Razor.BackgroundServices
{
    public class RemoveNotTrackedTvShowsService : BackgroundService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly ILogger<RemoveNotTrackedTvShowsService> _logger;

        public RemoveNotTrackedTvShowsService(IServiceScopeFactory serviceScopeFactory, ILogger<RemoveNotTrackedTvShowsService> logger)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInfoWithTime($"Removing not tracked Tv Shows");
                using var scope = _serviceScopeFactory.CreateScope();

                try
                {
                    var context = scope.ServiceProvider.GetRequiredService<DataContext>();

                    var notTrackedTvShows = context.TvShows
                        .Where(x => !context.UserTvShows.Any(y => y.TvShowId == x.Id))
                        .ToList();

                    context.TvShows.RemoveRange(notTrackedTvShows);
                    await context.SaveChangesAsync(stoppingToken);

                    _logger.LogInfoWithTime($"Removed {notTrackedTvShows.Count} not tracked Tv Shows");
                    await Task.Delay(TimeSpan.FromDays(3), stoppingToken);
                }
                catch (OperationCanceledException)
                {
                    return;
                }
            }
        }
    }
}