using HabitTracker.Handlers;
using HabitTracker.Handlers.Interfaces;
using HabitTracker.Services;

using Microsoft.Extensions.DependencyInjection;

namespace HabitTracker;

public class Program
{
    public static void Main()
    {
        var serviceProvider = new ServiceCollection()
            .AddSingleton<IInputManager, InputManager>()
            .AddSingleton<IDisplayManager, DisplayManager>()
            .AddSingleton<IOptionHandler, OptionHandler>()
            .AddSingleton<IProgramManager, ProgramManager>()
            .BuildServiceProvider();

        var programManager = serviceProvider.GetRequiredService<IProgramManager>();

        programManager.Run();
    }
}