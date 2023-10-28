using ContosoCrafts.WebSite.Models;
using ContosoCrafts.WebSite.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;

namespace ContosoCrafts.WebSite.Pages.Product
{
    public class DeleteModel : PageModel
    {
        public JsonFilePawService PawService { get; }
        public DeleteModel(JsonFilePawService pawService)
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
                ModelState.AddModelError("ModelOnly", "Something went wrong");
                return Page();
            }
            var CheckPaw = PawService.GetPaws().FirstOrDefault(m => m.Id.Equals(Paw.Id));
            if (CheckPaw == null)
            {
                ModelState.AddModelError("ModelOnly", "Cannot find this paw");
                return Page();
            }

            PawService.DeletePaw(CheckPaw.Id);
            return RedirectToPage("./Index");
        }
    }
}
