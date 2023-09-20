using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassesExercise.Shapes
{
    public class Circle : Shape
    {
        public Circle(double x, double y) : base(x, y)
        {
        }

        public double Radius { get; set; } 
    }
}
