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
            // TODO: Move this logic in to the service after moving it to its own library
            TvShow = await _tvShowsService.FindAsync(Search);
            return TvShow == null ? RedirectToPage("./Index") : Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _tvShowsService.AddAsync(TvShow);
            return RedirectToPage("./Index");
        }
    }
}