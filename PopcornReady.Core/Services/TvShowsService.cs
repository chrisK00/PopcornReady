using System.Collections.Generic;
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

        public async Task AddAsync(TvShow tvShow)
        {
            if (await _context.TvShows.AnyAsync(x => x.ApiId == tvShow.ApiId))
            {
                return;
            }

            await _context.AddAsync(tvShow);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<TvShow>> GetAllAsync()
        {
            return await _context.TvShows.AsNoTracking()
                .Include(x => x.NextEpisode).ToListAsync();
        }

        public async Task<TvShow> FindAsync(string name)
        {
            var tvShow = await _context.TvShows.AsNoTracking()
                .Include(x => x.NextEpisode)
                .FirstOrDefaultAsync(x => string.Equals(x.Name, name, System.StringComparison.OrdinalIgnoreCase));

            tvShow ??= await _tvShowsApiService.GetTvSeriesAsync(name);
            return tvShow;
        }
    }
}
