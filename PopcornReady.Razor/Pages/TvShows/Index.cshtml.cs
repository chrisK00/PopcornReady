using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PopcornReady.Razor.ApiServices;
using PopcornReady.Razor.Entities;
using PopcornReady.Razor.Services;

namespace PopcornReady.Razor.Pages.TvShows
{
    public class IndexModel : PageModel
    {
        private readonly ITvShowsApiService _tvSeriesApiService;
        private readonly ITvShowsService _tvShowsService;

        public IndexModel(ITvShowsApiService tvSeriesApiService, ITvShowsService tvShowsService)
        {
            _tvSeriesApiService = tvSeriesApiService;
            _tvShowsService = tvShowsService;
        }

        [BindProperty]
        public string Search { get; set; }

        public IEnumerable<TvShow> TvShows { get; set; }
        public async Task OnGetAsync()
        {
            var tvShows = await _tvShowsService.GetAllAsync();
            TvShows = tvShows;
        }
        public async Task<ActionResult> OnPostAsync()
        {
            if (string.IsNullOrWhiteSpace(Search))
            {
                return Page();
            }

            var tvShow = await _tvSeriesApiService.GetTvSeriesAsync(Search);
            return tvShow == null ? Page() : RedirectToPage("./Add", tvShow);
        }
    }
}