using System.Collections.Generic;
using System.Threading.Tasks;
using PopcornReady.Razor.Entities;

namespace PopcornReady.Razor.Services
{
    public interface ITvShowsService
    {
        Task<IEnumerable<TvShow>> GetAllAsync();
        Task AddAsync(TvShow tvShow);
    }
}
