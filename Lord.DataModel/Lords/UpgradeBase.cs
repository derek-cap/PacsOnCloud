using System;
using System.Collections.Generic;
using System.Text;

namespace Lords.DataModel
{
    public class UpgradeBase : IUpgradable
    {
        protected int _MaxLevel;
        protected int _MaxExperience;

        public int Level { get; protected set; }

        public int Experience { get; protected set; }

        public event EventHandler<int> LevelUpEvent;
        public event EventHandler<int> LevelDownEvent;

        public UpgradeBase(int maxLevel, int maxExperience)
        {
            _MaxLevel = maxLevel;
            _MaxExperience = maxExperience;
        }

        public void DecreaseExperience(int experience)
        {
            int reExp = Experience - experience;
            Experience = reExp > 0 ? reExp : 0;
            while (Experience < GetLevelExperience(Level - 1))
            {
                Level--;
                LevelDownEvent?.Invoke(this, Level);
            }
        }

        public void Downgrade(int level)
        {
            while (level > 0)
            {
                int removeExp = CurrentLevelExperienceRange();
                DecreaseExperience(removeExp);
                level--;
            }
        }

        public void IncreaseExperience(int experience)
        {
            int reExp = Experience + experience;
            Experience = reExp < _MaxExperience ? reExp : _MaxExperience;
            while (Experience > GetLevelExperience(Level))
            {
                Level++;
                LevelUpEvent?.Invoke(this, Level);
            }
        }

        public void Upgrade(int level)
        {
            while (level > 0)
            {
                int addExp = CurrentLevelExperienceRange();
                IncreaseExperience(addExp);
                level--;
            }
        }

        protected virtual int GetLevelExperience(int level)
        {
            return 100 + 2 * level * level * level * level;
        }

        private int CurrentLevelExperienceRange()
        {
            return GetLevelExperience(Level) - GetLevelExperience(Level - 1);
        }
    }
}
