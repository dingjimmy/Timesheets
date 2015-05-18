using System;
using Caliburn.Micro;
using Ninject;
using Timesheets.Data;
using Timesheets.WinClient.Messages;

namespace Timesheets.WinClient
{
    public class ShellViewModel : Conductor<IScreen>, IHandle<AddTimesheetMessage>, IHandle<SelectTimesheetMessage>, IHandle<SaveTimesheetMessage>, IHandle<RemoveTimesheetMessage>, IHandle<EditTimesheetMessage>
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
            var vm = ninject.Get<TimesheetViewModel>();

            vm.ID = ts.ID;
            vm.Name = ts.Name;
            vm.Customer = ts.Customer;
            vm.PeriodStarts = ts.PeriodStarts;
            vm.PeriodEnds = ts.PeriodEnds;
            vm.State = TimesheetState.Edit;

            this.TimesheetList.Timesheets.Add(vm);

            ActivateItem(vm);
        }

        public void Handle(SelectTimesheetMessage message)
        {
            ActivateItem(this.TimesheetList.SelectedTimesheet);           
        }

        public void Handle(SaveTimesheetMessage message)
        {
            var vm = this.TimesheetList.SelectedTimesheet;
            var ts = data.Timesheets.Find(vm.ID);

            ts.Name = vm.Name;
            ts.Customer = vm.Customer;
            ts.PeriodStarts = vm.PeriodStarts;
            ts.PeriodEnds = vm.PeriodEnds;

            data.SaveChanges();

            vm.State = TimesheetState.Detail;

        }

        public void Handle(EditTimesheetMessage message)
        {
            this.TimesheetList.SelectedTimesheet.State = TimesheetState.Edit;
        }

        public void Handle(RemoveTimesheetMessage message)
        {
            var vm = this.TimesheetList.SelectedTimesheet;

            var ts = data.Timesheets.Find(vm.ID);
            data.Timesheets.Remove(ts);

            this.DeactivateItem(vm, true);

            this.TimesheetList.SelectedTimesheet = null;
            this.TimesheetList.Timesheets.Remove(vm);


            data.SaveChanges();
        }

        public Data.Model.Timesheet CreateBlankTimesheet()
        {
            var ts = new Data.Model.Timesheet()
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
