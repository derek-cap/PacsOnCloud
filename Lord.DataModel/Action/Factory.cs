using System;
using System.Collections.Generic;
using System.Text;

namespace Lords.DataModel
{
    class Factory
    {
        public string NewCastleId()
        {
            return new Random().Next(1000).ToString();
        }

        public void Build(Army army)
        {

        }
    }
}
