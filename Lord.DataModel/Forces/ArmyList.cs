using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lords.DataModel
{
    public class ArmyList
    {
        protected ConcurrentDictionary<Type, Army> _collection = new ConcurrentDictionary<Type, Army>();
        public ArmyList()
        {
        }

        public void Add(Army army)
        {
            Type type = army.GetType();
            if (_collection.ContainsKey(type))
            {
                _collection[type].Add(army.Count);
            }
            else
            {
                _collection[type] = army;
            }
        }

        public override string ToString()
        {
            var query = from r in _collection.Values
                        select r.Count;

            return query.Sum().ToString();
        }

    }
}
