using HabitTracker.Models;

using Spectre.Console;

namespace HabitTracker.Services;

public class DisplayManager : IDisplayManager
{
    public void DisplayMainMenu()
    {
        Console.WriteLine("Main Menu\n");
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

    public void DisplayInsertOccurrence()
    {
        throw new NotImplementedException();
    }

    public void DisplayHabits(List<Habit> habits)
    {
        var table = new Table();
        table.AddColumn("ID");
        table.AddColumn("Name");
        table.AddColumn("Occurrences");

        foreach (var habit in habits)
        {
            var occurrenceCount = habit.Occurrences.Count;

            table.AddRow(
                habit.Id.ToString(),
                habit.Name,
                occurrenceCount.ToString()
            );
        }

        AnsiConsole.Write(table);
    }
}