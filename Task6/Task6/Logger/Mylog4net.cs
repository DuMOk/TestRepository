using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using log4net;
using log4net.Config;

namespace Task6
{
    public class Mylog4net : ILogger
    {
        ILog _log = LogManager.GetLogger(typeof(Program));
        
        public void Log(string message)
        {
            log4net.Config.XmlConfigurator.Configure();
            _log.Error(message.ToString());
        }
    }
}
