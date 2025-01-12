// ReSharper disable PropertyCanBeMadeInitOnly.Global

using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using Timesheets.Web.Models.Shared.Molecules;

namespace Timesheets.Web.Models.Timesheet.Organisms
{
    /// <summary>
    /// Represents a new timesheet to be added to the system.
    /// </summary>
    public partial class NewTimesheetModel : IValidatableObject
    {
        public TextInputModel Name { get; set; } = new(nameof(Name));

        public TextInputModel Employee { get; set; } = new(nameof(Employee));
        
        public TextInputModel Customer { get; set; } = new(nameof(Customer));

        public DateInputModel PeriodEnds { get; set; } = new(nameof(PeriodEnds));
        
        public DateInputModel PeriodStarts { get; set; } = new(nameof(PeriodStarts));

        public NewTimesheetModel()
        {

        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrWhiteSpace(Name.Value) || !NameRegex().IsMatch(Name.Value))
            {
                yield return new ValidationResult(
                    "Must be between 5 and 128 characters long, and only contain letters and numbers.", [nameof(Employee)]);
            }

            if (string.IsNullOrWhiteSpace(Customer.Value) || !CustomerRegex().IsMatch(Customer.Value))
            {
                yield return new ValidationResult(
                    "Must be between 5 and 128 characters long, and only contain letters and numbers.", [nameof(Customer)]);
            }

            if (string.IsNullOrWhiteSpace(Employee.Value) || !EmployeeRegex().IsMatch(Employee.Value))
            {
                yield return new ValidationResult(
                    "Must be between 5 and 128 characters long, and only contain letters and numbers.", [nameof(Employee)]);
            }

            if (!DateTime.TryParse(PeriodStarts.Value, out var start))
            {
                yield return new ValidationResult("Start date must be a valid date.", [nameof(PeriodStarts)]);
            }
            
            if (!DateTime.TryParse(PeriodEnds.Value, out var end))
            {
                yield return new ValidationResult("End date must be a valid date.", [nameof(PeriodEnds)]);
            }
            
            if (start >= end)
            {
                yield return new ValidationResult("Start date must be before the end date.",
                    [nameof(PeriodEnds), nameof(PeriodStarts)]);
            }
        }

        [GeneratedRegex("^[a-zA-Z0-9 .?!]{5,128}$", RegexOptions.Multiline, 500)]
        private static partial Regex NameRegex();

        [GeneratedRegex("^[a-zA-Z0-9 .?!]{5,128}$", RegexOptions.Multiline, 500)]
        private static partial Regex CustomerRegex();

        [GeneratedRegex("^[a-zA-Z0-9 .?!]{5,128}$", RegexOptions.Multiline, 500)]
        private static partial Regex EmployeeRegex();
    }
}