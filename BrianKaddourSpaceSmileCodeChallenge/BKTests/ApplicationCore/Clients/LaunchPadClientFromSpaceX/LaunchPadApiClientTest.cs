using SpaceSmileBrianKaddour.Web.Interfaces;
using Microsoft.Extensions.Configuration;
using SpaceSmileBrianKaddour.ApplicationCore.Entities;
using SpaceSmileBrianKaddour.ApplicationCore.Exceptions;
using SpaceSmileBrianKaddour.ApplicationCore.Interfaces;

using Moq;
using System;
using Xunit;
using SpaceSmileBrianKaddour.ApplicationCore.Clients;

namespace SpaceSmileBrianKaddour.Clients.LaunchPadApiClientTest
{
    public class LaunchPadsExist
    {
        private int _invalidId = -1;
        private Mock<ILaunchpadApiClient> _mocLaunchPadApiClient;

        public LaunchPadsExist()
        {
            _mocLaunchPadApiClient = new Mock<ILaunchpadApiClient>();
        }

        //Would need more tests than this, very basic API test
        [Fact]
        public void LaunchPadClientShouldRecieveInfo()
        {
            var launchPadClientTest = new LaunchPadInfoClient(null, null, null);

            //TODO: Consider if this is valid still after DB
            var result = launchPadClientTest.GetValues();


            //should avoid ToString here probably but this is definitely a prototype
            Assert.NotEmpty(result.ToString());
        }

    }
}
