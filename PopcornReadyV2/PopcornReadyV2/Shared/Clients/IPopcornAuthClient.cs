using PopcornReadyV2.Shared.Params;
using PopcornReadyV2.Shared.Responses;
using Refit;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PopcornReadyV2.Shared.Clients
{
    public interface IPopcornAuthClient
    {
        [Get("/api/tvshows/my-shows")]
        Task<IEnumerable<TvShowResponse>> GetMyShowsAsync(TvShowParams parameters = null);

        [Get("/api/tvshows/{title}")]
        Task<ApiResponse<TvShowResponse>> GetTvShowAsync(string title);

        [Post("/api/tvshows/{title}")]
        Task<ApiResponse<TvShowResponse>> AddTvShowAsync(string title);

        // if dont return something like a JsonResponse that wraps should try catch this
        [Delete("/api/tvshows/{id}")]
        Task RemoveTvShowAsync(int id);
    }
}
