using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Lords.DataModel
{
    public abstract class Building
    {
        protected int _maxLevel;

        public int Id { get; protected set; }
        public int Level { get; protected set; }
        public string Name { get; protected set; }

        protected Collection<Appreciation> _appreciations = new Collection<Appreciation>();
        public ICollection<Appreciation> AppreciationList { get { return _appreciations; } } 

        public Building()
        {
            _maxLevel = 10;
            UpdateAppreciaitons();
        }

        public void LevelUp()
        {
            if (Level < _maxLevel)
            {
                Level++;
            }
        }

        public bool CanLevelUp()
        {
            return Level < _maxLevel;
        }

        protected virtual void UpdateAppreciaitons()
        {

        }
    }
}
