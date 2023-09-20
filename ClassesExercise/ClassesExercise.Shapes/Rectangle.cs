using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassesExercise.Shapes
{
    public class Rectangle : Shape
    {
        public Rectangle(double x, double y) : base(x, y)
        {
        }

        public override double CalculateArea()
        {
            return 0;
        }

        public new void Draw()
        {
            Console.WriteLine("Drawing Rectangle!");
        }
    }
}
