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

        //Getter and Setter of Detailed Paw Model
        public IEnumerable<DetailedPawModel> Paw { get; set; }

        //Getter and Setter of First Paw
        public string pawOne { get; set; }

        //Getter and Setter of Second Paw
        public string pawTwo { get; set; }

        //Getter and Setter of Date of Meetup
        public string meetupDate { get; set; }

        //Getter and Setter of Location of Meetup
        public string meetupLocation { get; set; }

        //Getter and Setter of Special Message
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
        /// REST OnPost method for adding meetup
        /// </summary>
        /// <param name="pawOne"></param>
        /// <param name="pawTwo"></param>
        /// <param name="meetupDate"></param>
        /// <param name="meetupLocation"></param>
        /// <param name="message"></param>
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

            // If meetupDate field is null then it will return to the page with validation error
            if (meetupDate == null)
            {
                ModelState.AddModelError("meetupDate", "Please donot keep this field empty");
                return Page();
            }

            // If meetupDate field is empty then it will return to the page with validation error
            if (meetupDate == "")
            {
                ModelState.AddModelError("meetupDate", "Please donot keep this field empty");
                return Page();
            }

            // If meetupLocation field is null then it will return to the page with validation error
            if (meetupLocation == null)
            {
                ModelState.AddModelError("meetupLocation", "Please donot keep this field empty");
                return Page();
            }

            // If meetupLocation field is empty then it will return to the page with validation error
            if (meetupLocation == "")
            {
                ModelState.AddModelError("meetupLocation", "Please donot keep this field empty");
                return Page();
            }

            // If message field is over limit then it will return to the page with validation error
            if (message.Length > 255)
            {
                ModelState.AddModelError("message", "Please limit your message");
                return Page();
            }


            //If the paw exists then it will add new meetup
            bool isValidMeetup = PawService.AddMeetup(pawOne, pawTwo, meetupDate, meetupLocation, message);

            if (isValidMeetup == false)
            {
                TempData["ErrorMessage"] = "Something went wrong while adding meetup to the paw(s) please retry";
                return RedirectToPage("../Error");
            }
            return RedirectToPage("./Index");
        }
    }
}