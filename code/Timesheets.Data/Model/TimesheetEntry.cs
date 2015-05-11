using System;

namespace Timesheets.Data.Model
{
    public class TimesheetEntry
    {
        public int ID { get; set; }

        public DateTime StartedOn { get; set; }

        public DateTime CompletedOn { get; set; }

        public string Description { get; set; }
    }
}
