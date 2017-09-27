using Dicom.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacsOnCloud
{
    public class TcpLog : ILog
    {
        public TcpLog(string ipAdress)
        {

        }

        public void Log(LogLevel level, string msg)
        {
            Console.WriteLine($"TcpLog get log: [{level}] {msg}");
        }

        public bool IsConnected()
        {
            return true;
        }
    }
}
