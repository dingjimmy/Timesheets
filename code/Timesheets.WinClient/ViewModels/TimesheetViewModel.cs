using System;
using System.Collections.Generic;
using Timesheets.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Timesheets.WinClient
{
    public class TimesheetViewModel
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public string Customer { get; set; }

        public string Employee { get; set; }

        public DateTime PeriodStarts { get; set; }

        public DateTime PeriodEnds { get; set; }

        public ICollection<TimesheetEntryViewModel> Entries { get; set; }
    }

    public static class TimesheetExtenstions
    {
        public static TimesheetViewModel AsViewModel(this Timesheet ts)
        {
            var vm = new TimesheetViewModel()
            {
                ID = ts.ID,
                Name = ts.Name,
                Employee = ts.Employee,
                Customer = ts.Customer,
                PeriodStarts = ts.PeriodStarts,
                PeriodEnds = ts.PeriodEnds
            };

            return vm;
        }
    }
}
