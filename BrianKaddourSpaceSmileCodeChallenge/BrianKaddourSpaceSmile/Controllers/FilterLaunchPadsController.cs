using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpaceSmileBrianKaddour.ApplicationCore.Entities;
using SpaceSmileBrianKaddour.Web.Extensions;
using SpaceSmileBrianKaddour.Web.Interfaces;

namespace BrianKaddourSpaceSmile.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilterLaunchPadsController : ControllerBase
    {

        private readonly ILaunchpadService _service;
        private readonly ILaunchpadApiClient _launchPadServiceClient;


        public FilterLaunchPadsController(ILaunchpadService service, ILaunchpadApiClient launchPadServiceClientIn)
        {
            _service = service;
            _launchPadServiceClient = launchPadServiceClientIn;
        }


        //// GET: api/FilterLaunchPads
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        // GET: api/FilterLaunchPads/5
        //http://localhost:28153/api/FilterLaunchPads/?filter=active
        [HttpGet(Name = "GetLaunchPads")]
        public async Task<IActionResult> GetAsync(string filter)
        {

            IEnumerable<LaunchPadInfo> launchPadModels = await _launchPadServiceClient.GetValues();



            //IEnumerable<Person> p = pwa.Select(p => new Person { Id = p.Id, Name = p.Name });


            //Consider putting Lamda expression into an adapter or using inheritance instead
            IEnumerable<LaunchPad> convertedLaunchPads = launchPadModels.Select(
            x => new LaunchPad
            {
                LaunchpadID = x.Id,
                LaunchpadName = x.FullName,
                LaunchpadStatus = x.Status
            });
            var listFiltered = _service.filterLaunchPadModels(filter,convertedLaunchPads);
            return Ok(listFiltered);
        }

        //// POST: api/FilterLaunchPads
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT: api/FilterLaunchPads/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE: api/ApiWithActions/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
