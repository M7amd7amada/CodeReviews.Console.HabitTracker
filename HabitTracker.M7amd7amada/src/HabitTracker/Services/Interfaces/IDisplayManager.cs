using HabitTracker.Models;

namespace HabitTracker.Services.Interfaces;

public interface IDisplayManager
{
    void DisplayWelcomeMessage();
    void DisplayMainMenu();
    void DisplayInsertHabit();
    void DisplayInsertOccurrence();
    void DisplayHabits(List<Habit> habits);
}