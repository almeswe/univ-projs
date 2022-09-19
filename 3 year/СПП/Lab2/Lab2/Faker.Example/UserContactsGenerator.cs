using System;
using System.Collections.Generic;

using Generator;

namespace Faker.Example
{
    public sealed class UserContactsGenerator : ValueGenerator
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

        public UserContactsGenerator() : base(typeof(IList<string>)) { }

        public override object Generate(Type type, GeneratorContext context)
        {
            var contacts = new List<string>();
            var size = (int)context.Faker.Create<byte>(context.Rules);
            for (int i = 0; i < size; i++)
                contacts.Add(this.Names[context.Faker.Create<int>(context.Rules) %this.Names.Count]);
            return contacts;
        }
    }
}
