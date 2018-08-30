using SpaceSmileBrianKaddour.Web.Interfaces;
using Microsoft.Extensions.Configuration;
using SpaceSmileBrianKaddour.ApplicationCore.Entities;
using SpaceSmileBrianKaddour.ApplicationCore.Exceptions;
using SpaceSmileBrianKaddour.ApplicationCore.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace SpaceSmileBrianKaddour.ApplicationCore.Clients
{
    public class LaunchPadInfoClient : ILaunchpadApiClient
    {

        private IConfiguration _configuration;

        public HttpClient Client { get; }

        //Setup a logger
        private readonly ILogger<LaunchPadInfoClient> _logger;

        
        /// <summary>
        /// HTTPClientRepo for LaunchPadInfo using SpaceX API 
        /// </summary>
        /// <param name="client">The HTTP client.</param>
        /// <param name="logger">Pass in a a logger for LaunchPardClient.</param>
        /// <param name="configuration">Pass in config variabls to assign base url.</param>
        public LaunchPadInfoClient(HttpClient client, ILogger<LaunchPadInfoClient> logger, IConfiguration configuration)
        {
            _configuration = configuration;
            //Set base URL
            string baseUrl = _configuration["LaunchpadBaseUrl"];
            client.BaseAddress = new Uri(baseUrl);
            _logger = logger;

            Client = client;
        }
       
        public async Task<IEnumerable<LaunchPadInfo>> GetValues()
        {
            
            try
            {                
                //Optimized HTTPClient return blank object if we catch an errors
                var response = await DeserializeOptimizedFromStreamCallAsync(Client, CancellationToken.None);

                return response;
            }
            catch (HttpRequestException ex)
            {
                _logger.LogInformation($"An error occured connecting to values API {ex.ToString()}");
                return Enumerable.Empty<LaunchPadInfo>();
            }
        }

        // Let's try to make the HTTPClient be a bit better
        // Consider making this a generic for now lets just use LaunchPadInfo
        // https://johnthiriet.com/efficient-api-calls/

        // Overload Method for passing the HttpClient
        private static async Task<List<LaunchPadInfo>> DeserializeOptimizedFromStreamCallAsync(HttpClient clientIn, CancellationToken cancellationToken)
        {
            var client = clientIn;
            using (var request = new HttpRequestMessage(HttpMethod.Get, client.BaseAddress))
            using (var response = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken))
            {
                var stream = await response.Content.ReadAsStreamAsync();

                if (response.IsSuccessStatusCode)
                    return DeserializeJsonFromStream<List<LaunchPadInfo>>(stream);

                var content = await StreamToStringAsync(stream);
                throw new ApiException
                {
                    StatusCode = (int)response.StatusCode,
                    Content = content
                };
            }
        }

        // Something has gone wronng Stream is dumping to string
        private static async Task<string> StreamToStringAsync(Stream stream)
        {
            string content = null;

            if (stream != null)
                using (var sr = new StreamReader(stream))
                    content = await sr.ReadToEndAsync();
            return content;
        }

        private static T DeserializeJsonFromStream<T>(Stream stream)
        {
            if (stream == null || stream.CanRead == false)
                return default(T);

            using (var sr = new StreamReader(stream))
            using (var jtr = new JsonTextReader(sr))
            {
                var js = new JsonSerializer();
                var searchResult = js.Deserialize<T>(jtr);
                return searchResult;
            }
        }
    }
}
