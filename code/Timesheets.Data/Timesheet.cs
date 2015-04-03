using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Timesheets.Data
{
    public class Timesheet
    {
        public int ID { get; set; }

        public string Customer { get; set; }

        public string Employee { get; set; }

        public DateTime PeriodStarts { get; set; }

        public DateTime PeriodEnds { get; set; }

        public ICollection<TimesheetEntry> Entries { get; set; }
        
    }
}
