using System;
using System.Collections.Generic;
using System.Linq;
using Caliburn.Micro;
using Timesheets.Data;

namespace Timesheets.WinClient
{
    public class TimesheetSummaryViewModel : PropertyChangedBase
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public string Customer { get; set; }

        public int Hours { get; set; }

        public int  Minutes { get; set; }


        #region Constructors

        public TimesheetSummaryViewModel()
        {
        }

        public TimesheetSummaryViewModel(Timesheet model)
        {
            var duration = TotalDuration(model.Entries);

            this.ID = model.ID;
            this.Name = model.Name;
            this.Customer = model.Customer;
            this.Hours = duration.Hours;
            this.Minutes = duration.Minutes;
        }

        #endregion


        #region Helper Methods

        private TimeSpan TotalDuration(IEnumerable<TimesheetEntry> entries)
        {
            var total = new TimeSpan();

            foreach (var entry in entries)
            {
                var duration = entry.CompletedOn.Subtract(entry.StartedOn);
                total += duration;
            }

            return total;
        }

        #endregion
    }
}
