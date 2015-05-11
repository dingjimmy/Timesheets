using System;
using System.Collections.Generic;

namespace Timesheets.Data.Model
{
    public class Timesheet
    {
        public int ID { get; set; }

        public string Name { get; set; }
        
        public string Customer { get; set; }

        public string Employee { get; set; }

        public DateTime PeriodStarts { get; set; }

        public DateTime PeriodEnds { get; set; }

        public ICollection<TimesheetEntry> Entries { get; set; }

        public Timesheet()
        {
            this.Entries = new List<TimesheetEntry>();
        }
        
    }
}
