using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Lords.DataModel
{
    public class Castle
    {
        public string Name { get; protected set; }
        public string Id { get; protected set; }
        public People People { get; protected set; }

        public ArmyList ArmyList { get; protected set; }

        public Castle(string id)
        {
            this.Id = id;
            Name = id;
            People = new People(1000);
            ArmyList = new ArmyList();
        }

        public void ResetName(string name)
        {
            Name = name;
        }

        public void Add(Army army)
        {
            ArmyList.Add(army);
        }

        public void AutoUpdateTick()
        {
            People.AutoUpdateTick();
        }

        public override string ToString()
        {
            return $"Castle: {Id}, {Name} People: {People}, Army: {ArmyList}";
        }

    }
}
