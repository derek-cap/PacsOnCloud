using System;
using System.Collections.Generic;
using System.Text;

namespace Lords.DataModel
{
    interface IUpgradable
    {
        int Level { get; }
        int Experience { get; }

        void IncreaseExperience(int experience);
        void DecreaseExperience(int experience);

        void Upgrade(int level);
        void Downgrade(int level);

        event EventHandler<int> LevelUpEvent;
        event EventHandler<int> LevelDownEvent;
    }
}
