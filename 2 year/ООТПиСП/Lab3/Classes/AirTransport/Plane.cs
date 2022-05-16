using System;

namespace Classes
{
    public sealed class Plane : AirTransport
    {
        public int FlyingHeight;

        public Plane() : base()
        {
            var random = TransportRandom.Random;
            this.FlyingHeight = random.Next(4000, 30000);
            this.Speed = random.Next(200, 800);
            this.Capacity = random.Next(4, 100);
        }
    }
}
