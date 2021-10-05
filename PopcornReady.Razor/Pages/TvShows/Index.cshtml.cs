using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PopcornReady.Core.Data.Entities;
using PopcornReady.Core.Services;

namespace PopcornReady.Razor.Pages.TvShows
{
    public class IndexModel : PageModel
    {
        private readonly ITvShowsService _tvShowsService;

        public IndexModel(ITvShowsService tvShowsService)
        {
            _tvShowsService = tvShowsService;
        }

        [Required, MinLength(2, ErrorMessage = "Search for a name with more than 2 characters")]
        [BindProperty]
        public string Search { get; set; }

        public IEnumerable<TvShow> TvShows { get; set; }

        public async Task OnGetAsync()
        {
            TvShows = await _tvShowsService.GetAllAsync();
        }

        public ActionResult OnPost()
        {
            return !ModelState.IsValid ? Page() : RedirectToPage("./Add", new { Search });
        }
    }
}