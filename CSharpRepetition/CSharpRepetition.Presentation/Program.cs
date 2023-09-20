internal static class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("CSharp repetition :)\n");

        while (true)
        {
            Console.WriteLine("\nExercises:");
            Console.WriteLine("Exercise 1 [1]");
            Console.WriteLine("Exercise 2 [2]");

            Console.Write("\nChoose the exercise [number]: ");
            string input = Console.ReadLine();
            if(!int.TryParse(input, out int key) || key < 1 || key > 2)
            {
                Console.WriteLine("Invalid input. Try again!");
                continue;
            }
            
            if(key == 1)
                key.Exercise1();
            else if(key == 2)
                key.Exercise2();
        }
    }

    private static void Exercise1(this int number) => number.IsNumberOne().IsNumberOddOrEven();
    private static void Exercise2(this int number)
    {
        for (int i = 1; i < 100; i++)
        {
            if (i.IsNumberDivisible(3) && i.IsNumberDivisible(5))
                Console.WriteLine("FizzBuzz");
            else if (i.IsNumberDivisible(3))
                Console.WriteLine("Fizz");
            else if (i.IsNumberDivisible(5))
                Console.WriteLine("Buzz");
            else
                Console.WriteLine(i);
        }
    }

    private static bool IsNumberDivisible(this int number, int divisor) => number % divisor == 0;

    private static int IsNumberOne(this int number)
    {
        if(number == 1)
            Environment.Exit(0);
        return number;
    }

    private static int IsNumberOddOrEven(this int number)
    {
        if(number % 2 == 0)
            Console.WriteLine("Number is even.");
        else
            Console.WriteLine("Number is odd.");
        return number;
    }
}