using SpaceSmileBrianKaddour.ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpaceSmileBrianKaddour.Web.Interfaces
{
    public interface ILaunchpadApiClient
    {
        Task<IEnumerable<LaunchPadInfo>> GetValues();
    }
}
