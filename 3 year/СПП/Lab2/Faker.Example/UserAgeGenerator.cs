using System;
using Generator;

namespace Faker.Example
{
    public sealed class UserAgeGenerator : ValueGenerator
    {
        public UserAgeGenerator() : base(typeof(int)) { }

        public override object Generate(Type type, GeneratorContext context)
        {
            return _random.Next() % 70;
        }
    }
}
