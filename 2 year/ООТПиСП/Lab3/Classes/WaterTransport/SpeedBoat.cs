using System;

namespace Classes
{
    public sealed class SpeedBoat : WaterTransport
    {
        public int CountOfPirates;

        public SpeedBoat() : base()
        {
            var random = TransportRandom.Random;
            this.CountOfPirates = random.Next(1, 10);
            this.Speed = random.Next(30, 100);
            this.Capacity = random.Next(4, 10);
        }
    }
}
