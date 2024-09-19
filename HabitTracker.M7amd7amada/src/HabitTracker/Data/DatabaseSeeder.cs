using Bogus;

using HabitTracker.Models;

namespace HabitTracker.Data;

public static class DatabaseSeeder
{
    private static readonly string DatabasePath = "habits.db";
    public static void Seed()
    {
        using var context = new AdoNetDbContext(DatabasePath);
        var existingHabits = context.ExecuteQuery("SELECT COUNT(*) FROM Habits");
        int habitCount = Convert.ToInt32(existingHabits[0]["COUNT(*)"]);

        if (habitCount == 0)
        {
            SeedHabits();
            SeedOccurrences();
            Console.WriteLine("Database seeding completed.");
        }
    }

    private static void SeedHabits()
    {
        using var context = new AdoNetDbContext(DatabasePath);
        var habitFaker = new Faker<Habit>()
            .RuleFor(h => h.Id, f => Guid.NewGuid())
            .RuleFor(h => h.Name, f => f.Lorem.Word());

        var habits = habitFaker.Generate(5);

        foreach (var habit in habits)
        {
            string insertHabitQuery = "INSERT INTO Habits (Id, Name) VALUES (@Id, @Name)";
            var parameters = new Dictionary<string, object>
                {
                    { "@Id", habit.Id },
                    { "@Name", habit.Name }
                };

            context.ExecuteNonQuery(insertHabitQuery, parameters);
        }
    }

    private static void SeedOccurrences()
    {
        using var context = new AdoNetDbContext(DatabasePath);
        var occurrenceFaker = new Faker<Occurrence>()
            .RuleFor(o => o.Id, f => Guid.NewGuid())
            .RuleFor(o => o.Date, f => f.Date.Past().ToString("yyyy-MM-dd"))
            .RuleFor(o => o.HabitId, f => Guid.Parse(f.PickRandom(context
                                                .ExecuteQuery("SELECT Id FROM Habits")
                                                .Select(row => row["Id"].ToString())
                                                .ToList())!));

        var occurrences = occurrenceFaker.Generate(20);

        foreach (var occurrence in occurrences)
        {
            string insertOccurrenceQuery = "INSERT INTO Occurrences (Id, Date, HabitId) VALUES (@Id, @Date, @HabitId)";
            var parameters = new Dictionary<string, object>
                {
                    { "@Id", occurrence.Id },
                    { "@Date", occurrence.Date },
                    { "@HabitId", occurrence.HabitId }
                };

            context.ExecuteNonQuery(insertOccurrenceQuery, parameters);
        }
    }
}