namespace Timesheets.Web.Models
{
    /// <summary>
    /// Represents an existing timesheet with changes that need to be saved.
    /// </summary>
    public record TimesheetSaveModel(
        string? Name,
        string? Employee,
        string? Customer,
        string? PeriodEnds,
        string? PeriodStarts);
}