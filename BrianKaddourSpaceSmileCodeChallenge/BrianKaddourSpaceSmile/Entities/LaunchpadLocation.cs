using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpaceSmileBrianKaddour.ApplicationCore.Entities
{
    public class LaunchpadLocation
    {
        public string Name { get; set; }
        public string Region { get; set; }
        public float? Latitude { get; set; }
        public float? Longitude { get; set; }
    }
}
