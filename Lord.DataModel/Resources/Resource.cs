using System;
using System.Threading.Tasks;

namespace Lords.DataModel
{
    public abstract class Resource
    {
        protected double _dValue;
        public int Value
        {
            get { return (int)_dValue; }
        }

        protected int _baseAcceleration;
        protected int _accApprec;

        public int Acceleration
        {
            get { return (int) (_baseAcceleration * (1 + _accApprec * 0.01)); }
        }

        public int MaxValue { get; protected set; }

        private readonly object _mutex = new object();

        public Resource(int orgin)
        {
            _dValue = orgin;
            _baseAcceleration = 1;
            _accApprec = 0;
        }

        public void SpreadCapacity(int number)
        {
            lock (_mutex)
            {
                MaxValue += number;
            }
        }

        public virtual void Add(int number)
        {
            lock (_mutex)
            {
                _dValue = _dValue + number;
            }
        }

        public void Consume(int number)
        {
            lock (_mutex)
            {
                _dValue = _dValue - number;
            }
        }

        public void AcceptAppreciation(Appreciation appreciation)
        {
            if (appreciation.ResourceType == GetType().Name.ToLower())
            {
                MaxValue += appreciation.Capacity;
                _accApprec += appreciation.Acceleration;
            }
        }

        public virtual void AutoUpdateTick()
        {
            lock (_mutex)
            {
                // Only increase when the value < MaxValue
                if (_dValue < MaxValue)
                {
                    double buffer = _dValue + _baseAcceleration * (1 + _accApprec * 0.01);
                    _dValue = buffer < MaxValue ? buffer : MaxValue;
                }
            }
        }

        public override string ToString()
        {
            return $"{Value}/{MaxValue}";
        }
    }
}
