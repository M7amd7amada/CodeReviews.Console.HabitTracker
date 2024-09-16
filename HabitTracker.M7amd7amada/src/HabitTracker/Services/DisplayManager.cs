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
}