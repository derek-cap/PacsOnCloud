using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Lords.DataModel
{
    public class Lord : UpgradeBase
    {
        public string Id { get; protected set; }

        public Food Food { get; protected set; }
        public Gold Gold { get; protected set; }
        public Iron Iron { get; protected set; }
        public Wood Wood { get; protected set; }
        public Stone Stone { get; protected set; }

        public Castle Castle { get; protected set; }

        // Resource increase timer.
        private readonly Timer _timer;

        public Lord(string id, string castleId)
            : base(int.MaxValue, int.MaxValue)
        {
            Id = id;
            Level = 1;

            Food = new Food(100);
            Gold = new Gold(100);
            Iron = new Iron(100);
            Wood = new Wood(100);
            Stone = new Stone(100);
            
            Castle = new Castle(castleId, this);
            _timer = new Timer(new TimerCallback(ResourceTickPerSecond), null, 0, 1000);

            LevelUpEvent += new EventHandler<int>((o, level) => { Console.WriteLine($"Congratulation! You level up to {Level}"); });
            LevelDownEvent += new EventHandler<int>((o, level) => { Console.WriteLine($"Too bad! You level fall to {Level}"); });
        }

        public void AcceptAppreciation(Appreciation appreciation)
        {
            foreach(var item in AllResouce())
            {
                item.AcceptAppreciation(appreciation);
            }
        }

        public void ResourceTickPerSecond(object o)
        {
            foreach (var item in AllResouce())
            {
                item.TickPerSecond();
            }
            Castle.TickPerSecond();

            //Console.WriteLine(this);
            //Console.WriteLine();
        }

        private IEnumerable<Resource> AllResouce()
        {
            yield return Food;
            yield return Gold;
            yield return Iron;
            yield return Wood;
            yield return Stone;
        }

        public override string ToString()
        {
            return $"Lord {Id} {Castle}\nFood: {Food}, Gold: {Gold}, Iron: {Iron}, Wood: {Wood}, Stone: {Stone}";
        }
    }
}
