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
        private readonly CoreWinSubLog.MinLogger _logger;

        public MinLogger(string ipAddress)
        {
            _logger = new CoreWinSubLog.MinLogger("127.0.0.1", "C:\\Temp\\Log");
        }

        public override void Log(LogLevel level, string msg, params object[] args)
        {
            string levelString = level.ToString();
            CoreWinSubLog.LogLevel theLevel = CoreWinSubLog.LogLevel.Debug;
            Enum.TryParse(levelString, out theLevel);
            _logger.Log(theLevel, msg, args);
        }
    }
}
