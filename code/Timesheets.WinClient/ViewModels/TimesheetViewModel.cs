using System;
using System.Collections.Generic;
using System.Linq;
using Caliburn.Micro;
using Timesheets.Data;

namespace Timesheets.WinClient
{
    public class TimesheetViewModel : Screen
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public string Customer { get; set; }

        public DateTime PeriodStarts { get; set; }

        public DateTime PeriodEnds { get; set; }

        public int Hours { get; set; }

        public int  Minutes { get; set; }

        public bool CanSave { get; set; }

        public bool CanRemove { get; set; }

        //        public bool CanAddEntry{ get; set; }
        //        public bool CanRemoveEntry { get; set; }
        //        public bool CanPrint { get; set; }
        //        public bool CanEdit { get; set; }


        #region Constructors

        public TimesheetViewModel()
        {
            CanSave = true;
            CanRemove = true;
        }

        public TimesheetViewModel(Data.Model.Timesheet model)
        {
            var duration = TotalDuration(model.Entries);

            this.ID = model.ID;
            this.Name = model.Name;
            this.Customer = model.Customer;
            this.PeriodStarts = model.PeriodStarts;
            this.PeriodEnds = model.PeriodEnds;

            this.Hours = duration.Hours;
            this.Minutes = duration.Minutes;

            CanSave = true;
            CanRemove = true;
        }

        #endregion


        #region Action Methods

        public void Save()
        {

        }

        public void Remove()
        {
            System.Windows.MessageBox.Show("Remove Timesheet.");
        }


        //        public void AddEntry()
        //        {
        //            System.Windows.MessageBox.Show("Add an Entry");
        //        }
        //
        //        public void RemoveEntry()
        //        {
        //            System.Windows.MessageBox.Show("Remove an Entry");
        //        }
        //
        //        public void Print()
        //        {
        //            System.Windows.MessageBox.Show("Print Timesheet");
        //        }
        //
        //        public void Edit()
        //        {
        //            System.Windows.MessageBox.Show("Edit Timesheet");
        //        }


        #endregion


        #region Helper Methods


        private TimeSpan TotalDuration(IEnumerable<Data.Model.TimesheetEntry> entries)
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
