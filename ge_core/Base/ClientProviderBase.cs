using ge_core.Coretypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ge_core.Base
{
    public abstract class ClientProviderBase : BaseDataProvider
    {
        protected ClientProviderBase() { }

        public abstract Client GetClientDetails();
    }
}
