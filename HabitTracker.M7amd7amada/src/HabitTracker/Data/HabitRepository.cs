using HabitTracker.Data.Interfaces;
using HabitTracker.Models;

namespace HabitTracker.Data;

public class HabitRepository : IHabitRepository
{
    private readonly string _databasePath = "habits.db";
    public void AddHabit(string habitName)
    {
        using AdoNetDbContext context = new(_databasePath);

        var query = "INSERT INTO Habits (Id, Name) VALUES (@id, @name)";
        var paramaters = new Dictionary<string, object>
        {
            { "@id", Guid.NewGuid().ToString() },
            { "@name", habitName }
        };
        context.ExecuteNonQuery(query, paramaters);
    }

    public void InsertOccurrence(Guid habitId, DateTime date)
    {
        using AdoNetDbContext context = new(_databasePath);

        var query = "INSERT INTO Occurrences (Date, HabitId) VALUES (@date, @habitId)";
        var paramaters = new Dictionary<string, object>
        {
            { "@id", Guid.NewGuid().ToString() },
            { "@date", date.ToString() },
            { "@habitId", habitId.ToString() }
        };
        context.ExecuteNonQuery(query, paramaters);
    }

    public int GetHabitOccurrencesCount(Guid habitId)
    {
        return (GetHabit(habitId) ?? throw new ArgumentNullException(nameof(habitId)))
            .Occurrences.Where(x => x.HabitId == habitId).Count();
    }

    public List<Habit> GetAllHabits()
    {
        using AdoNetDbContext context = new(_databasePath);

        var query = "SELECT Id, Name FROM Habits";

        var results = context.ExecuteQuery(query);

        var habits = new List<Habit>();

        foreach (var result in results)
        {
            var habitId = Guid.Parse(result["Id"].ToString()!);
            habits.Add(new Habit
            {
                Id = habitId,
                Name = result["Name"].ToString()!,
                Occurrences = GetOccurrences(habitId)
            });
        }

        return habits;
    }

    public void DeleteHabit(Guid habitId)
    {
        using AdoNetDbContext context = new(_databasePath);

        try
        {
            var deleteOccurrencesQuery = "DELETE FROM Occurrences WHERE HabitId = @id";
            var occurrencesParameters = new Dictionary<string, object>
            {
                { "@id", habitId.ToString() }
            };
            int occurrencesDeleted = context.ExecuteNonQuery(deleteOccurrencesQuery, occurrencesParameters);
            Console.WriteLine($"Deleted {occurrencesDeleted} occurrences for habit {habitId}");

            var deleteHabitQuery = "DELETE FROM Habits WHERE Id = @id";
            var habitParameters = new Dictionary<string, object>
            {
                { "@id", habitId.ToString() }
            };
            int habitsDeleted = context.ExecuteNonQuery(deleteHabitQuery, habitParameters);
            Console.WriteLine($"Deleted {habitsDeleted} habits with ID {habitId}");

            if (habitsDeleted == 0)
            {
                Console.WriteLine($"No habit found with ID {habitId}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error deleting habit: {ex.Message}");
        }
    }

    public Habit? GetHabit(Guid habitId)
    {
        using AdoNetDbContext context = new(_databasePath);
        var query = "SELECT Id, Name FROM Habits WHERE Id = @id";
        var parameters = new Dictionary<string, object>
        {
            { "@id", habitId.ToString() }
        };

        var result = context.ExecuteQuery(query, parameters).FirstOrDefault();

        return result is not null
            ? new Habit
            {
                Id = Guid.Parse(result["Id"].ToString()!),
                Name = result["Name"].ToString()!,
                Occurrences = GetOccurrences(habitId)
            }
            : null;
    }

    public void UpdateHabit(Habit habit)
    {
        using AdoNetDbContext context = new(_databasePath);
        var query = "UPDATE Habits SET Name = @name WHERE Id = @id";
        var parameters = new Dictionary<string, object>
        {
            { "@id", habit.Id.ToString() },
            { "@name", habit.Name }
        };
        context.ExecuteNonQuery(query, parameters);
    }

    private List<Occurrence> GetOccurrences(Guid habitId)
    {
        using AdoNetDbContext context = new(_databasePath);
        var query = "SELECT Id, Date FROM Occurrences WHERE HabitId = @habitId";

        var paramaters = new Dictionary<string, object>
        {
            { "@habitId", habitId}
        };

        var results = context.ExecuteQuery(query, paramaters);

        var occurrences = new List<Occurrence>();

        foreach (var result in results)
        {
            occurrences.Add(new Occurrence
            {
                Id = Guid.Parse(result["Id"].ToString()!),
                Date = result["Date"].ToString()!,
                HabitId = habitId,
                Habit = GetHabit(habitId)!
            });
        }

        return occurrences;
    }

}
