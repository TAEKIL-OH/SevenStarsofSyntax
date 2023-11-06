using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContosoCrafts.WebSite.Models;
using ContosoCrafts.WebSite.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ContosoCrafts.WebSite.Pages.Product
{
	public class AddFeedbackModel : PageModel
    {
        public AddFeedbackModel(JsonFilePawService pawService)
        {
            PawService = pawService;
        }

        public JsonFilePawService PawService { get; }

        [BindProperty]
        public PawModel Paw { get; set; }
        public string message { get; set; }

        public void OnGet(string id)
        {
            Paw = PawService.GetPaws().FirstOrDefault(m => m.Id.Equals(id));
        }

        public IActionResult OnPost(string message)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("ModelOnly", "Something went wrong");
                return Page();
            }

            if (message == null)
            {
                ModelState.AddModelError("message", "Please donot keep this field empty");
                return Page();
            }

            if (message == "")
            {
                ModelState.AddModelError("message", "Please donot keep this field empty");
                return Page();
            }

            bool feedback = PawService.AddFeedckToPaw(Paw.Id, message);
            return RedirectToPage("./Index");

        }

    }
}
