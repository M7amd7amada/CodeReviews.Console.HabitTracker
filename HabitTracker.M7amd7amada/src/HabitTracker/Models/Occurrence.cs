namespace HabitTracker.Models;

public class Occurrence
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Date { get; set; } = default!;

    // Relationships
    public Guid HabitId { get; set; }
    public Habit Habit { get; set; } = default!;
}