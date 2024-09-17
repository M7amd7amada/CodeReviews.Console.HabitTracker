using HabitTracker.Data;
using HabitTracker.Handlers.Interfaces;

namespace HabitTracker.Services;

public class ProgramManager(
    IDisplayManager displayManager,
    IInputManager inputManager,
    IOptionHandler optionHandler) : IProgramManager
{
    public void Run()
    {
        DatabaseSeeder.Seed();
        while (true)
        {
            displayManager.DisplayMainMenu();
            Option option = inputManager.GetUserOption();
            optionHandler.HandleOption(option);
        }
    }
}