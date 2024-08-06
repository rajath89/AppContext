using ConsoleApp1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var testc = ServiceProviderManager<EmployeeServiceProviderBase>.Provider;
            var resp = testc.GetDataUsingDataContract(null);
        }
    }
}
