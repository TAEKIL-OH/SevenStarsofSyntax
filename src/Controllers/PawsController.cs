using ContosoCrafts.WebSite.Models;
using ContosoCrafts.WebSite.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ContosoCrafts.WebSite.Controllers
{
    public class PawsController : ControllerBase
    {
        public PawsController(JsonFilePawService pawService)
        {
            PawService = pawService;
        }

        public JsonFilePawService PawService { get; }

        [HttpGet]
        public IEnumerable<PawModel> Get()
        {
            return PawService.GetPaws();
        }
    }
}
