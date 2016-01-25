using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;

namespace ZF.Log
{
    public class LogHelper
    {
        private static readonly ILog log = LogManager.GetLogger("Log");

        public static void WriteInfo(object obj)
        {
            log.Info(obj);
        }

        public static void WriteError(object obj)
        {
            log.Error(obj);
        }

        public static void WriteDebug(object obj)
        {
            log.Debug(obj);
        }

        public static void WriteWarn(object obj)
        {
            log.Warn(obj);
        }

        public static void Configure()
        {
            log4net.Config.XmlConfigurator.Configure();
        }
    }
}
