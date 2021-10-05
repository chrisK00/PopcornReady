using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using PopcornReady.Razor.ApiModels.Episodate;
using PopcornReady.Razor.Entities;

namespace PopcornReady.Razor.ApiServices
{
    public class EpisodateApiService : ITvShowsApiService
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly string _showDetailsUri = $"show-details";

        public EpisodateApiService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<TvShow> GetTvSeriesAsync(string name)
        {
            var client = _clientFactory.CreateClient("episodate");
            var episodedateTvSeries = await client.GetFromJsonAsync<EpisodateRootobject>($"{_showDetailsUri}?q={name}");

            //use automapper and its conversions e.x date should be datetime and wtf is countdown of type object
            var tvSeries = new TvShow
            {
                Name = episodedateTvSeries.TvShow.Name,
                ApiId = episodedateTvSeries.TvShow.Id,
                Status = episodedateTvSeries.TvShow.Status
            };

            return tvSeries;
        }
    }
}