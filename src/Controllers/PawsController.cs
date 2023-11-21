using ContosoCrafts.WebSite.Models;
using ContosoCrafts.WebSite.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ContosoCrafts.WebSite.Controllers
{
    /// <summary>
    /// PawsController will enable calls to be made directly to controller
    /// </summary>
    public class PawsController : ControllerBase
    {
        /// <summary>
        /// Constructor of PawsControlller 
        /// </summary>
        /// <param name="pawService"></param>
        public PawsController(JsonFilePawService pawService)
        {
            PawService = pawService;
        }

        //Variale of PawService getter
        public JsonFilePawService PawService { get; }

        /// <summary>
        /// varaiable of IEnumerable of PawModel Type
        /// </summary>
        /// <returns> GetPaws -> Paws Data </returns>
        [HttpGet]
        public IEnumerable<DetailedPawModel> Get()
        {
            //Returned the list of all paws using the PawService method GetPaws()
            return PawService.GetPaws();
        }
    }
}
