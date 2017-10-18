using System;
using System.Collections.Generic;
using System.Text;

namespace Lords.DataModel
{
    class BuildRecord
    {
        public object Result { get; protected set; }
        public TimeSpan TimeNeed { get; protected set; }
        public string CastleId { get; protected set; }
    }
}
