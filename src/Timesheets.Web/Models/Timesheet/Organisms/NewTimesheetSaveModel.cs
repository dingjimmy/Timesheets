namespace Timesheets.Web.Models
{
    /// <summary>
    /// Represents a new timesheet to be added to the system.
    /// </summary>
    public record NewTimesheetSaveModel(
        string? Name,
        string? Employee,
        string? Customer,
        string? PeriodEnds,
        string? PeriodStarts);
}