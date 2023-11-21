using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc.RazorPages;

using ContosoCrafts.WebSite.Models;
using ContosoCrafts.WebSite.Services;

namespace ContosoCrafts.WebSite.Pages.Product
{
    /// <summary>
    /// This IndexModel is the class for displaying the list of paws data in the tabular manner
    /// </summary
    public class PawIndexModel : PageModel
    {
        /// <summary>
        /// Constructor for Index Model
        /// </summary>
        /// <param name="productService"></param>
        public PawIndexModel(JsonFilePawService pawService)
        {
            PawService = pawService;
        }

        // Getter for PaswServices
        public JsonFilePawService PawService { get; }

        //Getter and Private Setter for DetailedPawModel
        public IEnumerable<DetailedPawModel> Paws { get; private set; }

        /// <summary>
        /// REST OnGet, return all data
        /// </summary>
        public void OnGet()
        {
            Paws = PawService.GetPaws();
        }
    }
}