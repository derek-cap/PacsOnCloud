using System;
using System.Collections.Generic;
using System.Text;

namespace Lords.DataModel
{
    public class Population : Resource
    {
        public Population(int number)
            : base(number)
        {
            _baseAcceleration = 10;
        }
    }
}
