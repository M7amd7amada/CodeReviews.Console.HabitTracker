using HabitTracker.Handlers.Interfaces;

namespace HabitTracker.Handlers;

public class OptionHandler : IOptionHandler
{
    public void HandleOption(Option option)
    {
        switch (option.Name)
        {
            case nameof(Option.InsertRecord):
                Console.WriteLine("Inserting record...");
                break;
            case nameof(Option.ViewAllRecords):
                Console.WriteLine("Viewing habits...");
                break;
            case nameof(Option.UpdateRecord):
                Console.WriteLine("Updating record...");
                break;
            case nameof(Option.DeleteRecord):
                Console.WriteLine("Deleting record...");
                break;
            case nameof(Option.Exit):
                Environment.Exit(0);
                break;
            default:
                Console.WriteLine("Invalid option.");
                break;
        }
    }
}