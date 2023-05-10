using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassesExercise.Console
{
    public class CoffeeMachine
    {
        public string color;
        public int power;

        public void SetGrindingDegree(int degree)
        {
            System.Console.WriteLine($"Set grinding degree to: {degree}");
        }

        public void SetGrindingPower(int power)
        {
            this.power = power;
            System.Console.WriteLine($"Set grinding power to: {power}");
        }

        public void CreateBigCoffee()
        {
            StartHeater();
            System.Console.WriteLine($"{color} maschine creating a coffee with a big amount of water.");
        }

        public void CreateSmallCoffee()
        {
            StartHeater();
            System.Console.WriteLine($"{color} maschine creating a coffee with a small amount of water.");
        }

        private void StartHeater()
        {
            System.Console.WriteLine($"{color} maschine starting the heater.");
        }
    }
}
