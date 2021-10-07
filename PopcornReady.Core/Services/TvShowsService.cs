using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PopcornReady.Core.ApiServices;
using PopcornReady.Core.Data;
using PopcornReady.Core.Data.Entities;

namespace PopcornReady.Core.Services
{
    public class TvShowsService : ITvShowsService
    {
        private readonly DataContext _context;
        private readonly ITvShowsApiService _tvShowsApiService;

        public TvShowsService(DataContext context, ITvShowsApiService tvShowsApiService)
        {
            _context = context;
            _tvShowsApiService = tvShowsApiService;
        }

        public async Task AddAsync(TvShow tvShow, int userId)
        {
            var tvShowFromDb = await _context.TvShows.FirstOrDefaultAsync(x => x.ApiId == tvShow.ApiId);
            UserTvShow userTvShow = null;

            if (tvShowFromDb == null)
            {
                await _context.AddAsync(tvShow);
                userTvShow = new UserTvShow { UserId = userId, TvShowId = tvShow.Id };
            }

            if (tvShowFromDb != null)
            {
                if (await _context.UserTvShows.AnyAsync(x => x.TvShowId == tvShowFromDb.Id && x.UserId == userId))
                {
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

            tvShow ??= await _tvShowsApiService.GetTvSeriesAsync(name);
            return tvShow;
        }
    }
}
