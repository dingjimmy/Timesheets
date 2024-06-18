using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;
using Timesheets.Domain;
using Timesheets.Web.Models;

namespace Timesheets.Web.Controllers
{

    public class TimesheetController : Controller
    {
        private static readonly TimesheetValidator _Validator = new();

        static readonly Dictionary<int, Timesheet> _Timesheets = new()
        {
            { 1001, new Timesheet(1001, "2024 Week 5", "HM Government", "Winston Churchill", DateTime.Now.ToString(), DateTime.Now.AddDays(7).ToString()) },
            { 1002, new Timesheet(1002, "2024 Week 6", "HM Government", "Winston Churchill", DateTime.Now.ToString(), DateTime.Now.AddDays(1).ToString()) },
            { 1003, new Timesheet(1003, "2024 Week 7", "HM Government", "Winston Churchill", DateTime.Now.ToString(), DateTime.Now.AddDays(1).ToString()) },
            { 1004, new Timesheet(1004, "2024 Week 8", "HM Government", "Winston Churchill", DateTime.Now.ToString(), DateTime.Now.AddDays(1).ToString()) },
            { 1005, new Timesheet(1005, "2024 Week 9", "HM Government", "Winston Churchill", DateTime.Now.ToString(), DateTime.Now.AddDays(1).ToString()) },
            { 1006, new Timesheet(1006, "2024 Week 10", "HM Government", "Winston Churchill", DateTime.Now.ToString(), DateTime.Now.AddDays(1).ToString()) },
            { 1007, new Timesheet(1007, "2024 Week 11", "HM Government", "Winston Churchill", DateTime.Now.ToString(), DateTime.Now.AddDays(1).ToString()) },
            { 1008, new Timesheet(1008, "2024 Week 12", "HM Government", "Winston Churchill", DateTime.Now.ToString(), DateTime.Now.AddDays(1).ToString()) }
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

        [HttpGet("timesheet/{id}")]
        public ActionResult ViewTimesheet(int id)
        {
            if (!_Timesheets.TryGetValue(id, out Timesheet? timesheet))
            {
                return NotFound();
            }

            return View(timesheet);
        }

        [HttpGet("timesheet/{id}/edit")]
        public ActionResult EditTimesheet(int id)
        {
            if (!_Timesheets.TryGetValue(id, out Timesheet? timesheet))
            {
                return NotFound();
            }

            return View(timesheet);
        }

        [HttpPost("timesheet")]
        public ActionResult SaveNewTimesheet(AddTimesheetViewModel newTimeSheet)
        {
            if (!_Validator.IsValid(newTimeSheet.Name, newTimeSheet.Customer, newTimeSheet.Employee, newTimeSheet.PeriodStarts, newTimeSheet.PeriodEnds))
            {
                return BadRequest();
            }

            try
            {
                var nextId = _Timesheets.Keys.Last() + 1;
                var timesheet = new Timesheet(nextId, newTimeSheet.Name, newTimeSheet.Customer, newTimeSheet.Employee, newTimeSheet.PeriodStarts, newTimeSheet.PeriodEnds);
                _Timesheets.Add(nextId, timesheet);

                return ViewTimesheet(timesheet.ID);
            }
            catch 
            {
                //TODO: log error
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPut("timesheet/{id}")]
        public ActionResult SaveEditedTimesheet(int id, UpdateTimesheetViewModel updatedTimeSheet)
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
                var timesheet = new Timesheet(id, updatedTimeSheet.Name, updatedTimeSheet.Customer, updatedTimeSheet.Employee, updatedTimeSheet.PeriodStarts, updatedTimeSheet.PeriodEnds);
                _Timesheets[id] = timesheet;

                return ViewTimesheet(id);
            }
            catch
            {
                //TODO: log error
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}