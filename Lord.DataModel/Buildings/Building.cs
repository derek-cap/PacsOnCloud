using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Lords.DataModel
{
    public abstract class Building
    {
        public int Id { get; protected set; }
        public int Level { get; protected set; }
        public string Name { get; protected set; }

        protected Collection<Appreciation> _appreciations = new Collection<Appreciation>();
        public ICollection<Appreciation> AppreciationList { get { return _appreciations; } } 

        public Building()
        {
            UpdateAppreciaitons();
        }

        public void LevelUp()
        {
            Level++;
        }

        protected virtual void UpdateAppreciaitons()
        {

        }
    }
}
