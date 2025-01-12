using Microsoft.AspNetCore.Mvc;
using Timesheets.Domain;
using Timesheets.Web.Models.Timesheet.Organisms;
using NewTimesheetModel = Timesheets.Web.Models.Timesheet.Organisms.NewTimesheetModel;

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

        static readonly Dictionary<int, ViewTimesheetModel> _Timesheets = new()
        {
            { 1001, CreateDemoTimesheet(1001, "2024 Week 5", "HM Government", "Winston Churchill", DateTime.Now, DateTime.Now.AddDays(7)) },
            { 1002, CreateDemoTimesheet(1002, "2024 Week 6", "HM Government", "Winston Churchill", DateTime.Now, DateTime.Now.AddDays(1)) },
            { 1003, CreateDemoTimesheet(1003, "2024 Week 7", "HM Government", "Winston Churchill", DateTime.Now, DateTime.Now.AddDays(1)) },
            { 1004, CreateDemoTimesheet(1004, "2024 Week 8", "HM Government", "Winston Churchill", DateTime.Now, DateTime.Now.AddDays(1)) },
            { 1005, CreateDemoTimesheet(1005, "2024 Week 9", "HM Government", "Winston Churchill", DateTime.Now, DateTime.Now.AddDays(1)) },
            { 1006, CreateDemoTimesheet(1006, "2024 Week 10", "HM Government", "Winston Churchill", DateTime.Now, DateTime.Now.AddDays(1)) },
            { 1007, CreateDemoTimesheet(1007, "2024 Week 11", "HM Government", "Winston Churchill", DateTime.Now, DateTime.Now.AddDays(1)) },
            { 1008, CreateDemoTimesheet(1008, "2024 Week 12", "HM Government", "Winston Churchill", DateTime.Now, DateTime.Now.AddDays(1)) },
            { 1009, CreateDemoTimesheet(1009, "2024 Week 13", "HM Government", "Winston Churchill", DateTime.Now, DateTime.Now.AddDays(1)) }
        };


        [HttpGet("timesheets")]
        public IActionResult ListTimesheets()
        {
            if (IsPartialRequest())
            {
                PushHistoryUrl(Url.Action("ListTimesheets"));
                return PartialView("Organisms/Timesheets", _Timesheets.Values);
            }
            else
            {
                return View("Organisms/Timesheets", _Timesheets.Values);
            }
        }

        [HttpGet("timesheets/new")]
        public ActionResult NewTimesheet()
        {
            if (IsPartialRequest())
            {
                PushHistoryUrl(Url.Action("NewTimesheet"));
                return PartialView("Organisms/NewTimesheet", new NewTimesheetModel());
            }
            else
            {
                return View("Organisms/Timesheets", _Timesheets.Values);
            }
        }

        [HttpPost("timesheets/new")]
        public ActionResult SaveNewTimesheet(NewTimesheetModel newTimeSheet)
        {
            // validate inputs
            if (!ModelState.IsValid)
            {                
                return PartialView("Organisms/NewTimesheet", newTimeSheet);
            }

            // validate state
            var result = CanCreateTimesheet(newTimeSheet);
            if (result.IsFailure)
            { 
                ModelState.AddModelError("", result.Error);
                return PartialView("Organisms/NewTimesheet", newTimeSheet);
            }

            // create timesheet
            try
            {
                var timesheet = CreateTimesheet(newTimeSheet.Name.Value!, newTimeSheet.Customer.Value!, newTimeSheet.Employee.Value!, newTimeSheet.PeriodStarts.Value, newTimeSheet.PeriodEnds.Value);

                return ViewTimesheet(timesheet.ID);
            }
            catch
            {
                //TODO: log error
                ModelState.AddModelError("", "An error occurred while creating the timesheet.");
                return PartialView("Organisms/NewTimesheet", newTimeSheet);
            }
        }

        [HttpGet("timesheets/{id}")]
        public ActionResult ViewTimesheet(int id)
        {
            if (!_Timesheets.TryGetValue(id, out ViewTimesheetModel? timesheet))
            {
                return NotFound();
            }

            //if (!IsPartialRequest())
            //{
            //    return View("Organisms/Timesheets", _Timesheets.Values, timesheet);
            //}
            //else
            //{
                PushHistoryUrl(Url.Action("ViewTimesheet", new { id }));
                return PartialView("Organisms/ViewTimesheet", timesheet);
            //}
        }

        [HttpPut("timesheets/{id}")]
        public ActionResult SaveEditedTimesheet(int id, EditTimesheetModel updatedTimeSheet)
        {
            if (IsPartialRequest())
            {
                return BadRequest();
            }

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

        /// <summary>
        /// Determines if the current request is for a partial page or full page.
        /// </summary>
        /// <remarks>
        /// Uses the 'HX-Request' header to determine if the request was triggered by HTMX, which 
        /// indicates a partial request.
        /// </remarks>
        private bool IsPartialRequest()
        {
            return Request.Headers.ContainsKey("HX-Request");
        }

        /// <summary>
        /// Pushes a URL into the browser's location history.
        /// </summary>
        /// <remarks>
        /// Uses the 'HX-Push-Url' resonse header to tell HTMX on the browser to push a URL into the browser's 
        /// location history. This creates a new history entry, allowing navigation with the browser�s back and 
        /// forward buttons. 
        /// </remarks>
        private void PushHistoryUrl(string? url)
        {
            Response.Headers.Append("HX-Push-Url", url);
        }

        private static ViewTimesheetModel CreateDemoTimesheet(int id, string name, string customer, string employee, DateTime periodStarts, DateTime periodEnds)
        {
            var entries = new List<TimesheetEntryViewModel>();

            for (int day = 1; day <= 7; day++)
            {
                for (int hour = 8; hour <= 12; hour++)
                {
                    entries.Add(new TimesheetEntryViewModel(0, DayOfTheWeek.FromNumber(day), hour, _WorkTypes[5]));
                }
            }

            var ts = new ViewTimesheetModel(id, name, customer, employee, periodEnds, periodStarts, entries, _WorkTypes);

            return ts;
        }


        private static Result CanCreateTimesheet(NewTimesheetModel newTimeSheet)
        {
            return Result.Success();
        }

        private static ViewTimesheetModel CreateTimesheet(string name, string customer, string employee, string periodEnds, string periodStarts)
        {
            var nextId = _Timesheets.Keys.Last() + 1;

            var timesheet =  CreateDemoTimesheet(nextId, name, customer, employee, DateTime.Parse(periodEnds), DateTime.Parse(periodStarts));

            _Timesheets.Add(nextId, timesheet);

            return timesheet;
        }
    }
}