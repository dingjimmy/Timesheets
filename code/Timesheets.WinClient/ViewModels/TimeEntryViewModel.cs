using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Timesheets.WinClient
{
    public class TimesheetEntryViewModel
    {
        public int ID { get; set; }

        public DateTime StartedOn { get; set; }

        public DateTime CompletedOn { get; set; }

        public string Description { get; set; }
    }
}
