using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

using ContosoCrafts.WebSite.Models;
using ContosoCrafts.WebSite.Services;

namespace ContosoCrafts.WebSite.Pages
{
    /// <summary>
    /// IndexModel is the main class for displaying the Index Page (Home)
    /// </summary>
    public class IndexModel : PageModel
    {
        // Logger variable used for logging
        private readonly ILogger<IndexModel> _logger;

        /// <summary>
        /// Constructor for IndexModel
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="pawService"></param>
        public IndexModel(ILogger<IndexModel> logger,
            JsonFilePawService pawService)
        {
            _logger = logger;
            PawService = pawService;
        }
        // Getter for PaswServices
        public JsonFilePawService PawService { get; }

        //Getter and Private Setter for DetailedPawModel
        public IEnumerable<DetailedPawModel> Paws { get; private set; }

        // OnGet will get all the Data and render it in the Razor Page
        public void OnGet()
        {
            Paws = PawService.GetPaws();
        }
    }
}