using Spectre.Console;
using System.Runtime.CompilerServices;

internal class Program
{
    private const char LOWEST_NUMBER = (char)('0'-1);
    private const char HIGHEST_NUMBER = '9';
    private const int NUMBER_COUNT = 10;

    private const char LOWEST_UPPER_CHAR = (char)('A' - 1);
    private const char HIGHEST_UPPER_CHAR = 'Z';
    private const int UPPER_CHAR_COUNT = 26;

    private const char LOWEST_LOWER_CHAR = (char)('a' - 1);
    private const char HIGHEST_LOWER_CHAR = 'z';
    private const int LOWER_CHAR_COUNT = 26;

    private static void Main(string[] args)
    {
        while (true)
        {
            Console.Clear();
            AnsiConsole.Write(
                new FigletText("Ceasar Encoding")
                    .Centered()
                    .Color(Color.Aqua));

            var selectedMenuOption = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[green]What[/] would you like to do?")
                    .AddChoices(new[] {
                        "Encode", "Decode", "Exit"
                    }));
            Console.WriteLine();

            if (selectedMenuOption == "Exit")
                break;

            if (selectedMenuOption == "Encode")
                Encode();
            else
                Decode();

            AnsiConsole.Prompt(
                new TextPrompt<string>("[grey]Press[/] [aqua]ENTER[/] key to [green]continue[/].")
                    .AllowEmpty());
        }
    }

    private static void Encode()
    {
        string text = AnsiConsole.Ask<string>("\nWhat's your text to [green]encode[/]?");
        int offset = AnsiConsole.Ask<int>("What's the [green]offset[/] you want to encode with?");

        Console.Write("\nThe encoded result is: ");
        AnsiConsole.MarkupLine($"[red]{CodeString(text, offset)}[/]");
        Console.WriteLine("\n\n");
    }

    private static void Decode()
    {
        string text = AnsiConsole.Ask<string>("\nWhat's your text to [green]decode[/]?");
        int offset = AnsiConsole.Ask<int>("What's the [green]offset[/] you want to decode with?");

        Console.Write("\nThe decoded result is: ");
        AnsiConsole.MarkupLine($"[red]{CodeString(text, (offset * -1))}[/]");
        Console.WriteLine("\n\n");
    }

    private static string CodeString(string text, int offset)
    { 
        string codedText = string.Empty;
        text.ToList().ForEach(x => codedText += (char)(x == ' ' ? x 
        : char.IsDigit(x) 
        ? (new Func<char>(() => { var diffrence = (x + (offset % NUMBER_COUNT)); var res = (char)((diffrence % HIGHEST_NUMBER) + ((diffrence > HIGHEST_NUMBER) ? LOWEST_NUMBER : 0) + ((diffrence <= LOWEST_NUMBER) ? NUMBER_COUNT : 0)); return (res == 0) ? HIGHEST_NUMBER : res; })).Invoke()
        : char.IsUpper(x) 
        ? (new Func<char>(() => { var diffrence = (x + (offset % UPPER_CHAR_COUNT)); var res = (char)((diffrence % HIGHEST_UPPER_CHAR) + ((diffrence > HIGHEST_UPPER_CHAR) ? LOWEST_UPPER_CHAR : 0) + ((diffrence <= LOWEST_UPPER_CHAR) ? UPPER_CHAR_COUNT : 0)); return (res == 0) ? HIGHEST_UPPER_CHAR : res; })).Invoke()
        : char.IsLower(x)
        ? (new Func<char>(() => { var diffrence = (x + (offset % LOWER_CHAR_COUNT)); var res = (char)((diffrence % HIGHEST_LOWER_CHAR) + ((diffrence > HIGHEST_LOWER_CHAR) ? LOWEST_LOWER_CHAR : 0) + ((diffrence <= LOWEST_LOWER_CHAR) ? LOWER_CHAR_COUNT : 0)); return (res == 0) ? HIGHEST_LOWER_CHAR : res; })).Invoke()
        : '?'
        ));

        return codedText;
    }
}