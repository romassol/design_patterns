using System;

namespace homework_8
{
    public interface IVisitor
    {
        void Visit(Triangle figure);
        void Visit(Rectangle figure);
        void Visit(Circle figure);
    }
    
    public class DrawVisitor : IVisitor
    {

        public void Visit(Triangle figure)
        {
            Console.WriteLine($"Draw three lines from point ({figure.X}, {figure.Y})");
        }

        public void Visit(Rectangle figure)
        {
            Console.WriteLine($"Draw four lines from point ({figure.X}, {figure.Y})");
        }

        public void Visit(Circle figure)
        {
            Console.WriteLine($"Draw closed line without corner from point ({figure.X}, {figure.Y})");
        }
    }
    
    public class GetAreaVisitor : IVisitor
    {

        public void Visit(Triangle figure)
        {
            Console.WriteLine("0,5*a*h");
        }

        public void Visit(Rectangle figure)
        {
            Console.WriteLine("a*b");
        }

        public void Visit(Circle figure)
        {
            Console.WriteLine("pi*r^2");
        }
    }
    
    public class GetPerimeterVisitor : IVisitor
    {

        public void Visit(Triangle figure)
        {
            Console.WriteLine("a+b+c");
        }

        public void Visit(Rectangle figure)
        {
            Console.WriteLine("a*2+b*2");
        }

        public void Visit(Circle figure)
        {
            Console.WriteLine("2*pi*r");
        }
    }

    public abstract class Figure
    {
        public abstract string Name { get; set; }
        public abstract int X { get; set; }
        public abstract int Y { get; set; }
        public abstract void Accept(IVisitor visitor);
    }
    public class Triangle: Figure
    {
        public override string Name { get; set; }
        public override int X { get; set; }
        public override int Y { get; set; }

        public Triangle(int x, int y)
        {
            X = x;
            Y = y;
            Name = "triangle";
        }
        public override void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }

    public class Rectangle: Figure
    {
        public override string Name { get; set; }
        public override int X { get; set; }
        public override int Y { get; set; }

        public Rectangle(int x, int y)
        {
            X = x;
            Y = y;
            Name = "rectangle";
        }
        public override void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }

    public class Circle: Figure
    {
        public override string Name { get; set; }
        public override int X { get; set; }
        public override int Y { get; set; }

        public Circle(int x, int y)
        {
            X = x;
            Y = y;
            Name = "circle";
        }
        public override void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}