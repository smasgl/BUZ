namespace ClassesExercise.Shapes
{
    public class Shape
    {
        public Position Position { get; set; }

        public Shape(double x, double y)
        {
            Position = new Position(x, y);
        }

        public virtual double CalculateArea()
        {
            return 0;
        }

        public void Draw()
        {
            Console.WriteLine("Drawing Shape!");
        }
    }
}