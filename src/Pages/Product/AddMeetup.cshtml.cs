using ContosoCrafts.WebSite.Models;
using ContosoCrafts.WebSite.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace ContosoCrafts.WebSite.Pages.Product
{
    /// <summary>
    /// This AddMeetup model is the class for adding new meetup data
    /// </summary>
    public class AddMeetupModel : PageModel
    {

        /// <summary>
        /// Constructor of AddMeetup Model
        /// </summary>
        /// <param name="pawService"></param>
        public AddMeetupModel(JsonFilePawService pawService)
        {
            PawService = pawService;
        }

        // Getter of PawService
        public JsonFilePawService PawService { get; }


        // The data to be used while adding paws
        [BindProperty]
        public IEnumerable<PawModel> Paw { get; set; }
        public string pawOne { get; set; }
        public string pawTwo { get; set; }
        public string meetupDate { get; set; }
        public string meetupLocation { get; set; }
        public string message { get; set; }

        /// <summary>
        /// REST OnGet method to get list of paws
        /// </summary>
        /// <returns></returns>
        public void OnGet()
        {
            Paw = PawService.GetPaws();
        }

        /// <summary>
        /// Rest OnPost method to add meetup
        /// </summary>
        /// <returns></returns>
        public IActionResult OnPost(string pawOne, string pawTwo, string meetupDate, string meetupLocation, string message)
        {
            // If model state is invalid then it will return to the page
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("ModelOnly", "Something went wrong");
                return Page();
            }

            // If pawOne field is null then it will return to the page with validation error
            if (pawOne == null)
            {
                ModelState.AddModelError("pawOne", "Please donot keep this field empty");
                return Page();
            }

            // If pawOne field is empty then it will return to the page with validation error
            if (pawOne == "")
            {
                ModelState.AddModelError("pawOne", "Please donot keep this field empty");
                return Page();
            }

            // If pawTwo field is null then it will return to the page with validation error
            if (pawTwo == null)
            {
                ModelState.AddModelError("pawTwo", "Please donot keep this field empty");
                return Page();
            }

            // If pawTwo field is empty then it will return to the page with validation error
            if (pawTwo == "")
            {
                ModelState.AddModelError("pawTwo", "Please donot keep this field empty");
                return Page();
            }

            //If the paw exists then it will add new meetup
            bool isValidMeetup = PawService.AddMeetup(pawOne, pawTwo, meetupDate, meetupLocation, message);

            if (isValidMeetup == false)
            {
                ModelState.AddModelError("ModelOnly", "Something went wrong");
                return Page();
            }
            return RedirectToPage("./Index");
        }
    }
}