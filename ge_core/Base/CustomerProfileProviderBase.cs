using ge_core.Coretypes;
using ge_core.DataClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ge_core.Base
{
    public abstract class CustomerProfileProviderBase : BaseDataProvider
    {
        public abstract RetrieveProfileResponse RetrieveProfile();
    }
}
