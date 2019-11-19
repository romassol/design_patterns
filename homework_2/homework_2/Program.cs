using System.Net;

namespace homework_2
{
    public interface ICarFactory
    {
        ICarcass CreateCarcass();
        IEngine CreateEngine();
        ICabin CreateCabin();
    }

    public interface ICabin
    {
        string Material { get; }
    }

    public interface IEngine
    {
        int Power { get; }
        string Class { get; }
    }

    public interface ICarcass
    {
        string Material { get; }
        string Color { get; }
    }

    public class Cabin : ICabin
    {
        public Cabin(string material)
        {
            Material = material;
        }

        public string Material { get; }
    }

    public class Engine : IEngine
    {
        public Engine(int power, string @class)
        {
            Power = power;
            Class = @class;
        }

        public int Power { get; }
        public string Class { get; }
    }

    public class Carcass : ICarcass
    {
        public Carcass(string material, string color)
        {
            Material = material;
            Color = color;
        }

        public string Material { get; }
        public string Color { get; }
    }

    public class BMW : ICarFactory
    {
        public ICarcass CreateCarcass()
        {
            return new Carcass("iron", "blue");
        }

        public IEngine CreateEngine()
        {
            return new Engine(500, "5B");
        }

        public ICabin CreateCabin()
        {
            return new Cabin("skin");
        }
    }

    public class AUDI : ICarFactory
    {
        public ICarcass CreateCarcass()
        {
            return new Carcass("iron", "yellow");
        }

        public IEngine CreateEngine()
        {
            return new Engine(550, "6H");
        }

        public ICabin CreateCabin()
        {
            return new Cabin("skin");
        }
    }

    internal class Program
    {
        public static void Main(string[] args)
        {
        }
        public static void AbstractFactoryExample()
        {
            var bmw = new BMW();
            
            var bmwEngine = bmw.CreateEngine();
            var bmwCarcass = bmw.CreateCarcass();
            var bmwCabin = bmw.CreateCabin();
            
            
            var audi = new AUDI();
            
            var audiEngine = audi.CreateEngine();
            var audiCarcass = audi.CreateCarcass();
            var audiCabin = audi.CreateCabin();
        }
    }
}