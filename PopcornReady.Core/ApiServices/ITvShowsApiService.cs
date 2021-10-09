using System.Threading.Tasks;
using PopcornReady.Core.Data.Entities;

namespace PopcornReady.Core.ApiServices
{
    public interface ITvShowsApiService
    {
        Task<TvShow> GetTvShowAsync(string name);
    }
}
