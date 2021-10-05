using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PopcornReady.Razor.ApiServices;
using PopcornReady.Razor.Entities;
using PopcornReady.Razor.Services;

namespace PopcornReady.Razor.Pages.TvShows
{
    public class AddModel : PageModel
    {
        private readonly ITvShowsApiService _tvSeriesApiService;
        private readonly ITvShowsService _tvShowsService;

        public AddModel(ITvShowsService tvShowsService, ITvShowsApiService tvSeriesApiService)
        {
            _tvShowsService = tvShowsService;
            _tvSeriesApiService = tvSeriesApiService;
        }

        [BindProperty(SupportsGet = true)]
        public string Search { get; set; }

        [BindProperty]
        public TvShow TvShow { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
           var tvShow = await _tvSeriesApiService.GetTvSeriesAsync(Search);
            if (tvShow == null)
            {
                return RedirectToPage("./Index");
            }

            TvShow = tvShow;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _tvShowsService.AddAsync(TvShow);
            return RedirectToPage("./Index");
        }
    }
}