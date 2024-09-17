using HabitTracker.Models;

namespace HabitTracker.Data.Interfaces;

public interface IHabitRepository
{
    void AddHabit(string habitName);
    void UpdateHabit(Habit habit);
    void DeleteHabit(Guid habitId);
    Habit? GetHabit(Guid habitId);
    List<Habit> GetAllHabits();
    int GetHabitOccurrencesCount(Guid habitId);
    void InsertOccurrence(Guid habitId, DateTime date);
}