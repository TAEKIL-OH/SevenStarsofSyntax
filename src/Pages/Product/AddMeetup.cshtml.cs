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
        public string pawToMeet { get; set; }
        public string meetupDate { get; set; }
        public string meetupLocation { get; set; }
        public string message { get; set; }

        public void OnGet()
        {
            Paw = PawService.GetPaws();
        }
     

    }
}
