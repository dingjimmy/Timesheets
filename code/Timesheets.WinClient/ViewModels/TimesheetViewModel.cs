using System;
using System.Collections.Generic;
using System.Linq;
using Caliburn.Micro;
using Timesheets.Data;
using Timesheets.WinClient.Messages;

namespace Timesheets.WinClient
{
    public class TimesheetViewModel : Screen
    {

        #region Dependancies

        private IEventAggregator messages;

        #endregion


        #region Data Properties 

        public int ID
        {
            get { return id; }
            set
            {
                id = value;
                NotifyOfPropertyChange(() => ID);
            }
        }
        private int id;

        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                NotifyOfPropertyChange(() => Name);
            }
        }
        private string name;

        public string Customer
        {
            get { return customer; }
            set
            {
                customer = value;
                NotifyOfPropertyChange(() => Customer);
            }
        }
        private string customer;

        public DateTime PeriodStarts
        {
            get { return starts; }
            set
            {
                starts = value;
                NotifyOfPropertyChange(() => PeriodStarts);
            }
        }
        private DateTime starts;
     
        public DateTime PeriodEnds
        {
            get { return ends; }
            set
            {
                ends = value;
                NotifyOfPropertyChange(() => PeriodEnds);
            }
        }   
        private DateTime ends;

        public int Hours
        {
            get { return hours; }
            set
            {
                hours = value;
                NotifyOfPropertyChange(() => Hours);
            }
        }
        private int hours;

        public int Minutes
        {
            get { return minutes; }
            set
            {
                minutes = value;
                NotifyOfPropertyChange(() => Minutes);
            }
        }
        private int minutes;

        public TimesheetState State
        {
            get {

                return state;
            }
            set
            {
                state = value;
                NotifyOfPropertyChange(() => State);
            }
        }
        private TimesheetState state;


        #endregion


        #region Guard Properties

        // public bool CanSave { get; set; }
        // public bool CanRemove { get; set; }
        // public bool CanAddEntry{ get; set; }
        // public bool CanRemoveEntry { get; set; }
        // public bool CanPrint { get; set; }
        // public bool CanEdit { get; set; }

        #endregion


        #region Constructors

        public TimesheetViewModel(IEventAggregator msgs)
        {
            if (msgs == null) throw new ArgumentNullException(nameof(msgs));

            this.messages = msgs;
        }

        public TimesheetViewModel(Data.Model.Timesheet model, IEventAggregator msgs)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));
            if (msgs == null) throw new ArgumentNullException(nameof(msgs));

            this.messages = msgs;

            var duration = TotalDuration(model.Entries);

            this.ID = model.ID;
            this.Name = model.Name;
            this.Customer = model.Customer;
            this.PeriodStarts = model.PeriodStarts;
            this.PeriodEnds = model.PeriodEnds;

            this.Hours = duration.Hours;
            this.Minutes = duration.Minutes;

        }

        #endregion


        #region Action Methods

        public void Save()
        {
            this.messages.PublishOnUIThread(new SaveTimesheetMessage());
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

    public enum  TimesheetState
    {
        Detail,
        Edit
    }
}
