using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ge_core.Coretypes
{
    public class RetrieveProfileResponse
    {
        public ResponseStatus ResponseStatus { get; set; }
    }

    public enum ResponseStatus
    {
        Success,
        Failure
    }
}
