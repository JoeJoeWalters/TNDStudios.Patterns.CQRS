using System;
using System.Collections.Generic;
using System.Text;

namespace TNDStudios.Patterns.CQRS.Service.API
{
    public static class Constants
    {
#warning Not happy about this but is needed for now as it slightly breaks the principle of Dependency Inversion but for now it will do
        /// <summary>
        /// Locations of the blobs that will trigger the services (this would be a service bus topic
        /// with subscribers in a non-local emulated implementation)
        /// </summary>
        public const String EUTriggerPath = "searches/eu/outstanding/{name}";
        public const String UKTriggerPath = "searches/uk/outstanding/{name}";
        public const String InternationalTriggerPath = "searches/international/outstanding/{name}";
    }
}
