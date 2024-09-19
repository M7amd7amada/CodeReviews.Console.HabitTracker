using HabitTracker.Models;

using Spectre.Console;

namespace HabitTracker.Services;

public class DisplayManager : IDisplayManager
{
    public void DisplayMainMenu()
    {
        Console.WriteLine("\nMain Menu\n");
        Console.WriteLine("What would you like to do?\n");

        Console.WriteLine("Type 0 to close the application.");
        Console.WriteLine("Type 1 to View All Records.");
        Console.WriteLine("Type 2 to Insert Record.");
        Console.WriteLine("Type 3 to Update Record.");
        Console.WriteLine("Type 4 to Delete Record.");
        LineSeperator();
    }

    public void DisplayWelcomeMessage()
    {
        Console.WriteLine("Welcome to Habit Tracker!");
        LineSeperator();
    }

    private static void LineSeperator()
    {
        Console.WriteLine(new string('-', 60));
    }

    public void DisplayInsertHabit()
    {
        Console.Write("Insert the habit name: ");
    }

    public void DisplayHabits(List<Habit> habits)
    {
        var table = new Table();
        table.AddColumn("#");
        table.AddColumn("Name");
        table.AddColumn("Occurrences");
        var counter = 0;

        foreach (var habit in habits)
        {
            counter++;
            var occurrenceCount = habit.Occurrences.Count;

            table.AddRow(
                counter.ToString(),
                habit.Name,
                occurrenceCount.ToString()
            );
        }

        AnsiConsole.Write(table);
    }

    public void DisplayOccurrences(List<Occurrence> occurrences)
    {
        var table = new Table();
        table.AddColumn("#");
        table.AddColumn("Habit Name");
        table.AddColumn("Date");
        var counter = 0;

        foreach (var occurrence in occurrences ?? Enumerable.Empty<Occurrence>())
        {
            counter++;

            table.AddRow(
                counter.ToString(),
                (occurrence.Habit ?? new()).Name ?? string.Empty,
                occurrence.Date
            );
        }

        AnsiConsole.Write(table);
    }
}