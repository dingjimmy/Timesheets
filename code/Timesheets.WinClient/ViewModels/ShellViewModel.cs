using System;
using Caliburn.Micro;
using Ninject;
using Timesheets.Data;
using Timesheets.WinClient.Messages;

namespace Timesheets.WinClient
{
    public class ShellViewModel : Conductor<IScreen>, IHandle<AddTimesheetMessage>, IHandle<SelectTimesheetMessage>
    { 
        private ITimesheetDbContext data;
        private IWindowManager windows;
        private IEventAggregator messages;
        private IKernel ninject;

        public ITimesheetList TimesheetList { get; set; }


        public ShellViewModel(ITimesheetDbContext data, IWindowManager windows, IEventAggregator msgs, IKernel ioc)
        {
            if (data == null) throw new ArgumentNullException(nameof(data));
            if (windows == null) throw new ArgumentNullException(nameof(windows));
            if (msgs == null) throw new ArgumentNullException(nameof(messages));
            if (ioc == null) throw new ArgumentNullException(nameof(ioc));

            this.data = data;
            this.windows = windows;
            this.messages = msgs;
            this.ninject = ioc;

            this.messages.Subscribe(this);

            this.TimesheetList = ninject.Get<TimesheetListViewModel>();
        }

        public void Handle(AddTimesheetMessage message)
        {
            var ts = CreateBlankTimesheet();

            this.TimesheetList.Timesheets.Add(new TimesheetSummaryViewModel(ts));

            ActivateItem(new EditorScreenViewModel(ts));
        }

        public void Handle(SelectTimesheetMessage message)
        {
            var ts = this.data.Timesheets.Find(message.TimesheetID);

            //ActivateItem(new DetailScreenViewModel(ts));
            ActivateItem(new EditorScreenViewModel(ts));
        }

        public Timesheet CreateBlankTimesheet()
        {
            var ts = new Timesheet()
            {
                Name = "New Timesheet",
                Customer = "A Customer",
                PeriodStarts = DateTime.Today,
                PeriodEnds = DateTime.Today.AddMonths(1)
            };

            data.Timesheets.Add(ts);
            data.SaveChanges();

            ts.Name = string.Format("New Timesheet {0}", ts.ID);
            data.SaveChanges();

            return ts;
        }

    }
}
