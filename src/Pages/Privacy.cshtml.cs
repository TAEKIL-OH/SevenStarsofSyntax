using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace ContosoCrafts.WebSite.Pages
{

    /// <summary>
    /// This model is made to display the privacy terms of pawfect
    /// </summary>
    public class PrivacyModel : PageModel
    {

        // Logger variable
        private readonly ILogger<PrivacyModel> _logger;

        // Constructor that accepts logger as input
        public PrivacyModel(ILogger<PrivacyModel> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// In this page model OnGet will do nothing
        /// </summary>
        public void OnGet()
        {
        }

    }

}