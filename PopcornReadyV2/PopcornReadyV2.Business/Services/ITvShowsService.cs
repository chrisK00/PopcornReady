using PopcornReadyV2.Business.Data.Entities;
using PopcornReadyV2.Shared.Params;
using PopcornReadyV2.Shared.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PopcornReady.Core.Services
{
    public interface ITvShowsService
    {
        Task<IEnumerable<TvShowResponse>> GetAllAsync(TvShowParams param);
        Task<IEnumerable<TvShowResponse>> GetAllForUserAsync(TvShowParams param, string userId);

        // TODO: remove when have a global exc handler
        Task<(TvShowResponse TvShow, string Error)> AddAsync(string title, string userId);
        Task<TvShowResponse> FindAsync(string name);
        Task RemoveAsync(int tvShowId, string userId);
    }
}
