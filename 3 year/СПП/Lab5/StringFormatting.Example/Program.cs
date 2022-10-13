using System;
using StringFormatting;

using System.Collections.Generic;

namespace StringFormatting.Example
{
    public sealed class User
    {
        public int Age;
        public string Name { get; set; }
        public List<string> Contacts { get; set; }
        public List<List<int>> y = new List<List<int>>() {
                new List<int>{ 1, 2, 3 }, new List<int> { 4, 5, 6 } };
        public User()
        {
            this.Age = 22;
            this.Name = "Alexey";
            this.Contacts = new List<string>()
            {
                "Vasya",
                "Petya",
                "Ivan"
            };
        }
    }

    public static class Program
    {
        private static void Main(string[] args)
        {
            var user = new User();
            while (true)
            {
                var input = Console.ReadLine();
                try
                {
                    Console.WriteLine(StringFormatter.Shared.Format(input, user));
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
    }
}
