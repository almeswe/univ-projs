namespace Faker.Exceptions
{
    public sealed class FakerUnhandledException : FakerException
    {
        public FakerUnhandledException(string message)
            : base(message) { }
    }
}
