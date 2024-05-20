using ge_core.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ge_core.Base
{
    public class Session
    {
        public string SessionId { get; set; }

        public User User { get; set; }

        public string Site { get; set; }
    }
}
