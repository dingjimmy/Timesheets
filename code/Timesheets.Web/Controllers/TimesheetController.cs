using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;
using System.Text.Encodings.Web;
using Timesheets.Domain;
using Timesheets.Web.Models;

namespace Timesheets.Web.Controllers
{

    public class TimesheetController : Controller
    {
        private static readonly TimesheetValidator _Validator = new();

        static readonly List<WorkType> _WorkTypes = new()
        {
            { WorkType.NotWorking },
            { new WorkType(1, "SD Service Desk", "#00FF00") },
            { new WorkType(2, "PW Project", "#0000FF") },
            { new WorkType(3, "PD Personal Development", "#FFFF00") },
            { new WorkType(4, "LM Line Management", "#00FFFF") },
            { new WorkType(5, "TC Team Collaboration", "#000000") },
            { new WorkType(6, "OC Other Collaboration", "#000000") }
        };

        static readonly Dictionary<int, TimesheetViewModel> _Timesheets = new()
        {
            { 1001, CreateDemoTimesheet(1001, "2024 Week 5", "HM Government", "Winston Churchill", DateTime.Now, DateTime.Now.AddDays(7)) },
            { 1002, CreateDemoTimesheet(1002, "2024 Week 6", "HM Government", "Winston Churchill", DateTime.Now, DateTime.Now.AddDays(1)) },
            { 1003, CreateDemoTimesheet(1003, "2024 Week 7", "HM Government", "Winston Churchill", DateTime.Now, DateTime.Now.AddDays(1)) },
            { 1004, CreateDemoTimesheet(1004, "2024 Week 8", "HM Government", "Winston Churchill", DateTime.Now, DateTime.Now.AddDays(1)) },
            { 1005, CreateDemoTimesheet(1005, "2024 Week 9", "HM Government", "Winston Churchill", DateTime.Now, DateTime.Now.AddDays(1)) },
            { 1006, CreateDemoTimesheet(1006, "2024 Week 10", "HM Government", "Winston Churchill", DateTime.Now, DateTime.Now.AddDays(1)) },
            { 1007, CreateDemoTimesheet(1007, "2024 Week 11", "HM Government", "Winston Churchill", DateTime.Now, DateTime.Now.AddDays(1)) },
            { 1008, CreateDemoTimesheet(1008, "2024 Week 12", "HM Government", "Winston Churchill", DateTime.Now, DateTime.Now.AddDays(1)) }
        };

        [HttpGet("timesheets")]
        public IActionResult ListTimesheets()
        {
            return View(_Timesheets.Values);
        }

        [HttpGet("timesheet")]
        public ActionResult NewTimesheet()
        {
            return View();
        }

        [HttpPost("timesheet")]
        public ActionResult SaveNewTimesheet(NewTimesheetSaveModel newTimeSheet)
        {
            if (!_Validator.IsValid(newTimeSheet.Name, newTimeSheet.Customer, newTimeSheet.Employee, newTimeSheet.PeriodStarts, newTimeSheet.PeriodEnds))
            {
                return BadRequest();
            }

            try
            {
                var nextId = _Timesheets.Keys.Last() + 1;
                var timesheet = CreateDemoTimesheet(nextId, newTimeSheet.Name, newTimeSheet.Customer, newTimeSheet.Employee, DateTime.Parse(newTimeSheet.PeriodStarts!.ToString()), DateTime.Parse(newTimeSheet.PeriodEnds!.ToString()));
                _Timesheets.Add(nextId, timesheet);

                return ViewTimesheet(timesheet.ID);
            }
            catch
            {
                //TODO: log error
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("timesheet/{id}")]
        public ActionResult ViewTimesheet(int id)
        {
            if (!_Timesheets.TryGetValue(id, out TimesheetViewModel? timesheet))
            {
                return NotFound();
            }

            return View(timesheet);
        }

        [HttpPut("timesheet/{id}")]
        public ActionResult SaveEditedTimesheet(int id, TimesheetSaveModel updatedTimeSheet)
        {
            if (!_Validator.IsValid(updatedTimeSheet.Name, updatedTimeSheet.Customer, updatedTimeSheet.Employee, updatedTimeSheet.PeriodStarts, updatedTimeSheet.PeriodEnds))
            {
                return BadRequest();
            }

            if (!_Timesheets.ContainsKey(id))
            {
                return NotFound();
            }

            try
            {
                var timesheet = CreateDemoTimesheet(id, updatedTimeSheet.Name, updatedTimeSheet.Customer, updatedTimeSheet.Employee, DateTime.Parse(updatedTimeSheet.PeriodStarts!.ToString()), DateTime.Parse(updatedTimeSheet.PeriodEnds!.ToString()));
                _Timesheets[id] = timesheet;
                return ViewTimesheet(id);
            }
            catch
            {
                //TODO: log error
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        private static TimesheetViewModel CreateDemoTimesheet(int id, string name, string customer, string employee, DateTime periodEnds, DateTime periodStarts)
        {
            var entries = new List<TimesheetEntryViewModel>();

            for (int day = 1; day <= 7; day++)
            {
                for (int hour = 8; hour <= 12; hour++)
                {
                    entries.Add(new TimesheetEntryViewModel(0, Models.DayOfTheWeek.FromNumber(day), hour, _WorkTypes[5]));
                }
            }

            var ts = new TimesheetViewModel(id, name, customer, employee, periodEnds, periodStarts, entries, _WorkTypes);

            return ts;
        }
    }
}