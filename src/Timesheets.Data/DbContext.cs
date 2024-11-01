using Microsoft.EntityFrameworkCore;
using Timesheets.Data.Model;

namespace Timesheets.Data
{
    public interface ITimesheetDbContext : IDisposable
    {
        DbSet<Timesheet> Timesheets { get; set; }

        DbSet<TimesheetEntry> Entries { get; set; }

        int SaveChanges();
    }

    public class TimesheetDbContext : DbContext, ITimesheetDbContext
    {
        public DbSet<Timesheet> Timesheets { get; set; }

        public DbSet<TimesheetEntry> Entries { get; set; }

        public TimesheetDbContext(DbContextOptions<TimesheetDbContext> options) : base(options)
        {
        }

    }
}