using ContosoCrafts.WebSite.Models;
using ContosoCrafts.WebSite.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ContosoCrafts.WebSite.Pages.Product
{
    public class CreateModel : PageModel
    {
        public JsonFilePawService PawService { get; }

        public CreateModel(JsonFilePawService pawService)
        {
            PawService = pawService;
        }

        // The data to show
        [BindProperty]
        public PawModel Paw { get; set; }

        public void OnGet()
        {
        }
    }
}
