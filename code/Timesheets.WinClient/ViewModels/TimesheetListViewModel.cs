﻿using System;
using Caliburn.Micro;
using Timesheets.Data;
using Timesheets.WinClient.Messages;

namespace Timesheets.WinClient
{

    public interface ITimesheetList : IScreen
    {
        BindableCollection<TimesheetSummaryViewModel> Timesheets { get; set; }

        TimesheetSummaryViewModel SelectedTimesheet { get; set; }

    }

    public class TimesheetListViewModel : Screen, ITimesheetList
    {
        private ITimesheetDbContext data;
        private IEventAggregator messages;
        private TimesheetSummaryViewModel selectedTimesheet;

        public BindableCollection<TimesheetSummaryViewModel> Timesheets { get; set; }

        public TimesheetSummaryViewModel SelectedTimesheet
        {
            get
            {
                return selectedTimesheet;
            }
            set
            {
                this.selectedTimesheet = value;
                NotifyOfPropertyChange(() => SelectedTimesheet);
                NotifyOfPropertyChange(() => CanSelectTimesheet);
            }
        }

        public TimesheetListViewModel(ITimesheetDbContext data, IEventAggregator msgs)
        {
            if (data == null) throw new ArgumentNullException(nameof(data));

            this.data = data;
            this.messages = msgs;

            this.Timesheets = new BindableCollection<TimesheetSummaryViewModel>();

        }

        public void AddTimesheet()
        {
            this.messages.PublishOnUIThread(new AddTimesheetMessage());
        }

        public bool CanAddTimesheet
        {
            get
            {
                return true;
            }
        }

        public void SelectTimesheet()
        {
            this.messages.PublishOnUIThread(new SelectTimesheetMessage(this.selectedTimesheet.ID));
        }

        public bool CanSelectTimesheet
        {
            get
            {
                //return this.selectedTimesheet != null;
                return true;
            }
        }

    }
}