using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLogger
{
    public enum LogLevel
    {Debug,Info,Warn,Error,Fatal}

    public interface ILogger
    {
        void Log(LogLevel level, string message);
        void Log(LogLevel level, string message, params object[] args);
    }
}
