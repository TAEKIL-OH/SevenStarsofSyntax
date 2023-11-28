using ContosoCrafts.WebSite.Models;
using ContosoCrafts.WebSite.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ContosoCrafts.WebSite.Pages.Product
{
    /// <summary>
    /// This CreateModel is the class for creating new paw data
    /// </summary>
    public class CreateModel : PageModel
    {
        /// <summary>
        /// Constructor of Create Model
        /// </summary>
        /// <param name="pawService"></param>
        public CreateModel(JsonFilePawService pawService)
        {
            PawService = pawService;
        }

        // Getter of PawService
        public JsonFilePawService PawService { get; }

        // The data to be created
        [BindProperty]

        //Getter and Setter of DetailedPawModel
        public DetailedPawModel Paw { get; set; }

        /// <summary>
        /// REST OnPost method to create a new paw data
        /// </summary>
        /// <returns></returns>
        public IActionResult OnPost()
        {
            // If model state is invalid then it will return to the page
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("ModelOnly", "Something went wrong");
                return Page();
            }

            // If Id field is null then it will return to the page with validation error
            if (Paw.Id == null)
            {
                ModelState.AddModelError("Paw.Id", "Please donot keep this field empty");
                return Page();
            }

            // If Id field is empty then it will return to the page with validation error
            if (Paw.Id == "")
            {
                ModelState.AddModelError("Paw.Id", "Please donot keep this field empty");
                return Page();
            }

            // If name field is null then it will return to the page with validation error
            if (Paw.Paw.Name == null)
            {
                ModelState.AddModelError("Paw.Paw.Name", "Please donot keep this field empty");
                return Page();
            }

            // If name field is empty then it will return to the page with validation error
            if (Paw.Paw.Name == "")
            {
                ModelState.AddModelError("Paw.Paw.Name", "Please donot keep this field empty");
                return Page();
            }

            // If breed field is null then it will return to the page with validation error
            if (Paw.Paw.Breed == null)
            {
                ModelState.AddModelError("Paw.Paw.Breed", "Please donot keep this field empty");
                return Page();
            }

            // If breed field is empty then it will return to the page with validation error
            if (Paw.Paw.Breed == "")
            {
                ModelState.AddModelError("Paw.Paw.Breed", "Please donot keep this field empty");
                return Page();
            }

            // If Age is less then 0.1 then it will return to the page with validation error
            if (Paw.Paw.Age < 0.1)
            {
                ModelState.AddModelError("Paw.Paw.Age", "Please have the age lager than 0");
                return Page();
            }

            // If Size field is null then it will return to the page with validation error
            if (Paw.Paw.Size == null)
            {
                ModelState.AddModelError("Paw.Paw.Size", "Please donot keep this field empty");
                return Page();
            }

            // If Size field is empty then it will return to the page with validation error
            if (Paw.Paw.Size == "")
            {
                ModelState.AddModelError("Paw.Paw.Size", "Please donot keep this field empty");
                return Page();
            }

            // If Description field is over limit then it will return to the page with validation error
            if (Paw.Paw.Description.Length > 255)
            {
                ModelState.AddModelError("Paw.Paw.Description", "Please donot keep this field empty");
                return Page();
            }

            // If Image field is null then it will return to the page with validation error
            if (Paw.Paw.Image == null)
            {
                ModelState.AddModelError("Paw.Paw.Image", "Please donot keep this field empty");
                return Page();
            }

            // If Image field is empty then it will return to the page with validation error
            if (Paw.Paw.Image == "")
            {
                ModelState.AddModelError("Paw.Paw.Image", "Please donot keep this field empty");
                return Page();
            }

            //If Owner Name field is null then it will return to the page with validation error
            if (Paw.Owner.Name == null)
            {
                ModelState.AddModelError("Paw.Owner.Name", "Please donot keep this field empty");
                return Page();
            }

            //If Owner Name field is empty then it will return to the page with validation error
            if (Paw.Owner.Name == "")
            {
                ModelState.AddModelError("Paw.Owner.Name", "Please donot keep this field empty");
                return Page();
            }

            //If Owner Address field is over limit then it will return to the page with validation error
            if (Paw.Owner.Address.Length > 255)
            {
                ModelState.AddModelError("Paw.Owner.Address", "Please donot keep this field empty");
                return Page();
            }

            //If Owner City field is null then it will return to the page with validation error
            if (Paw.Owner.City == null)
            {
                ModelState.AddModelError("Paw.Owner.City", "Please donot keep this field empty");
                return Page();
            }

            //If Owner City field is empty then it will return to the page with validation error
            if (Paw.Owner.City == "")
            {
                ModelState.AddModelError("Paw.Owner.City", "Please donot keep this field empty");
                return Page();
            }

            //If Owner Zipcode field is empty then it will return to the page with validation error
            if (Paw.Owner.Zipcode == null)
            {
                ModelState.AddModelError("Paw.Owner.Zipcode", "Please donot keep this field empty");
                return Page();
            }

            //If Owner Zipcode field is empty then it will return to the page with validation error
            if (Paw.Owner.Zipcode == "")
            {
                ModelState.AddModelError("Paw.Owner.Zipcode", "Please donot keep this field empty");
                return Page();
            }

            //If Owner Email field is null then it will return to the page with validation error
            if (Paw.Owner.Email == null)
            {
                ModelState.AddModelError("Paw.Owner.Email", "Please donot keep this field empty");
                return Page();
            }

            //If Owner Email field is empty then it will return to the page with validation error
            if (Paw.Owner.Email == "")
            {
                ModelState.AddModelError("Paw.Owner.Email", "Please donot keep this field empty");
                return Page();
            }

            //If Owner Phone field is null then it will return to the page with validation error
            if (Paw.Owner.Phone == null)
            {
                ModelState.AddModelError("Paw.Owner.Phone", "Please donot keep this field empty");
                return Page();
            }

            //If Owner Phone field is empty then it will return to the page with validation error
            if (Paw.Owner.Phone == "")
            {
                ModelState.AddModelError("Paw.Owner.Phone", "Please donot keep this field empty");
                return Page();
            }

            //If all validation are passed then create the paw and redirect to Index page
            PawService.CreatePaw(Paw);
            return RedirectToPage("./Index");
        }

    }

}