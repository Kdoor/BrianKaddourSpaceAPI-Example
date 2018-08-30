using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SpaceSmileBrianKaddour.ApplicationCore.Clients;
using SpaceSmileBrianKaddour.ApplicationCore.Entities;
using SpaceSmileBrianKaddour.ApplicationCore.Interfaces;
using SpaceSmileBrianKaddour.Web.Extensions;
using SpaceSmileBrianKaddour.Web.Interfaces;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Linq;

namespace BrianKaddourSpaceSmile
{
    //Launchpad Service used to build and filter launchpads
    public class LaunchPadService : ILaunchpadService
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _client;
        private readonly ILaunchpadApiClient _LaunchPadInfoClient;

        public LaunchPadService
        ( 
            HttpClient client, 
            ILogger<LaunchPadInfoClient> logger, 
            IConfiguration configuration, 
            ILaunchpadApiClient LaunchPadInfoClient
        )
        {
            _configuration = configuration;
            //Set base URL
            string baseUrl = _configuration["LaunchpadBaseUrl"];
            client.BaseAddress = new Uri(baseUrl);
            _client = client;
            _LaunchPadInfoClient = LaunchPadInfoClient;
        }



        //Call to get launchpads from SpaceX
        //public async Task<IEnumerable<LaunchPadInfo>> GetLaunchPads()
        //{
        //    var response = await _LaunchPadInfoClient.GetValues();

        //    return response;
        //}



        //Call to filter models
        public List<LaunchPad> filterLaunchPadModels(string filteringParams, IEnumerable<LaunchPad> launchPadsIn )
        {
            //List<LaunchPadInfo> query = launchPadsIn.Cast<LaunchPadInfo>().ToList();
            List<LaunchPad> query = launchPadsIn.ToList();

            //TODO: Proof of concept this, needs to be refactored and combined in, probably via an overload
            // also maybe use specifications instead
            var filterBy = filteringParams.Trim().ToLowerInvariant();
            if (!string.IsNullOrEmpty(filterBy))
            {
                query = query
                    .Where(
                        m => m.LaunchpadName.ToLowerInvariant().Contains(filterBy)
                       || m.LaunchpadID.ToLowerInvariant().Contains(filterBy)
                       || m.LaunchpadStatus.ToLowerInvariant().Contains(filterBy)).ToList();
            }

            return query;
        }
    }
}