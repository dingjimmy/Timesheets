using System.Drawing;

namespace Timesheets.Web.Models
{
    /// <summary>
    /// Represents the data needed to display a <see cref="Timesheets.Domain.Timesheet"/> on screen.
    /// </summary>
    public record TimesheetViewModel(
        int ID,
        string Name,
        string Employee,
        string Customer,
        DateTime PeriodEnds,
        DateTime PeriodStarts,
        IEnumerable<TimesheetEntryViewModel> Entries,
        IEnumerable<WorkType> AvailableWorkTypes
        );

    /// <summary>
    /// 
    /// </summary>
    /// <param name="ID"></param>
    /// <param name="DayOfWeek"></param>
    /// <param name="HourOfDay"></param>
    /// <param name="TypeOfWork"></param>
    public record TimesheetEntryViewModel(int ID, DayOfTheWeek DayOfWeek, int HourOfDay, WorkType TypeOfWork);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="Number"></param>
    /// <param name="Name"></param>
    public record DayOfTheWeek(int Number, string Name)
    {
        public static DayOfTheWeek Monday { get; } = new(1, "Monday");
        public static DayOfTheWeek Tuesday { get; } = new(2, "Tuesday");
        public static DayOfTheWeek Wednesday { get; } = new(3, "Wednesday");
        public static DayOfTheWeek Thursday { get; } = new(4, "Thursday");
        public static DayOfTheWeek Friday { get; } = new(5, "Friday");      
        public static DayOfTheWeek Saturday { get; } = new(6, "Saturday");
        public static DayOfTheWeek Sunday { get; } = new(7, "Sunday");

        public static DayOfTheWeek FromNumber(int number)
        {
            return number switch
            {
                1 => Monday,
                2 => Tuesday,
                3 => Wednesday,
                4 => Thursday,
                5 => Friday,
                6 => Saturday,
                7 => Sunday,
                _ => throw new ArgumentOutOfRangeException(nameof(number), number, "Day of week must be between 1 and 7.")
            };
        }

        public override string ToString()
        {
            return Name;
        }

        public string ToShortString()
        {
            return Name.Substring(0, 3);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="ID"></param>
    /// <param name="Name"></param>
    /// <param name="Colour"></param>
    public record WorkType(int ID, string Name, string Colour)
    {
        public static WorkType NotWorking { get; } = new(0, "Not Working", "#E8E8E8");
    }
}