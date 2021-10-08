using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PopcornReady.Core.Data.Entities;
using PopcornReady.Core.Services;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace PopcornReady.Razor.Pages.TvShows
{
    public class IndexModel : PageModel
    {
        private readonly ITvShowsService _tvShowsService;

        public IndexModel(ITvShowsService tvShowsService)
        {
            _tvShowsService = tvShowsService;
        }

        [MinLength(2, ErrorMessage = "Search for a name with more than 2 characters")]
        [BindProperty]
        public string Search { get; set; }

        public IEnumerable<TvShow> TvShows { get; set; }

        public async Task OnGetAsync()
        {
            // TOOD: remove hardcoded 1
            TvShows = await _tvShowsService.GetAllAsync(1);
        }

        public ActionResult OnPost()
        {
            return !ModelState.IsValid ? Page() : RedirectToPage("./Add", new { Search });
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            await _tvShowsService.RemoveAsync(id, 1);
            return RedirectToPage();
        }
    }
}