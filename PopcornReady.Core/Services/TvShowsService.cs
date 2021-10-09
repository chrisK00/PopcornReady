using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PopcornReady.Core.ApiServices;
using PopcornReady.Core.Data;
using PopcornReady.Core.Data.Entities;

namespace PopcornReady.Core.Services
{
    public class TvShowsService : ITvShowsService
    {
        private readonly DataContext _context;
        private readonly ITvShowsApiService _tvShowsApiService;
        private readonly ILogger<TvShowsService> _logger;

        public TvShowsService(DataContext context, ITvShowsApiService tvShowsApiService, ILogger<TvShowsService> logger)
        {
            _context = context;
            _tvShowsApiService = tvShowsApiService;
            _logger = logger;
        }

        public async Task AddAsync(TvShow tvShow, int userId)
        {
            var tvShowFromDb = await _context.TvShows.FirstOrDefaultAsync(x => x.ApiId == tvShow.ApiId);
            UserTvShow userTvShow = null;

            if (tvShowFromDb == null)
            {
                _logger.LogInformation($"Adding a new Tv Show named: {tvShow.Name} to the database");
                await _context.AddAsync(tvShow);
                await _context.SaveChangesAsync();
                userTvShow = new UserTvShow { UserId = userId, TvShowId = tvShow.Id };
            }
            else
            {
                if (await _context.UserTvShows.AnyAsync(x => x.TvShowId == tvShowFromDb.Id && x.UserId == userId))
                {
                    _logger.LogInformation($"A user tried to track an already tracked Tv Show called: {tvShow.Name}");
                    return;
                }

                userTvShow = new UserTvShow { UserId = userId, TvShowId = tvShowFromDb.Id };
            }

            await _context.AddAsync(userTvShow);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<TvShow>> GetAllAsync(int userId)
        {
            return await _context.UserTvShows.AsNoTracking().Include(x => x.TvShow.NextEpisode)
                .Where(x => x.UserId == userId)
                .Select(x => x.TvShow).ToListAsync();
        }

        public async Task<TvShow> FindAsync(string name)
        {
            var tvShow = await _context.TvShows.AsNoTracking()
                .Include(x => x.NextEpisode)
                .FirstOrDefaultAsync(x => x.Name.ToLower() == name.ToLower());

            if (tvShow == null)
            {
                _logger.LogInformation($"The Tv Show: {name} does not exist in the Db, sending a request to the API");
                tvShow = await _tvShowsApiService.GetTvShowAsync(name);
            }

            return tvShow;
        }

        public async Task RemoveAsync(int tvShowId, int userId)
        {
            var userTvShow = await _context.UserTvShows.FirstOrDefaultAsync(x => x.TvShowId == tvShowId && x.UserId == userId);

            _ = userTvShow ?? throw new KeyNotFoundException($"Tv Show with the id {tvShowId} was not found");

            _context.UserTvShows.Remove(userTvShow);
            await _context.SaveChangesAsync();
        }
    }
}
