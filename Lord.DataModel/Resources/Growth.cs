using System;
using System.Collections.Generic;
using System.Text;

namespace Lords.DataModel
{
    public class Growth
    {
        public Type ResourceType { get; }

        protected int _baseAcceleration;
        protected int _accApprec;

        public int Acceleration
        {
            get { return (int)(_baseAcceleration * (1 + _accApprec * 0.01)); }
        }

        public Growth(Type resourceType, int baseAcc, int accApprec)
        {
            ResourceType = resourceType;
            _baseAcceleration = baseAcc;
            _accApprec = accApprec;
        }

        public void Superpose(Growth growth)
        {
            if (growth.ResourceType == ResourceType)
            {
                _baseAcceleration += growth._baseAcceleration;
                _accApprec += growth._accApprec;
            }
        }
    }
}
