using System.Drawing;

namespace Timesheets.Web.Models
{
    /// <summary>
    /// Represents a timesheet that records the hours worked by a person for a given period.
    /// </summary>
    public record TimesheetViewModel(
        int ID,
        string Name,
        string Employee,
        string Customer,
        DateTime PeriodEnds,
        DateTime PeriodStarts,
        IEnumerable<TimesheetEntryViewModel> Entries);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="ID"></param>
    /// <param name="DayOfWeek"></param>
    /// <param name="HourOfDay"></param>
    /// <param name="TypeOfWork"></param>
    public record TimesheetEntryViewModel(int ID, int DayOfWeek, int HourOfDay, WorkType TypeOfWork);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="ID"></param>
    /// <param name="Name"></param>
    /// <param name="Colour"></param>
    public record WorkType(int ID, string Name, string Colour)
    {
        public static WorkType NotWorking = new(0, "Not Working", "#E8E8E8");
    }
}