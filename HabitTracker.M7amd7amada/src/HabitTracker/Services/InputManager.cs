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

    public string GetHabitName()
    {
        return Console.ReadLine() ?? string.Empty;
    }

    private static Option ParseOption(string input)
    {
        return (int.TryParse(input, out int value) && Option.List.Select(x => x.Value)
            .Contains(value)) ? Option.FromValue(value) : Option.InvalidOption;
    }

    public int GetHabitNubmer(List<int> habitsNumbers)
    {
        while (true)
        {
            Console.Write("Enter the number of the habit to list its occurrences: ");
            string input = Console.ReadLine() ?? string.Empty;
            if (int.TryParse(input, out int value) && habitsNumbers.Contains(value))
            {
                return value;
            }
            Console.WriteLine("Invalid option. Please try again.");
        }
    }
}