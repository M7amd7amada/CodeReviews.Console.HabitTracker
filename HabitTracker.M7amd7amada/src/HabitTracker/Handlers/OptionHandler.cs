using HabitTracker.Data.Interfaces;
using HabitTracker.Handlers.Interfaces;

namespace HabitTracker.Handlers;

public class OptionHandler(
    IDisplayManager displayManager,
    IInputManager inputManager,
    IHabitRepository repository) : IOptionHandler
{
    public void HandleOption(Option option)
    {
        switch (option.Name)
        {
            case nameof(Option.InsertRecord):
                InsertRecord();
                break;
            case nameof(Option.ViewAllRecords):
                ViewAllRecords();
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

    private void ViewAllRecords()
    {
        var habits = repository.GetAllHabits();
        displayManager.DisplayHabits(habits);
    }

    private void InsertRecord()
    {
        displayManager.DisplayInsertHabit();
        var habitName = inputManager.GetHabitName();
        repository.AddHabit(habitName);
    }
}