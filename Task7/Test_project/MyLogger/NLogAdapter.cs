using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLogger
{
    class NLogAdapter:ILogger
    {
        private Logger _logger;

        public NLogAdapter()
        {
            _logger = LogManager.GetCurrentClassLogger();
        }

        public void Log(LogLevel level, string message)
        {
            NLog.LogLevel Nlevel;
            switch (level)
            {
                case LogLevel.Debug:
                    Nlevel = NLog.LogLevel.Debug;
                    break;
                case LogLevel.Info:
                    Nlevel = NLog.LogLevel.Info;
                    break;
                case LogLevel.Warn:
                    Nlevel = NLog.LogLevel.Warn;
                    break;
                case LogLevel.Error:
                    Nlevel = NLog.LogLevel.Error;
                    break;
                case LogLevel.Fatal:
                    Nlevel = NLog.LogLevel.Fatal;
                    break;
                default:
                    Nlevel = NLog.LogLevel.Off;
                    break;
            }
            _logger.Log(Nlevel, message);
        }
        public void Log(LogLevel level, string message,params object[] args)
        {
            Log(level, string.Format(message, args));
        }
    }
}
