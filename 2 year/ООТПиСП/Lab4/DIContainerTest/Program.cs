using DIContainer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIContainerTest
{
    public interface ISmthElse
    {

    }

    public class SmthElseImpl : ISmthElse
    {
        public override string ToString() =>
            "SmthElseImpl1";
    }

    public interface IWriter
    {
        void Write(string message);
    }

    public class Writer1 : IWriter
    {
        private ISmthElse _smthElse;

        public Writer1(SmthElseImpl smthelse) =>
            this._smthElse = smthelse;

        public void Write(string message) =>
            Console.WriteLine($"1: {this._smthElse}>{message}");
    }

    public class Writer2 : IWriter
    {
        public void Write(string message) =>
            Console.WriteLine($"2: {message}");
    }

    public class MyClass
    {
        private IWriter _writer;

        public MyClass(IWriter writer) =>
            this._writer = writer;

        public void DoSmth(string message) =>
            this._writer.Write(message);
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            var container = new ServiceContainer();
            container.Register<IWriter, Writer1>()
                    .Register<ISmthElse, SmthElseImpl>();
            var myClass = container.Resolve<MyClass>();
            var myClass2 = container.Resolve<MyClass>();
            myClass.DoSmth("Smth");
            myClass2.DoSmth("Smth2");
        }
    }
}
