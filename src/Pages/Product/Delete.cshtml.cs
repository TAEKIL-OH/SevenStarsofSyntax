using ContosoCrafts.WebSite.Models;
using ContosoCrafts.WebSite.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;

namespace ContosoCrafts.WebSite.Pages.Product
{
    /// <summary>
    /// This DeleteModel is the class for deleting the existing paw data
    /// </summary>
    public class DeleteModel : PageModel
    {
        /// <summary>
        /// Constructor of DeleteModel
        /// </summary>
        /// <param name="pawService"></param>
        public DeleteModel(JsonFilePawService pawService)
        {
            PawService = pawService;
        }

        // Getter for PawServices
        public JsonFilePawService PawService { get; }


        // The data to show and delete
        [BindProperty]

        //Getter and Setter of DetailedPawModel class
        public DetailedPawModel Paw { get; set; }

        /// <summary>
        /// Rest OnGet method to display the data of selected paw 
        /// </summary>
        /// <param name="id"></param>
        public IActionResult OnGet(string id)
        {
            Paw = PawService.GetPaws().FirstOrDefault(m => m.Id.Equals(id));

            if (Paw == null)
            {
                TempData["ErrorMessage"] = "Something went wrong while fetching the paw please retry";
                return RedirectToPage("../Error");
            }

            return Page();
        }

        /// <summary>
        /// REST OnPost method to delete the paw data
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

            //If the paw exists then it will delete that paw and redirect to index page
            bool isValidDelete = PawService.DeletePaw(Paw.Id);
            if (isValidDelete == false)
            {
                TempData["ErrorMessage"] = "Something went wrong while deleting the paw please retry";
                return RedirectToPage("../Error");
            }
            return RedirectToPage("./Index");
        }
    }
}