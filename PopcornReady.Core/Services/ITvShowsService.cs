using System.Collections.Generic;
using System.Threading.Tasks;
using PopcornReady.Core.Data.Entities;

namespace PopcornReady.Core.Services
{
    public interface ITvShowsService
    {
        // TODO: Use Request models instead
        Task<IEnumerable<TvShow>> GetAllAsync(int userId);
        Task AddAsync(TvShow tvShow, int userId);
        Task<TvShow> FindAsync(string name);
    }
}
