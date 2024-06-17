using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;

namespace Timesheets.Web.Controllers
{

    public class TimesheetController : Controller
    {
        private static readonly TimesheetValidator _Validator = new();

        static readonly Dictionary<int, Timesheet> _Timesheets = new()
        {
            { 1001, new Timesheet(1001, "2024 Week 5", "HM Government", "Winston Churchill", DateTime.Now.ToString(), DateTime.Now.AddDays(1).ToString()) },
            { 1002, new Timesheet(1002, "2024 Week 6", "HM Government", "Winston Churchill", DateTime.Now.ToString(), DateTime.Now.AddDays(1).ToString()) },
            { 1003, new Timesheet(1003, "2024 Week 7", "HM Government", "Winston Churchill", DateTime.Now.ToString(), DateTime.Now.AddDays(1).ToString()) }
        };

        [HttpGet("timesheets")]
        public IActionResult ListTimesheets()
        {
            return View();
        }

        [HttpGet("timesheet")]
        public ActionResult NewTimesheet()
        {
            return View();
        }

        [HttpGet("timesheet/{id}")]
        public ActionResult ViewTimesheet(int id, bool? edit)
        {
            if (!_Timesheets.TryGetValue(id, out Timesheet? timesheet))
            {
                return NotFound();
            }

            var viewToReturn = edit == true ? "EditTimesheet" : "ViewTimesheet";
            return View(viewToReturn, timesheet);
        }

        [HttpPost("timesheet")]
        public ActionResult SaveNewTimesheet(AddTimesheetRequest request)
        {
            if (!_Validator.IsValid(request.Name, request.Customer, request.Employee, request.PeriodStarts, request.PeriodEnds))
            {
                return BadRequest();
            }

            try
            {
                var nextId = _Timesheets.Keys.Last() + 1;
                var timesheet = new Timesheet(nextId, request.Name, request.Customer, request.Employee, request.PeriodStarts, request.PeriodEnds);
                _Timesheets.Add(nextId, timesheet);

                return ViewTimesheet(timesheet.ID, false);
            }
            catch
            {
                //TODO: log error
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPut("timesheet/{id}")]
        public ActionResult SaveEditedTimesheet(int id, UpdateTimesheetRequest request)
        {
            if (!_Validator.IsValid(request.Name, request.Customer, request.Employee, request.PeriodStarts, request.PeriodEnds))
            {
                return BadRequest();
            }

            if (!_Timesheets.ContainsKey(id))
            {
                return NotFound();
            }

            try
            {
                var timesheet = new Timesheet(id, request.Name, request.Customer, request.Employee, request.PeriodStarts, request.PeriodEnds);
                _Timesheets[id] = timesheet;

                return ViewTimesheet(id, false);
            }
            catch
            {
                //TODO: log error
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}