using ge_core.Base;
using ge_core.Coretypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ge_mymproviders
{
    public class ClientProvider : ClientProviderBase
    {
        public override Client GetClientDetails()
        {
            Client client = null;

            var data = PlatformContextAccessor.ClientData;

            if (data != null)
            {
                client = data;
            }
            else
            {
                var profile = DataProviderManager<CustomerProfileProviderBase>.Provider.RetrieveProfile();
                if (profile != null && profile.ResponseStatus == ResponseStatus.Success)
                {
                    client = PlatformContextAccessor.ClientData;
                }
            }

            return client;
        }
    }
}
