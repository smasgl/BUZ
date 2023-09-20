using ClassesExercise.Console;
using System.Drawing;

internal class Program
{
    private static void Main(string[] args)
    {
        CoffeeMachine redCoffeeMachine = new CoffeeMachine(CoffeeMachineColor.Red);

        CoffeeMachine greenCoffeeMachine = new CoffeeMachine(CoffeeMachineColor.Green);

        redCoffeeMachine.CreateSmallCoffee();
        greenCoffeeMachine.CreateSmallCoffee();
    }
}