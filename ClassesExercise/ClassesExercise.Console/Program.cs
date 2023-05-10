using ClassesExercise.Console;
using System.Drawing;

internal class Program
{
    private static void Main(string[] args)
    {
        CoffeeMachine redCoffeeMachine = new CoffeeMachine();
        redCoffeeMachine.color = "red";

        CoffeeMachine greenCoffeeMachine = new CoffeeMachine();
        greenCoffeeMachine.color = "green";

        redCoffeeMachine.CreateSmallCoffee();
        greenCoffeeMachine.CreateSmallCoffee();
    }
}