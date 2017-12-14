using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Lords.DataModel
{
    public class BattleBase
    {
        private static Random _random = new Random();
        private CancellationTokenSource _source = new CancellationTokenSource();

        public string Id { get; private set; }

        private int _health;
        public int Health
        {
            get { return _health; }
            protected set
            {
                _health = value > 0 ? value : 0;
                if (_health == 0)
                {
                    DeadEvent?.Invoke(this, DateTime.Now);
                }
            }
        }

        public int Hit { get; protected set; }
        public int NoArmorHurt { get; protected set; }
        public int Armor { get; protected set; }
        public double AttackInterval { get; protected set; }
        public event EventHandler<DateTime> DeadEvent;
        public event EventHandler<int> HurtEvent;

        public BattleBase(string id)
        {
            Id = id;
            Health = 100;
            Hit = 24;
            Armor = 20;
            NoArmorHurt = 0;
            AttackInterval = 1;
        }

        public bool IsAlive()
        {
            return Health > 0;
        }

        private readonly object _mutex = new object();
        private BattleBase _enemy = null;
      
        public bool HasAttackTarget
        {
            get { return _enemy != null; }
        }

        public void ResetEnemyTarget(BattleBase enemy)
        {
            if (_enemy == enemy)
                return;

            if (HasAttackTarget) StopAttack();
            ResetTarget(enemy);
        }

        public async Task AttackAsync(BattleBase enemy)
        {
            ResetEnemyTarget(enemy);

            var token = _source.Token;
            while (IsAlive() && enemy.IsAlive())
            {
                if (token != null) token.ThrowIfCancellationRequested();
                HurtEnemy(enemy);
                await Task.Delay((int)(AttackInterval * 1000));
            }

            ClearTarget();
        }

        public void StopAttack()
        {
            _source.Cancel();
        }

        public void AttackOnce(BattleBase enemy = null)
        {
            if (enemy != null)
            {
                ResetEnemyTarget(enemy);
            }

            if (HasAttackTarget)
            {
                HurtEnemy(_enemy);
                if (_enemy.IsAlive() == false)
                {
                    ClearTarget();
                }
            }
        }     

        #region private function
        private void ResetTarget(BattleBase enemy)
        {
            lock (_mutex) _enemy = enemy;
        }

        private void ClearTarget()
        {
            lock (_mutex) _enemy = null;
        }

        private void HurtEnemy(BattleBase enemy)
        {
            enemy.GotHurt(NoArmorHurt + CalNormalHurt(enemy.Armor));
        }

        private void GotHurt(int value)
        {
            HurtEvent?.Invoke(this, value);
            Health -= value;
        }

        protected int CalNormalHurt(int armor)
        {
            int result = Hit - _random.Next(armor);
            return result > 0 ? result : 0;
        }
        #endregion

    }
}
