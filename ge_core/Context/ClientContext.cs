using ge_core.Base;
using ge_core.Coretypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ge_core.Context
{
    public class ClientContext
    {
        public Client GetClientDetails()
        {
            Client client = null;

            var clientData = PlatformContextAccessor.ClientData;

            if (clientData != null)
            {
                Console.WriteLine("session is present");
                client = clientData;
            }
            else
            {
                Console.WriteLine("session is not present, calling provider");
                client = DataProviderManager<ClientProviderBase>.Provider.GetClientDetails();
            }


            return client;
        }
    }
}
