namespace HabitTracker.Services;

public class InputManager : IInputManager
{
    public Option GetUserOption()
    {
        while (true)
        {
            Console.Write("Enter your choice: ");
            string input = Console.ReadLine() ?? string.Empty;
            Option option = ParseOption(input);
            if (option != Option.InvalidOption)
            {
                return option;
            }
            Console.WriteLine("Invalid option. Please try again.");
        }
    }

    private static Option ParseOption(string input)
    {
        return int.TryParse(input, out int value) ? Option.FromValue(value) : Option.InvalidOption;
    }

    public string GetHabitName()
    {
        return Console.ReadLine() ?? string.Empty;
    }
}