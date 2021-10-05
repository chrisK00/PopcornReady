using System.Collections.Generic;
using System.Threading.Tasks;
using PopcornReady.Core.Data.Entities;

namespace PopcornReady.Core.Services
{
    public interface ITvShowsService
    {
        Task<IEnumerable<TvShow>> GetAllAsync();
        Task AddAsync(TvShow tvShow);
        Task<TvShow> FindAsync(string name);
    }
}
