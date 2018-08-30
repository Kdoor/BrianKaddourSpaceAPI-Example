using System;

namespace SpaceSmileBrianKaddour.ApplicationCore.Exceptions
{
    public class LaunchpadNotFoundException : Exception
    {
        public LaunchpadNotFoundException(int launchpadId) : base($"No launchpad found with id {launchpadId}")
        {
        }

        protected LaunchpadNotFoundException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context)
        {
        }

        public LaunchpadNotFoundException(string message) : base(message)
        {
        }

        public LaunchpadNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
