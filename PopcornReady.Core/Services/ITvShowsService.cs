using System.Collections.Generic;
using System.Threading.Tasks;
using PopcornReady.Core.Data.Entities;
using PopcornReady.Core.Params;

namespace PopcornReady.Core.Services
{
    public interface ITvShowsService
    {
        // TODO: Use Request models
        Task<IEnumerable<TvShow>> GetAllAsync(TvShowParams param);
        Task AddAsync(TvShow tvShow, int userId);
        Task<TvShow> FindAsync(string name);
        Task RemoveAsync(int tvShowId, int userId);
    }
}
