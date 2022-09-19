using System;
using System.Collections.Generic;

using Generator;

namespace Faker.Example
{
    public sealed class UserNameGenerator : ValueGenerator
    {
        private readonly List<string> Names = new List<string>()
        {
            "Liam",
            "Noah",
            "Oliver",
            "Elijah",
            "James",
            "William",
            "Benjamin",
            "Lucas",
            "Henry",
            "Theodore",
            "Jack",
            "Levi",
            "Alexander",
            "Jackson",
            "Mateo",
            "Daniel",
            "Michael",
            "Mason",
            "Sebastian",
            "Ethan",
            "Logan",
            "Owen",
            "Samuel",
            "Jacob",
            "Asher",
            "Aiden",
            "John",
            "Joseph",
            "Wyatt",
            "David",
            "Leo",
            "Luke",
            "Julian",
            "Hudson",
            "Grayson",
            "Matthew",
            "Ezra",
            "Gabriel",
            "Carter",
            "Isaac"
        };

        public UserNameGenerator() : base(typeof(string)) { }

        public override object Generate(Type type, GeneratorContext context) =>
            this.Names[context.Faker.Create<int>(context.Rules) % this.Names.Count];
    }
}
