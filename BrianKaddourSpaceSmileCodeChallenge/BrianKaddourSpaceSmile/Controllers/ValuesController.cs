using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SpaceSmileBrianKaddour.ApplicationCore.Entities;
using SpaceSmileBrianKaddour.ApplicationCore.Interfaces;
using SpaceSmileBrianKaddour.Web.Extensions;
using SpaceSmileBrianKaddour.Web.Interfaces;

namespace BrianKaddourSpaceSmile.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {


        private readonly ILaunchpadApiClient _launchPadServiceClient;
        

        // Add the Filters in 
        //private Dictionary<string, string> _filters;



        public ValuesController(ILaunchpadApiClient launchPadServiceClientIn)
        {
            _launchPadServiceClient = launchPadServiceClientIn;
        }


        // Geta all launchpads
        // GET api/values
        [HttpGet]
        public async Task<IActionResult> Get()
        {

            IEnumerable<LaunchPadInfo> launchPadModels = await _launchPadServiceClient.GetValues();

            //Consider putting Lamda expression into an adapter or using inheritance instead
            IEnumerable<LaunchPad> convertedLaunchPads = launchPadModels.Select(
            x => new LaunchPad
            {
                LaunchpadID = x.Id,
                LaunchpadName = x.FullName,
                LaunchpadStatus = x.Status
            });
            return Ok(convertedLaunchPads);
        }

        //// GET api/values
        //[HttpGet]
        //public ActionResult<IEnumerable<string>> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            IEnumerable<LaunchPadInfo> launchPadModels = await _launchPadServiceClient.GetValues(); ;

            //Consider putting Lamda expression into an adapter or using inheritance instead
            var restrictedPad = launchPadModels.FirstOrDefault(p => p.Id == id);
            return Ok(restrictedPad);
        }



        

        //// POST api/values
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT api/values/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/values/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
