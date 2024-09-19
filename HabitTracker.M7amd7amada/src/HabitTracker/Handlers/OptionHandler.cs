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
                DeleteRecord();
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
        int habitNumber = inputManager.GetHabitNubmer(Enumerable.Range(1, habits.Count).ToList());
        var occurrences = habits.ElementAt(habitNumber - 1).Occurrences;
        displayManager.DisplayOccurrences(occurrences);
    }

    private void InsertRecord()
    {
        displayManager.DisplayInsertHabit();
        var habitName = inputManager.GetHabitName();
        repository.AddHabit(habitName);
    }

    private void DeleteRecord()
    {
        var habits = repository.GetAllHabits();
        displayManager.DisplayHabits(habits);
        int habitNumber = inputManager.GetHabitNubmer(Enumerable.Range(1, habits.Count).ToList());
        var habitToDelete = habits.ElementAt(habitNumber - 1);
        Console.WriteLine($"Attempting to delete habit: {habitToDelete.Name} with ID: {habitToDelete.Id}");
        repository.DeleteHabit(habitToDelete.Id);
        Console.WriteLine("Deletion attempt completed. Verifying...");
        var remainingHabits = repository.GetAllHabits();
        if (remainingHabits.Any(h => h.Id == habitToDelete.Id))
        {
            Console.WriteLine("Error: Habit was not deleted from the database.");
        }
        else
        {
            Console.WriteLine("Habit successfully deleted from the database.");
        }
    }
}