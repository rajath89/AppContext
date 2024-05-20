using ge_core.Coretypes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ge_core.Util
{
    public static class Util
    {
        public static ConfigObj GetConfigObj()
        {
            //Assembly pluginAssembly = Assembly.LoadFrom("ge_ustproviders, Version=0.0.0.0, Culture=neutral, PublicKeyToken=17242887c5aa9d81");


            var jsonData = File.ReadAllText(Constants.ConfigFilePath);
            return JsonConvert.DeserializeObject<ConfigObj>(jsonData);
        }
    }
}
