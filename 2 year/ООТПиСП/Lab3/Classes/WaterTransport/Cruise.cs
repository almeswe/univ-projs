using System;

namespace Classes
{
    public sealed class Cruise : WaterTransport
    {
        public bool HasSwimmingPool;

        public Cruise() : base()
        {
            var random = TransportRandom.Random;
            this.HasSwimmingPool = random.Next(0, 2) == 1;
            this.Speed = random.Next(30, 60);
            this.Capacity = random.Next(40, 100);
        }
    }
}
