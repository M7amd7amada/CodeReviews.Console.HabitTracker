using Ardalis.SmartEnum;

namespace HabitTracker.Models.Enums;

public class Option(string name, int value) : SmartEnum<Option>(name, value)
{
    public static readonly Option Exit = new(nameof(Exit), 0);
    public static readonly Option ViewAllRecords = new(nameof(ViewAllRecords), 1);
    public static readonly Option InsertRecord = new(nameof(InsertRecord), 2);
    public static readonly Option UpdateRecord = new(nameof(UpdateRecord), 3);
    public static readonly Option DeleteRecord = new(nameof(DeleteRecord), 4);

    public static readonly Option InvalidOption = new(nameof(InvalidOption), -1);
}