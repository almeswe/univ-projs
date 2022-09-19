using System;

using Faker.Rules;

namespace Faker.Abstractions
{
    public interface IFaker
    {
        T Create<T>(FakerRules rules = null);
        object Create(Type type, FakerRules rules = null);
    }
}
