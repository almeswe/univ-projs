using System;

namespace Faker.Analyzer.Exceptions
{
    public abstract class GraphNetException : Exception
    {
        public GraphNetException(string message) 
            : base(message) { }
    }
}
