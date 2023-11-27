using System.Linq;

using Microsoft.AspNetCore.Mvc.RazorPages;

using ContosoCrafts.WebSite.Models;
using ContosoCrafts.WebSite.Services;
using Microsoft.AspNetCore.Mvc;

namespace ContosoCrafts.WebSite.Pages.Product
{
    /// <summary>
    /// ReadModel is the main class for displaying the information of selected paw 
    /// </summary>
    public class ReadModel : PageModel
    {
        /// <summary>
        /// COnstructor for Read Model
        /// </summary>
        /// <param name="pawService"></param>
        public ReadModel(JsonFilePawService pawService)
        {
            PawService = pawService;
        }

        //Getter for PawServices
        public JsonFilePawService PawService { get; }

        // Paw data to show
        public DetailedPawModel Paw;

        /// <summary>
        /// REST Get request for the particular paw
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

    }

}