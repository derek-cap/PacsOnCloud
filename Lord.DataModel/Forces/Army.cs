using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lords.DataModel
{
    public abstract class Army
    {
        public string Id { get; }

        private Random _random = new Random();
        private int _attackIntervalInMs;

        protected readonly List<BattleBase> _battleUnits = new List<BattleBase>();
        public IEnumerable<BattleBase> BattleUnits
        {
            get { return _battleUnits; }
        }

        private readonly object _mutex = new object();

        protected int _maxCount;
        public int Count
        {
            get { return BattleUnits.Count(); }
        }

        public event EventHandler<BattleBase> UnitDeadEvent;
        public event EventHandler<DateTime> DeadEvent;

        public int RecruitRound { get; protected set; }

        public bool IsAlive()
        {
            return Count > 0;
        }

        protected Army(string id)
        {
            Id = id;
        }

        protected void InitializeBattleUnits(int count)
        {
            if (count == 0)
                throw new Exception("The army count cannot be 0");

            int realCount = count < _maxCount ? count : _maxCount;
            for (int i = 0; i < realCount; i++)
            {
                var one = NewBattleBase(i.ToString());
                one.DeadEvent += OnUnitDead;
                _battleUnits.Add(one);
            }
            _attackIntervalInMs = (int)(_battleUnits.First().AttackInterval * 1000);
        }

        protected abstract BattleBase NewBattleBase(string id);

        public void Recovery()
        {
            if (_battleUnits.Count() < _maxCount)
            {

            }
        }

        public async Task AttackAsync(Army enemy)
        {
            //while (IsAlive() && enemy.IsAlive())
            //{
            //    foreach (var item in _battleUnits)
            //    {
            //        if (item.HasAttackTarget() == false)
            //        {
            //            var one = enemy.FindOne();
            //            item.AttackOnce(one);
            //        }
            //        else
            //        {
            //            item.AttackOnce();
            //        }
            //    }
            //    await Task.Delay(_attackIntervalInMs);
            //}
            IEnumerable<Task> tasksQuery = from u in _battleUnits
                                           select OneAttackAsync(u, enemy);
            Task[] tasks = tasksQuery.ToArray();
            await Task.WhenAll(tasks);
        }

        private async Task OneAttackAsync(BattleBase unit, Army enemy)
        {
            while (unit.IsAlive() && enemy.IsAlive())
            {
                var one = enemy.FindOne();
                if (one != null)
                {
                    await unit.AttackAsync(one);
                }
            }
        }

        private BattleBase FindOne()
        {
            lock (_mutex)
            {
                if (_battleUnits.Count() > 0)
                {
                    int index = _random.Next(_battleUnits.Count());
                    return _battleUnits.ElementAt(index);
                }
                return null;
            }
        }

        private void OnUnitDead(object sender, DateTime dateTime)
        {
            BattleBase one = sender as BattleBase;
            if (one != null)
            {
                UnitDeadEvent?.Invoke(this, one);
                lock(_mutex)
                {
                    if (_battleUnits.Contains(one))
                    {
                        _battleUnits.Remove(one);
                        one.DeadEvent -= OnUnitDead;
                    }                   
                }
                if (!IsAlive())
                {
                    DeadEvent?.Invoke(this, dateTime);
                }
            }
        }
    }
}
