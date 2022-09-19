namespace Faker.Exceptions
{
    public sealed class FakerBadConstructorException : FakerException
    {
        public FakerBadConstructorException(string message)
            : base(message) { }
    }
}
