using System;
using System.Collections.Generic;
using System.Text;

namespace Lords.DataModel
{
    public class Spear : BattleBase
    {
        public Spear(string id)
            : base(id)
        {
            AttackInterval = 0.7;
            Health = 200;
            Hit = 24;
            Armor = 20;
            NoArmorHurt = 0;
        }
    }

    public class SpearArmy : Army
    {
        public SpearArmy(string id)
            : base(id)
        {
            _maxCount = 60;
            RecruitRound = 1;
            InitializeBattleUnits(_maxCount);
        }

        protected override BattleBase NewBattleBase(string id)
        {
            string theId = "Spear " + id;
            return new Spear(theId);
        }
    }
}
