using Dicom.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacsOnCloud
{
    public class MinLogManager : LogManager
    {
        private readonly Logger _loggerImpl;

        public MinLogManager(string ipAddress)
        {
            _loggerImpl = new MinLogger(ipAddress);
        }

        protected override Logger GetLoggerImpl(string name)
        {
            return _loggerImpl;
        }
    }

    public class MinLogger : Logger
    {
        readonly TcpLog _tcpLog;
        readonly TextLog _textLog;

        public MinLogger(string ipAddress)
        {
            _tcpLog = new TcpLog(ipAddress);
            _textLog = new TextLog();
        }

        public override void Log(LogLevel level, string msg, params object[] args)
        {
            string theMessage = string.Format(NameFormatToPositionalFormat(msg), args);

            ILog usedOne = GetUsedLog();
            usedOne.Log(level, theMessage);
        }

        private ILog GetUsedLog()
        {
            if (_tcpLog.IsConnected())
            {
                return _tcpLog;
            }
            else
            {
                return _textLog;
            }
        }
    }
}
