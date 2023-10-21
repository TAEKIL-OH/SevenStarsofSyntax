using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

using ContosoCrafts.WebSite.Models;
using ContosoCrafts.WebSite.Services;
//This comment is by Zhou
namespace ContosoCrafts.WebSite.Pages
{
    /// <summary>
    /// Mike Koenig
    /// TAEKIL OH
    /// YASH MODI
    /// Gayathri Gandham
    /// Zhou Liu
    /// </summary>
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger,
            JsonFilePawService pawService)
        {
            _logger = logger;
            PawService = pawService;
        }

        public JsonFilePawService PawService { get; }
        public IEnumerable<PawModel> Paws { get; private set; }

        public void OnGet()
        {
            Paws = PawService.GetPaws();
        }
    }
}