using HabitTracker.Models;

namespace HabitTracker.Services.Interfaces;

public interface IDisplayManager
{
    void DisplayWelcomeMessage();
    void DisplayMainMenu();
    void DisplayInsertHabit();
    void DisplayHabits(List<Habit> habits);
    void DisplayOccurrences(List<Occurrence> occurrences);
}