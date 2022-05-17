using DIContainer;

namespace DIContainerTest
{
    public class Program
    {
        public static void Main(string[] args)
        {
            TransientTest();
            System.Console.WriteLine();
            SingletonTest();
            System.Console.ReadLine();
        }

        public static void TransientTest()
        {
            var container = new ServiceContainer()
                .Register<IFont, Consolas>()
                .Register<IText, BoldText>()
                .Register<IPen, SolidPen>()
                .Register<Writer, Writer>();
            var writer = container.Resolve<Writer, Writer>();
            writer.Write($"Transient1 [{writer.GetHashCode()}]");
            writer = container.Resolve<Writer, Writer>();
            writer.Write($"Transient2 [{writer.GetHashCode()}]");
        }

        public static void SingletonTest()
        {
            var container = new ServiceContainer()
                .Register<IFont, Consolas>()
                .Register<IText, BoldText>()
                .Register<IPen, SolidPen>()
                .Register<Writer, Writer>(ServiceLifetime.Singleton);
            var writer = container.Resolve<Writer, Writer>();
            writer.Write($"Singleton1 [{writer.GetHashCode()}]");
            writer = container.Resolve<Writer, Writer>();
            writer.Write($"Singleton2 [{writer.GetHashCode()}]");
        }
    }
}
