using System;
using System.Threading.Tasks;

namespace Lords.DataModel
{
    public abstract class Resource
    {
        public int Value { get; protected set; }
        public int Acceleration { get; protected set; }

        private readonly object _mutex = new object();

        public Resource(int orgin)
        {
            Value = orgin;
            Acceleration = 1;
        }

        public virtual void Add(int number)
        {
            lock (_mutex)
            {
                Value = Value + number;
            }
        }

        public void Consume(int number)
        {
            lock (_mutex)
            {
                Value = Value - number;
            }
        }

        public virtual void AutoUpdateTick()
        {
            lock (_mutex)
            {
                Value = Value + Acceleration;
            }
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
