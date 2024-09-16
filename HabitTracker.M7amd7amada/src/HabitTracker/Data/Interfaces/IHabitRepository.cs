using HabitTracker.Models;

namespace HabitTracker.Data.Interfaces;

public interface IHabitRepository
{
    void AddHabit(string habitName);
    void UpdateHabit(Habit habit);
    void DeleteHabit(Guid habitId);
    Habit? GetHabit(Guid habitId);
}