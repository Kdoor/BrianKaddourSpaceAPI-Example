using SpaceSmileBrianKaddour.ApplicationCore.Entities;
using SpaceSmileBrianKaddour.Web.Extensions;
using System.Collections.Generic;

namespace BrianKaddourSpaceSmile
{
    public interface ILaunchpadService
    {
        List<LaunchPad> filterLaunchPadModels(string filteringParams, IEnumerable<LaunchPad> launchPadsIn );
    }
}