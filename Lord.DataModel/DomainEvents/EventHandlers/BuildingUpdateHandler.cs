using System;
using System.Collections.Generic;
using System.Text;

namespace Lords.DataModel
{
    public class BuildingUpdateHandler : IHandler<IDomainEvent>
    {
        public bool CanHandle(IDomainEvent eventType)
        {
            return eventType is LordUpdateBuilding;
        }

        public void Handle(IDomainEvent eventData)
        {
            LordUpdateBuilding data = eventData as LordUpdateBuilding;

        }
    }
}
