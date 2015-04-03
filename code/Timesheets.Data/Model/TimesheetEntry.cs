using System;

namespace Timesheets.Data
{
    public class TimesheetEntry
    {
        public int ID { get; set; }

        public DateTime StartedOn { get; set; }

        public DateTime CompletedOn { get; set; }

        public string Description { get; set; }
    }
}
