using Newtonsoft.Json;
using System.Collections.Generic;

namespace SpaceSmileBrianKaddour.ApplicationCore.Entities
{
    //Don't expose this out, use for
    public class LaunchPadInfo : BaseEntity
    {
            new public string Id { get; set; }

            //Naming is odd
            [JsonProperty("full_name")]
            public string FullName { get; set; }

            public string Status { get; set; }
            public LaunchpadLocation Location { get; set; }

            [JsonProperty("vehicles_launched")]
            public List<string> VehiclesLaunched { get; set; }

            public string Details { get; set; }

    }
}
