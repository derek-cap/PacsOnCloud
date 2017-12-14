using Lords.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PacsOnCloud
{
    class LordTest
    {
        public static void TestBattleBase()
        {
            BattleBase battleBase1 = new Spear("A");
            BattleBase battleBase2 = new BattleBase("B");

            battleBase1.DeadEvent += (o, d) => Console.WriteLine($"{((BattleBase)o).Id} dead at {d}.------------");
            battleBase1.HurtEvent += (o, d) => Console.WriteLine($"{((BattleBase)o).Id} got hurt {d}.");

            battleBase2.DeadEvent += (o, d) => Console.WriteLine($"{((BattleBase)o).Id} dead at {d}.--------------");
            battleBase2.HurtEvent += (o, d) => Console.WriteLine($"{((BattleBase)o).Id} got hurt {d}.");

            Task t1 = battleBase1.AttackAsync(battleBase2);
            Task t2 = battleBase2.AttackAsync(battleBase1);

            Thread.Sleep(4000);
            battleBase1.StopAttack();
            battleBase2.StopAttack();

            Task.WhenAll(t1, t2).Wait();
        }

        public static void TestPower()
        {
            BattleBase battleBase1 = new Spear("Spear");
            BattleBase battleBase2 = new Sword("Sword");

            battleBase1.DeadEvent += (o, d) => Console.WriteLine($"{((BattleBase)o).Id} dead at {d}.------------");
            battleBase1.HurtEvent += (o, d) => Console.WriteLine($"{((BattleBase)o).Id} got hurt {d}.");

            battleBase2.DeadEvent += (o, d) => Console.WriteLine($"{((BattleBase)o).Id} dead at {d}.--------------");
            battleBase2.HurtEvent += (o, d) => Console.WriteLine($"{((BattleBase)o).Id} got hurt {d}.");

            Task t1 = battleBase1.AttackAsync(battleBase2);
            Task t2 = battleBase2.AttackAsync(battleBase1);
        }

        public static async Task TestArmy()
        {
            Army army1 = new SpearArmy("Spear army");
            Army army2 = new SwordArmy("Sword army");
            
            army1.DeadEvent += (o, d) => Console.WriteLine($"{((Army)o).Id} dead at {d}.------------");
            army1.UnitDeadEvent += (o, d) => Console.WriteLine($"{d.Id} in {((Army)o).Id} is dead");
            army2.DeadEvent += (o, d) => Console.WriteLine($"{((Army)o).Id} dead at {d}.------------");
            army2.UnitDeadEvent += (o, d) => Console.WriteLine($"----{d.Id} in {((Army)o).Id} is dead");

            army2.AttackAsync(army1);
            await army1.AttackAsync(army2);

            Action<Army> action = a =>
            {
                Console.WriteLine(a.Count);
               
                foreach (var item in a.BattleUnits)
                {
                    Console.Write($"{item.Id} ");
                }
            };

            action(army1);
            action(army2);
        }

        public static void TestPositionable()
        {
            Positionable poistion = new Positionable();
            Point targetPosition = new Point(12, 5, 0);

            Action action = async () =>
            {
                Console.WriteLine($"From {poistion.Position} walk to {targetPosition}");
                Task walk = poistion.WalkAsync(targetPosition);
                while (walk.IsCompleted == false)
                {
                    await Task.Delay(1000);
                    Console.WriteLine(poistion.Position);
                }
            };

            action();
        }


    }
}
