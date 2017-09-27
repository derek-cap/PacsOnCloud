using Dicom.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacsOnCloud
{
    interface ILog
    {
        void Log(LogLevel level, string msg);
    }
}
