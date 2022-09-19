using Faker.Rules;

using Generator.Default;

using System.Collections.Generic;

namespace Faker.Example
{
    public static class Program
    {
        private static void Main(string[] args)
        {
            var faker = new Faker();
            var rules = new FakerRules();
            rules.AddPropRule<User, int, UserAgeGenerator>(user => user.Age);
            rules.AddPropRule<User, string, UserNameGenerator>(user => user.Name);
            rules.AddPropRule<User, List<string>, UserContactsGenerator>(user => user.Contacts);
            rules.AddTypeRule<List<User>, ListGenerator>();
            rules.AddTypeRule<List<string>, ListGenerator>();
            var a = faker.Create<List<User>>(rules);
        }
    }
}
