using HabitTracker.Models;

namespace HabitTracker.Services.Interfaces;

public interface IInputManager
{
    Option GetUserOption();
    string GetHabitName();
}