using System;

namespace Classes
{
    public sealed class Helicopter : AirTransport
    {
        public int Blades;

        public Helicopter() : base()
        {
            var random = TransportRandom.Random;
            this.Blades = random.Next(4, 6);
            this.Speed = random.Next(30, 100);
            this.Capacity = random.Next(4, 7);
        }
    }
}
