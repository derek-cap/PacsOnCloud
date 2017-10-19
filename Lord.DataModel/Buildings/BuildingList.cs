using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace Lords.DataModel
{
    public class BuildingList
    {
        protected ConcurrentDictionary<int, Building> _collection = new ConcurrentDictionary<int, Building>();
        public BuildingList()
        {
        }

        public void AddOrUpdate(Building building)
        {
            _collection.AddOrUpdate(building.Id, building, (key, value) => building);
        }

        public override string ToString()
        {
            string result = string.Empty;
            foreach (var item in _collection.Keys)
            {
                result += $"{item} ";
            }
            return result;
        }
    }
}
