using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task6
{
    public class MyLogger : ILogger
    {
        public void Log(string message)
        {
            Console.WriteLine("{0}", message);
        }
    }
}
