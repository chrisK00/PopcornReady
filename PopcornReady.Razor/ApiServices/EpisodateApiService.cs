using System;
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
            var episodedateTvShow = (await client.GetFromJsonAsync<EpisodateRootobject>($"{_showDetailsUri}?q={name}")).TvShow;

            var tvShow = new TvShow
            {
                Name = episodedateTvShow.Name,
                ApiId = episodedateTvShow.Id,
                Status = episodedateTvShow.Status,

            };

            if (episodedateTvShow.NextEpisode == null)
            {
                return tvShow;
            }

            tvShow.NextEpisode = new Episode
            {
                AirDate = DateTime.Parse(episodedateTvShow.NextEpisode.AirDate),
                Name = episodedateTvShow.NextEpisode.Name,
                Season = episodedateTvShow.NextEpisode.Season,
                Number = episodedateTvShow.NextEpisode.Episode
            };

            return tvShow;
        }
    }
}