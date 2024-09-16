using HabitTracker.Data.Interfaces;
using HabitTracker.Models;

namespace HabitTracker.Data;

public class HabitRepository(AdoNetDbContext context) : IHabitRepository
{
    public void AddHabit(string habitName)
    {
        var query = "INSERT INTO Habits (Name) VALUES (@name)";
        var paramaters = new Dictionary<string, object>
        {
            { "@id", Guid.NewGuid().ToString() },
            { "@name", habitName }
        };
        context.ExecuteNonQuery(query, paramaters);
    }

    public void InsertOccurrence(Guid habitId, DateTime date)
    {
        var query = "INSERT INTO Occurrences (Date, HabitId) VALUES (@date, @habitId)";
        var paramaters = new Dictionary<string, object>
        {
            { "@id", Guid.NewGuid().ToString() },
            { "@date", date.ToString() },
            { "@habitId", habitId.ToString() }
        };
        context.ExecuteNonQuery(query, paramaters);
    }

    public int GetHabitOccurrences(Guid habitId)
    {
        return (GetHabit(habitId) ?? throw new ArgumentNullException(nameof(habitId))).Occurrences.Count;
    }

    public List<Habit> GetAllHabits()
    {
        var query = "SELECT Id, Name FROM Habit";

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
        var query = "DELETE FROM Habits WHERE Id = @id";
        var parameters = new Dictionary<string, object>
        {
            { "@id", habitId.ToString() }
        };
        context.ExecuteNonQuery(query, parameters);
    }

    public Habit? GetHabit(Guid habitId)
    {
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

    private List<Occurrence> GetOccurrences(Guid habitId)
    {
        var query = "SELECT Id, Date FROM Occurrences";

        var results = context.ExecuteQuery(query);

        var occurrences = new List<Occurrence>();

        foreach (var result in results)
        {
            occurrences.Add(new Occurrence
            {
                Id = Guid.Parse(result["Id"].ToString()!),
                Date = result["Date"].ToString()!
            });
        }

        return occurrences;
    }

    public void UpdateHabit(Habit habit)
    {
        var query = "UPDATE Habits SET Name = @name WHERE Id = @id";
        var parameters = new Dictionary<string, object>
        {
            { "@id", habit.Id.ToString() },
            { "@name", habit.Name }
        };
        context.ExecuteNonQuery(query, parameters);
    }
}
