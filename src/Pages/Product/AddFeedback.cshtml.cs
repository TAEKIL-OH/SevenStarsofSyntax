using System.Linq;
using ContosoCrafts.WebSite.Models;
using ContosoCrafts.WebSite.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ContosoCrafts.WebSite.Pages.Product
{
    /// <summary>
    /// This AddFeedackModel is the class for adding feedback in paw data
    /// </summary>
    public class AddFeedbackModel : PageModel
    {
        /// <summary>
        /// Constructor of AddFeedackModel 
        /// </summary>
        /// <param name="pawService"></param>
        public AddFeedbackModel(JsonFilePawService pawService)
        {
            PawService = pawService;
        }

        //Getter of paw service
        public JsonFilePawService PawService { get; }


        [BindProperty]

        //Getter and setter of paw model
        public DetailedPawModel Paw { get; set; }

        //Getter and setter of message field
        public string message { get; set; }

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

        /// <summary>
        /// REST OnPost method to add a new feedback data
        /// </summary>
        /// <returns></returns>
        public IActionResult OnPost(string message)
        {
            // If model state is invalid then it will return to the page
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("ModelOnly", "Something went wrong");
                return Page();
            }

            // If message field is null then it will return to the page with validation error
            if (message == null)
            {
                ModelState.AddModelError("message", "Please donot keep this field empty");
                return Page();
            }

            // If message field is empty then it will return to the page with validation error
            if (message == "")
            {
                ModelState.AddModelError("message", "Please donot keep this field empty");
                return Page();
            }

            // If message field length is over limit then it will return to the page with validation error
            if (message.Length > 255)
            {
                ModelState.AddModelError("message", "Please limit the message");
                return Page();
            }

            // It will check that feedack is added if yes then it will redirect to index page otherwise it will return to current page with error
            bool isFeedbackAdded = PawService.AddFeedckToPaw(Paw.Id, message);

            if (isFeedbackAdded == false)
            {
                TempData["ErrorMessage"] = "Something went wrong while adding feedack to the paw please retry";
                return RedirectToPage("../Error");
            }

            return RedirectToPage("./Index");
        }

    }

}