using ContosoCrafts.WebSite.Models;
using ContosoCrafts.WebSite.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace ContosoCrafts.WebSite.Pages.Product
{
    public class AddMeetupModel : PageModel
    {
        public AddMeetupModel(JsonFilePawService pawService)
        {
            PawService = pawService;
        }

        
        public JsonFilePawService PawService { get; }

        
        [BindProperty]
        public IEnumerable<PawModel> Paw { get; set; }
        public string pawOne { get; set; }
        public string pawTwo { get; set; }
        public string meetupDate { get; set; }
        public string meetupLocation { get; set; }
        public string message { get; set; }

        public void OnGet()
        {
            Paw = PawService.GetPaws();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("ModelOnly", "Something went wrong");
                return Page();
            }

            if (pawOne == null)
            {
                ModelState.AddModelError("pawOne", "Please donot keep this field empty");
                return Page();
            }

            if (pawOne == "")
            {
                ModelState.AddModelError("pawOne", "Please donot keep this field empty");
                return Page();
            }

            if (pawTwo == null)
            {
                ModelState.AddModelError("pawTwo", "Please donot keep this field empty");
                return Page();
            }

            if (pawTwo == "")
            {
                ModelState.AddModelError("pawTwo", "Please donot keep this field empty");
                return Page();
            }

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
