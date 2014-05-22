using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NLog;

namespace Task6
{
    public class MyNLog : ILogger
    {
        private Logger _myLog = LogManager.GetCurrentClassLogger();

        public void Log(string message)
        {
            _myLog.Error(message);
        }
    }
}
