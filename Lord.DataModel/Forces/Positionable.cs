using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Lords.DataModel
{
    public class Positionable
    {
        public Point Position { get; private set; }
        public Vector Orientation { get; private set; }

        private double _walkSpeed;
        public double WalkSpeed
        {
            get { return _walkSpeed; }
            set
            {
                _walkSpeed = value > 0 ? value : 0;
            }
        }

        public Positionable()
        {
            Position = new Point(0, 0, 0);
            Orientation = new Vector(1, 0, 0);
            _walkSpeed = 1.0;
        }


        public async Task WalkAsync(Point targetPosistion)
        {
            double step = 0.1;      // seconds

            // Turn orientation.
            Vector vector = Position.Orientation(targetPosistion);
            await TurnAsync(vector);

            // Walk
            while (Position.Distance(targetPosistion) > 0)
            {
                double distance = Position.Distance(targetPosistion);
                if (distance > WalkSpeed * step)
                {
                    await Task.Delay(TimeSpan.FromSeconds(step));
                    vector.Length = WalkSpeed * step;
                    Position = Position + vector;
                }
                else
                {
                    await Task.Delay(TimeSpan.FromSeconds(distance / WalkSpeed));
                    Position = targetPosistion;
                }
            }
        }

        public async Task WalkAsync(Point targetPosistion, CancellationToken token)
        {
            double step = 0.1;      // seconds

            // Turn orientation.
            Vector vector = Position.Orientation(targetPosistion);
            await TurnAsync(vector);

            // Walk
            while (Position.Distance(targetPosistion) > 0)
            {
                token.ThrowIfCancellationRequested();

                double distance = Position.Distance(targetPosistion);
                if (distance > WalkSpeed * step)
                {
                    await Task.Delay(TimeSpan.FromSeconds(step));
                    vector.Length = WalkSpeed * step;
                    Position = Position + vector;
                }
                else
                {
                    await Task.Delay(TimeSpan.FromSeconds(distance / WalkSpeed));
                    Position = targetPosistion;
                }
            }
        }

        public async Task TurnAsync(Vector orientaion)
        {
            await Task.Delay(100);
            Orientation = orientaion;
        }
    }
}
