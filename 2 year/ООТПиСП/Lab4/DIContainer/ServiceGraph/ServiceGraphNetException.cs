using System;

namespace DIContainer
{
    public sealed class ServiceGraphNetException : Exception
    {
        public ServiceGraphNetException(string message)
            : base(message) { }
    }
}