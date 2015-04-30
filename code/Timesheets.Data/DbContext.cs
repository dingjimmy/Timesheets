using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Collections;
using System.Linq.Expressions;
using System.Collections.ObjectModel;

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


    public class FakeTimehsheetDbContext : ITimesheetDbContext
    {
        private FakeDbSet<TimesheetEntry> entries;
        private FakeDbSet<Timesheet> timesheets;
        

        public IDbSet<TimesheetEntry> Entries
        {
            get { return this.entries; }
            set { this.entries = (FakeDbSet<TimesheetEntry>)value; }
        }

        public IDbSet<Timesheet> Timesheets
        {
            get { return this.timesheets; }
            set { this.timesheets = (FakeDbSet<Timesheet>)value; }
        }

        public FakeTimehsheetDbContext()
        {
            this.timesheets = new FakeDbSet<Timesheet>();
            this.entries = new FakeDbSet<TimesheetEntry>();
        }

        public int SaveChanges()
        {
            // do nothing for now. 
            return 0;
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).          
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources. 
        // ~FakeTimehsheetDbContext() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion

    }
}
