using System;
using System.Collections.Generic;
using System.Text;

namespace Lords.DataModel
{
    public class LordUpdateBuilding : IDomainEvent
    {
        public Building Building { get; private set; }
        public Lord Lord { get; private set; }

        public LordUpdateBuilding(Building building, Lord lord)
        {
            Building = building;
            Lord = lord;
        }
    }
}
