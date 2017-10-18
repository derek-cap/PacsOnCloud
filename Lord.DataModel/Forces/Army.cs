using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Lords.DataModel
{
    public abstract class Army
    {
        public int Count { get; protected set; }
        public int Power { get; protected set; }
        public int Defence { get; protected set; }

        protected int _perBuildTime;

        public int BuildTime
        {
            get { return _perBuildTime * Count; }
        }

        protected readonly object _mutex = new object();

        public void Add(int number)
        {
            lock (_mutex)
            {
                Count += number;
            }
        }

        public void Consume(int number)
        {
            lock (_mutex)
            {
                int result = Count - number;
                Count = (result > 0) ? result : 0;
            }
        }
    }
}
