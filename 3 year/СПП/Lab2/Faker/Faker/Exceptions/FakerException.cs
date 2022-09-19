using System;

namespace Faker.Exceptions
{
    public abstract class FakerException : Exception
    {
        public FakerException(string message) 
            : base(message) { }
    }
}
