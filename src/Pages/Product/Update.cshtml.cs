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

            if(Paw.Paw.Name == null)
            {
                ModelState.AddModelError("Paw.Paw.Name", "Please donot keep this field empty");
                return Page();
            }
            if (Paw.Paw.Name == "")
            {
                ModelState.AddModelError("Paw.Paw.Name", "Please donot keep this field empty");
                return Page();
            }
            if (Paw.Paw.Breed == null)
            {
                ModelState.AddModelError("Paw.Paw.Breed", "Please donot keep this field empty");
                return Page();
            }
            if (Paw.Paw.Breed == "")
            {
                ModelState.AddModelError("Paw.Paw.Breed", "Please donot keep this field empty");
                return Page();
            }
            if (Paw.Paw.Age == null)
            {
                ModelState.AddModelError("Paw.Paw.Age", "Please donot keep this field empty");
                return Page();
            }
            if (Paw.Paw.Age == "")
            {
                ModelState.AddModelError("Paw.Paw.Age", "Please donot keep this field empty");
                return Page();
            }
            if (Paw.Paw.Size == null)
            {
                ModelState.AddModelError("Paw.Paw.Size", "Please donot keep this field empty");
                return Page();
            }
            if (Paw.Paw.Size == "")
            {
                ModelState.AddModelError("Paw.Paw.Size", "Please donot keep this field empty");
                return Page();
            }
            if (Paw.Paw.Description == null)
            {
                ModelState.AddModelError("Paw.Paw.Description", "Please donot keep this field empty");
                return Page();
            }
            if (Paw.Paw.Description == "")
            {
                ModelState.AddModelError("Paw.Paw.Description", "Please donot keep this field empty");
                return Page();
            }
            if (Paw.Paw.Image == null)
            {
                ModelState.AddModelError("Paw.Paw.Image", "Please donot keep this field empty");
                return Page();
            }
            if (Paw.Paw.Image == "")
            {
                ModelState.AddModelError("Paw.Paw.Image", "Please donot keep this field empty");
                return Page();
            }
            if (Paw.Owner.Name == null)
            {
                ModelState.AddModelError("Paw.Owner.Name", "Please donot keep this field empty");
                return Page();
            }
            if (Paw.Owner.Name == "")
            {
                ModelState.AddModelError("Paw.Owner.Name", "Please donot keep this field empty");
                return Page();
            }
            if (Paw.Owner.Address == null)
            {
                ModelState.AddModelError("Paw.Owner.Address", "Please donot keep this field empty");
                return Page();
            }
            if (Paw.Owner.Address == "")
            {
                ModelState.AddModelError("Paw.Owner.Address", "Please donot keep this field empty");
                return Page();
            }
            if (Paw.Owner.City == null)
            {
                ModelState.AddModelError("Paw.Owner.City", "Please donot keep this field empty");
                return Page();
            }
            if (Paw.Owner.City == "")
            {
                ModelState.AddModelError("Paw.Owner.City", "Please donot keep this field empty");
                return Page();
            }
            if (Paw.Owner.Zipcode == null)
            {
                ModelState.AddModelError("Paw.Owner.Zipcode", "Please donot keep this field empty");
                return Page();
            }
            if (Paw.Owner.Zipcode == "")
            {
                ModelState.AddModelError("Paw.Owner.Zipcode", "Please donot keep this field empty");
                return Page();
            }
            if (Paw.Owner.Email == null)
            {
                ModelState.AddModelError("Paw.Owner.Email", "Please donot keep this field empty");
                return Page();
            }
            if (Paw.Owner.Email == "")
            {
                ModelState.AddModelError("Paw.Owner.Email", "Please donot keep this field empty");
                return Page();
            }
            if (Paw.Owner.Phone == null)
            {
                ModelState.AddModelError("Paw.Owner.Phone", "Please donot keep this field empty");
                return Page();
            }
            if (Paw.Owner.Phone == "")
            {
                ModelState.AddModelError("Paw.Owner.Phone", "Please donot keep this field empty");
                return Page();
            }

            PawService.UpdatePaw(Paw);

            return RedirectToPage("./Index");
        }
    }
}
