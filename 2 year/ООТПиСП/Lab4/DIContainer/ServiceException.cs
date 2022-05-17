using System;

namespace DIContainer
{
    public sealed class ServiceException : Exception
    {
        public ServiceException(string message) 
            : base(message) { } 
    }
}