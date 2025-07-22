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

    private static readonly ConsoleColor[] monkeyColors = new[]
    {
        ConsoleColor.Red, ConsoleColor.Green, ConsoleColor.Blue, ConsoleColor.Yellow,
        ConsoleColor.Magenta, ConsoleColor.Cyan, ConsoleColor.White, ConsoleColor.Gray,
        ConsoleColor.DarkRed, ConsoleColor.DarkGreen, ConsoleColor.DarkBlue, ConsoleColor.DarkYellow,
        ConsoleColor.DarkMagenta
    };

    /// <summary>
    /// Writes text in the specified color and resets to default afterward.
    /// </summary>
    private static void WriteColoredText(string text, ConsoleColor color, bool newLine = true)
    {
        var originalColor = Console.ForegroundColor;
        Console.ForegroundColor = color;
        if (newLine)
            Console.WriteLine(text);
        else
            Console.Write(text);
        Console.ForegroundColor = originalColor;
    }

    /// <summary>
    /// Gets a unique color for a monkey based on its name hash.
    /// </summary>
    private static ConsoleColor GetMonkeyColor(string monkeyName)
    {
        var hash = monkeyName.GetHashCode();
        return monkeyColors[Math.Abs(hash) % monkeyColors.Length];
    }

    public static void Main(string[] args)
    {
        var random = new Random();
        while (true)
        {
            // Display random ASCII art in a random color
            if (random.Next(0, 3) == 0)
            {
                Console.WriteLine();
                var artColor = monkeyColors[random.Next(monkeyColors.Length)];
                WriteColoredText(asciiArt[random.Next(asciiArt.Length)], artColor);
                Console.WriteLine();
            }

            WriteColoredText("🐒 Monkey App Menu:", ConsoleColor.White);
            WriteColoredText("1. List all monkeys", ConsoleColor.Cyan);
            WriteColoredText("2. Get details for a specific monkey by name", ConsoleColor.Green);
            WriteColoredText("3. Get a random monkey", ConsoleColor.Yellow);
            WriteColoredText("4. Exit app", ConsoleColor.Magenta);
            WriteColoredText("Select an option (1-4): ", ConsoleColor.White, false);

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
                    WriteColoredText("Goodbye! 👋", ConsoleColor.Green);
                    return;
                default:
                    WriteColoredText("Invalid option. Please try again.\n", ConsoleColor.Red);
                    break;
            }
        }
    }

    private static void ListAllMonkeys()
    {
        var monkeys = MonkeyHelper.GetMonkeys();
        
        // Header with colors
        WriteColoredText("🐵 All Monkeys:", ConsoleColor.White);
        WriteColoredText(string.Format("| {0,-20} | {1,-25} | {2,-8} |", "Name", "Location", "Population"), ConsoleColor.Gray);
        WriteColoredText(new string('-', 65), ConsoleColor.Gray);
        
        foreach (var monkey in monkeys)
        {
            var monkeyColor = GetMonkeyColor(monkey.Name);
            var originalColor = Console.ForegroundColor;
            
            Console.ForegroundColor = monkeyColor;
            Console.Write("| {0,-20} ", monkey.Name);
            
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("| {0,-25} ", monkey.Location);
            
            Console.ForegroundColor = GetPopulationColor(monkey.Population);
            Console.WriteLine("| {0,-8} |", monkey.Population);
            
            Console.ForegroundColor = originalColor;
        }
        Console.WriteLine();
    }

    /// <summary>
    /// Gets a color based on population size for visual indication.
    /// </summary>
    private static ConsoleColor GetPopulationColor(int population)
    {
        return population switch
        {
            > 20000 => ConsoleColor.Green,      // High population - green
            > 10000 => ConsoleColor.Yellow,     // Medium population - yellow  
            > 1000 => ConsoleColor.DarkYellow,  // Low population - dark yellow
            _ => ConsoleColor.Red               // Very low population - red (endangered)
        };
    }

    private static void GetMonkeyByName()
    {
        WriteColoredText("🔍 Enter monkey name: ", ConsoleColor.Cyan, false);
        var name = Console.ReadLine() ?? string.Empty;
        var monkey = MonkeyHelper.GetMonkeyByName(name);
        if (monkey is null)
        {
            WriteColoredText($"❌ No monkey found with the name '{name}'.\n", ConsoleColor.Red);
            return;
        }
        WriteColoredText($"✅ Found monkey:", ConsoleColor.Green);
        DisplayMonkeyDetails(monkey);
    }

    private static void GetRandomMonkey()
    {
        var monkey = MonkeyHelper.GetRandomMonkey();
        WriteColoredText($"🎲 Random monkey (accessed {MonkeyHelper.GetRandomMonkeyAccessCount()} times):", ConsoleColor.Yellow);
        DisplayMonkeyDetails(monkey);
    }

    private static void DisplayMonkeyDetails(Monkey monkey)
    {
        var monkeyColor = GetMonkeyColor(monkey.Name);
        
        WriteColoredText($"🐒 Name: ", ConsoleColor.White, false);
        WriteColoredText(monkey.Name, monkeyColor);
        
        WriteColoredText("📍 Location: ", ConsoleColor.White, false);
        WriteColoredText(monkey.Location, ConsoleColor.Cyan);
        
        WriteColoredText("👥 Population: ", ConsoleColor.White, false);
        WriteColoredText(monkey.Population.ToString(), GetPopulationColor(monkey.Population));
        
        WriteColoredText("🌐 Latitude: ", ConsoleColor.White, false);
        WriteColoredText(monkey.Latitude.ToString(), ConsoleColor.Gray);
        
        WriteColoredText("🌐 Longitude: ", ConsoleColor.White, false);
        WriteColoredText(monkey.Longitude.ToString(), ConsoleColor.Gray);
        
        WriteColoredText("📝 Details: ", ConsoleColor.White, false);
        WriteColoredText(monkey.Details, ConsoleColor.White);
        
        WriteColoredText("🖼️ Image: ", ConsoleColor.White, false);
        WriteColoredText(monkey.Image, ConsoleColor.Blue);
        
        Console.WriteLine();
    }
}
