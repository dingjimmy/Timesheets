namespace Timesheets.Web.Models.Timesheet.Organisms
{
    /// <summary>
    /// Represents an existing timesheet with changes that need to be saved.
    /// </summary>
    public record EditTimesheetModel(
        string? Name,
        string? Employee,
        string? Customer,
        string? PeriodEnds,
        string? PeriodStarts);
}