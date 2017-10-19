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

        public Lord Lord { get; private set; }
        public ArmyList ArmyList { get; protected set; }
        private BuildingList _buildingList;


        public Castle(string id, Lord lord)
        {
            this.Id = id;
            Name = id;
            People = new People(1000);

            Lord = lord;
            ArmyList = new ArmyList();
            _buildingList = new BuildingList();
            AddNewBuilding(new Palace());
        }

        public void ResetName(string name)
        {
            Name = name;
        }

        public void AddNewBuilding(Building building)
        {
            _buildingList.AddOrUpdate(building);
            foreach (var item in building.AppreciationList)
            {
                People.AcceptAppreciation(item);
                Lord.AcceptAppreciation(item);
            }
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
