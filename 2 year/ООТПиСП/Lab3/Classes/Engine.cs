using System;

namespace Classes
{
    public sealed class Engine
    {
        public int Weight;
        public int Volume;

        public Engine()
        {
            var random = TransportRandom.Random;
            this.Weight = random.Next(100, 2000);
            this.Volume = random.Next(200, 3000);
        }
    }
}