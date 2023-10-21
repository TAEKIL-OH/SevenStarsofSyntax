using ContosoCrafts.WebSite.Models;
using ContosoCrafts.WebSite.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;

namespace ContosoCrafts.WebSite.Pages.Product
{
    public class UpdateModel : PageModel
    {
        public JsonFilePawService PawService { get; }

        public UpdateModel(JsonFilePawService pawService)
        {
            PawService = pawService;
        }



        // The data to show
        [BindProperty]
        public PawModel Paw { get; set; }
        public void OnGet(string id)
        {
            Paw = PawService.GetPaws().FirstOrDefault(m => m.Id.Equals(id));
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            PawService.UpdatePaw(Paw);

            return RedirectToPage("./Index");
        }
    }
}
