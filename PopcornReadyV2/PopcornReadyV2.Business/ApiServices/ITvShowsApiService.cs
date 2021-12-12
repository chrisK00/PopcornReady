using PopcornReadyV2.Business.Data.Entities;
using System.Threading.Tasks;

namespace PopcornReady.Core.ApiServices
{
    public interface ITvShowsApiService
    {
        Task<TvShow> GetTvShowAsync(string name);
    }
}
