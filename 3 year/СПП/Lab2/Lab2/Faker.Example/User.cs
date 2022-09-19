using System.Collections.Generic;

namespace Faker.Example
{
    public sealed class User
    {
        public int Age { get; set; }
        public string Name { get; set; }
        public List<string> Contacts { get; set; }
        public short FavoriteNumber { get; set; }
        public string CipheredPassword { get; set; }

        public User(string name) =>
            this.Name = name;

        public User(string name, int age)
        {
            this.Age = age;
            this.Name = name;
        }
    }
}
