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

    }
}
