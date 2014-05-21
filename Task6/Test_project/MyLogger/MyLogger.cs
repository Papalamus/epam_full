using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLogger
{
    class MyLogger : ILogger
    {
        private string _path;

        public MyLogger(string path)
        {
            _path = path;
        }

        public void Log(LogLevel level, string message)
        {
            File.AppendText(_path).WriteLine("LogLevel :{0}, Error:{1}",level,message);
        }

        public void Log(LogLevel level, string message, params object[] args)
        {
            Log(level, string.Format(message, args));
        }
    }
}
