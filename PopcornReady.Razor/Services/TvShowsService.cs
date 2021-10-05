using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PopcornReady.Razor.Data;
using PopcornReady.Razor.Entities;

namespace PopcornReady.Razor.Services
{
    public class TvShowsService : ITvShowsService
    {
        private readonly DataContext _context;

        public TvShowsService(DataContext context)
        {
            _context = context;
        }

        public async Task AddAsync(TvShow tvShow)
        {
            await _context.AddAsync(tvShow);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<TvShow>> GetAllAsync()
        {
            return await _context.TvShows.Include(x => x.NextEpisode).ToListAsync();
        }
    }
}
