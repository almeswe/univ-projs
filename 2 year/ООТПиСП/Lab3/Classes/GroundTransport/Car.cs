using System;

namespace Classes
{
    public sealed class Car : GroundTransport
    {
        public bool IsElectrical;

        public Car() : base()
        {
            var random = TransportRandom.Random;
            this.IsElectrical = random.Next(0, 2) == 1;
            this.Speed = random.Next(60, 201);
            this.Capacity = random.Next(4, 9);
        }
    }
}
