using System;

namespace Classes
{
    public sealed class Bus : GroundTransport
    {
        public bool IsSchoolBus;

        public Bus() : base()
        {
            var random = TransportRandom.Random;
            this.IsSchoolBus = random.Next(0, 2) == 1;
            this.Speed = random.Next(30, 100);
            this.Capacity = random.Next(20, 90);
        }
    }
}
