namespace HabitTracker.Models;

public class Habit
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = default!;

    // Relationships
    public List<Occurrence> Occurrences { get; set; } = [];
}