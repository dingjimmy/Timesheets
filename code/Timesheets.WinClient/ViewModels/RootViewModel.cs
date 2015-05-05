using System;
using Caliburn.Micro;
using Timesheets.Data;

namespace Timesheets.WinClient
{
    public class RootViewModel : Conductor<TimesheetSummaryViewModel>
    { 
        private ITimesheetDbContext dbContext;
        private IWindowManager winManager;
        private TimesheetSummaryViewModel selectedTimesheet;

        public BindableCollection<TimesheetSummaryViewModel> Timesheets { get; set; }

        public TimesheetSummaryViewModel SelectedTimesheet {
            get
            {
                return selectedTimesheet;
            }
            set
            {
                this.selectedTimesheet = value;
                NotifyOfPropertyChange(() => SelectedTimesheet);
                NotifyOfPropertyChange(() => CanViewTimesheet);
            }
        }
        
        public RootViewModel(ITimesheetDbContext dbCtx, IWindowManager winMgr)
        {
            if (dbCtx == null) throw new ArgumentNullException(nameof(dbCtx));
            if (winMgr == null) throw new ArgumentNullException(nameof(winMgr));

            this.dbContext = dbCtx;
            this.winManager = winMgr;

            this.Timesheets = new BindableCollection<TimesheetSummaryViewModel>();

            foreach (var ts in dbContext.Timesheets)
            {
                this.Timesheets.Add(ts.AsSummaryViewModel());           
            }
        }

        public void CreateTimesheet()
        {
            this.ActivateItem(new EditTimesheetViewModel(dbContext)
            {
                Name = "New Timesheet",
                Customer = "A Customer",
                PeriodStarts = DateTime.Today,
                PeriodEnds = DateTime.Today.AddMonths(1)
            } );
        }

        public bool CanCreateTimesheet
        {
            get
            {
                return true;
            }
        }

        public void ViewTimesheet()
        {
            this.winManager.ShowWindow(this.selectedTimesheet);
        }

        public bool CanViewTimesheet
        {
            get
            {
                return this.selectedTimesheet != null;
            }
        }

    }
}
