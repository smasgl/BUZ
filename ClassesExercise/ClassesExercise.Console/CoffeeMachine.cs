using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassesExercise.Console
{
    public class CoffeeMachine
    {
        public CoffeeMachineColor Color { get; }
        public int Power { get; set; }

        public static int NumberOfCreatedCoffeeMachines { get; private set; } = 0;

        public CoffeeMachine(CoffeeMachineColor color) : this(color, 1)
        {
        }

        public CoffeeMachine(CoffeeMachineColor color, int power)
        {
            Color = color;
            Power = power;

            NumberOfCreatedCoffeeMachines++;
        }

        public void SetGrindingDegree(int degree)
        {
            System.Console.WriteLine($"Set grinding degree to: {degree}");
        }

        public void SetGrindingPower(int power)
        {
            Power = power;
            System.Console.WriteLine($"Set grinding power to: {power}");
        }

        public void CreateBigCoffee()
        {
            StartHeater();
            System.Console.WriteLine($"{Color} maschine creating a coffee with a big amount of water.");
        }

        public void CreateSmallCoffee()
        {
            StartHeater();
            System.Console.WriteLine($"{Color} maschine creating a coffee with a small amount of water.");
        }

        private void StartHeater()
        {
            System.Console.WriteLine($"{Color} maschine starting the heater.");
        }
    }
}
