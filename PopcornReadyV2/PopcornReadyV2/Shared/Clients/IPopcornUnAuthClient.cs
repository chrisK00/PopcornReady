using PopcornReadyV2.Shared.Params;
using PopcornReadyV2.Shared.Responses;
using Refit;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PopcornReadyV2.Shared.Clients
{
    public interface IPopcornUnAuthClient
    {
        [Get("/api/tvshows")]
        Task<IEnumerable<TvShowResponse>> GetTvShowsAsync(TvShowParams parameters = null);
    }
}
