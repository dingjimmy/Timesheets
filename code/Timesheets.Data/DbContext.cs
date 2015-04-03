using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Timesheets.Data
{
    public interface ITimesheetDbContext : IDisposable
    {
        IDbSet<Timesheet> Timesheets { get; set; }

        IDbSet<TimesheetEntry> Entries { get; set; }

        int SaveChanges();
    }


    public class TimesheetDbContext : DbContext, ITimesheetDbContext
    {
        public IDbSet<Timesheet> Timesheets { get; set; }

        public IDbSet<TimesheetEntry> Entries { get; set; }
    }
}
