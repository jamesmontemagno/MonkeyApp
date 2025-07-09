using System;
using MyMonkeyApp;

namespace MyMonkeyApp;

public class Program
{
    private static readonly string[] asciiArt = new[]
    {
        @"  (o.o)   ",
        @" ( (..) ) ",
        @"  ( : )   ",
        @"  (" + '"' + @"." + '"' + @")   ",
        @"  (='.'=)  ",
        @"  (o_O)    ",
        @"  ( ^.^ )  "
    };

    public static void Main(string[] args)
    {
        var random = new Random();
        while (true)
        {
            // Display random ASCII art
            if (random.Next(0, 3) == 0)
            {
                Console.WriteLine();
                Console.WriteLine(asciiArt[random.Next(asciiArt.Length)]);
                Console.WriteLine();
            }

            Console.WriteLine("Monkey App Menu:");
            Console.WriteLine("1. List all monkeys");
            Console.WriteLine("2. Get details for a specific monkey by name");
            Console.WriteLine("3. Get a random monkey");
            Console.WriteLine("4. Exit app");
            Console.Write("Select an option (1-4): ");

            var input = Console.ReadLine();
            Console.WriteLine();

            switch (input)
            {
                case "1":
                    ListAllMonkeys();
                    break;
                case "2":
                    GetMonkeyByName();
                    break;
                case "3":
                    GetRandomMonkey();
                    break;
                case "4":
                    Console.WriteLine("Goodbye!");
                    return;
                default:
                    Console.WriteLine("Invalid option. Please try again.\n");
                    break;
            }
        }
    }

    private static void ListAllMonkeys()
    {
        var monkeys = MonkeyHelper.GetMonkeys();
        Console.WriteLine("| {0,-20} | {1,-25} | {2,-8} |", "Name", "Location", "Population");
        Console.WriteLine(new string('-', 65));
        foreach (var monkey in monkeys)
        {
            Console.WriteLine("| {0,-20} | {1,-25} | {2,-8} |", monkey.Name, monkey.Location, monkey.Population);
        }
        Console.WriteLine();
    }

    private static void GetMonkeyByName()
    {
        Console.Write("Enter monkey name: ");
        var name = Console.ReadLine() ?? string.Empty;
        var monkey = MonkeyHelper.GetMonkeyByName(name);
        if (monkey is null)
        {
            Console.WriteLine($"No monkey found with the name '{name}'.\n");
            return;
        }
        DisplayMonkeyDetails(monkey);
    }

    private static void GetRandomMonkey()
    {
        var monkey = MonkeyHelper.GetRandomMonkey();
        Console.WriteLine($"Random monkey (accessed {MonkeyHelper.GetRandomMonkeyAccessCount()} times):");
        DisplayMonkeyDetails(monkey);
    }

    private static void DisplayMonkeyDetails(Monkey monkey)
    {
        Console.WriteLine($"Name: {monkey.Name}");
        Console.WriteLine($"Location: {monkey.Location}");
        Console.WriteLine($"Population: {monkey.Population}");
        Console.WriteLine($"Latitude: {monkey.Latitude}");
        Console.WriteLine($"Longitude: {monkey.Longitude}");
        Console.WriteLine($"Details: {monkey.Details}");
        Console.WriteLine($"Image: {monkey.Image}");
        Console.WriteLine();
    }
}
