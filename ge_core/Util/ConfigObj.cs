using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ge_core.Util
{
    public class ConfigObj
    {
        public string CurrentSite { get; set; }

        public User User { get; set; }
    }

    public class User
    {
        public string UserName { get; set; }

        public Guid UUID => Guid.NewGuid();
    }
}
