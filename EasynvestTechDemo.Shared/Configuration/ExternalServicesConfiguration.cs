using System;
using System.Collections.Generic;
using System.Text;

namespace EasynvestTechDemo.Shared.Configuration
{
    public class ExternalServicesConfiguration
    {
        public IDictionary<string, ExternalServiceConfiguration> Services { get; set; }

        public ExternalServiceConfiguration GetConfiguration(string service)
        {
            if (!Services.TryGetValue(service, out ExternalServiceConfiguration serviceConfiguration))
                throw new Exception($"Missing Configuration for {service} service");

            return serviceConfiguration;
        }
    }

    public class ExternalServiceConfiguration
    {
        public string Uri { get; set; }
    }
}
