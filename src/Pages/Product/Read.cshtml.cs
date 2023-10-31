using System.Linq;

using Microsoft.AspNetCore.Mvc.RazorPages;

using ContosoCrafts.WebSite.Models;
using ContosoCrafts.WebSite.Services;

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
        public PawModel Paw;

        /// <summary>
        /// REST Get request for the particular paw
        /// </summary>
        /// <param name="id"></param>
        public void OnGet(string id)
        {
            Paw = PawService.GetPaws().FirstOrDefault(m => m.Id.Equals(id));
        }
    }
}