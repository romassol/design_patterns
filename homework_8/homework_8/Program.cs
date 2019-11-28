namespace homework_8
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Figure[] figures = {new Triangle(1,2), new Circle(3,4), new Rectangle(5,6)};
            var drawVisitor = new DrawVisitor();
            var getAreaVisitor = new GetAreaVisitor();
            var getPerimeterVisitor = new GetPerimeterVisitor();
            foreach (var figure in figures)
            {
                figure.Accept(drawVisitor);
                figure.Accept(getAreaVisitor);
                figure.Accept(getPerimeterVisitor);
            }
        }
    }
}