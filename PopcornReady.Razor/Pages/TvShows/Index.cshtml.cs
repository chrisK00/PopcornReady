using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PopcornReady.Razor.Entities;
using PopcornReady.Razor.Services;

namespace PopcornReady.Razor.Pages.TvShows
{
    public class IndexModel : PageModel
    {
        private readonly ITvShowsService _tvShowsService;

        public IndexModel(ITvShowsService tvShowsService)
        {
            _tvShowsService = tvShowsService;
        }

        [BindProperty]
        public string Search { get; set; }

        public IEnumerable<TvShow> TvShows { get; set; }

        public async Task OnGetAsync()
        {
            TvShows = await _tvShowsService.GetAllAsync();
        }

        public ActionResult OnPost()
        {
            return string.IsNullOrWhiteSpace(Search) ? Page() : RedirectToPage("./Add", new { Search });
        }
    }
}