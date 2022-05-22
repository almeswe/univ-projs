using System;

namespace Classes
{
    public abstract class Transport
    {
        public int Speed;
        public int Capacity;
        public Engine Engine;

        public Transport() =>
            this.Engine = new Engine();
    }

    public static class TransportRandom
    {
        public static Random Random { get; } = new Random();
    }
}
