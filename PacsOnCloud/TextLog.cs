using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dicom.Log;

namespace PacsOnCloud
{
    public class TextLog : ILog
    {
        public TextLog()
        {

        }

        public void Log(LogLevel level, string msg)
        {
            throw new NotImplementedException();
        }
    }
}
