using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PopcornReady.Core.Data.Entities;
using PopcornReady.Core.Services;

namespace PopcornReady.Razor.Pages.TvShows
{
    public class AddModel : PageModel
    {
        private readonly ITvShowsService _tvShowsService;

        public AddModel(ITvShowsService tvShowsService)
        {
            _tvShowsService = tvShowsService;
        }

        [BindProperty(SupportsGet = true)]
        public string Search { get; set; }

        [BindProperty]
        public TvShow TvShow { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            if (string.IsNullOrWhiteSpace(Search)) return RedirectToPage("./Index");

            TvShow = await _tvShowsService.FindAsync(Search);
            return TvShow == null ? RedirectToPage("./Index") : Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            // TODO: Change to take in user id from token
            await _tvShowsService.AddAsync(TvShow, 1);
            return RedirectToPage("./Index");
        }
    }
}