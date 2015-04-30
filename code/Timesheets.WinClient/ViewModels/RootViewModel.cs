using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Windows;
using Timesheets.Data;

namespace Timesheets.WinClient
{
    public class RootViewModel : Conductor<TimesheetViewModel>
    {

#region Fields

        private ITimesheetDbContext dbContext;
        private IWindowManager winManager;

#endregion
        
#region Data

        public BindableCollection<TimesheetViewModel> Timesheets { get; set; }

        public TimesheetViewModel SelectedTimesheet {
            get
            {
                return selectedTimesheet;
            }
            set
            {
                this.selectedTimesheet = value;
                NotifyOfPropertyChange(() => SelectedTimesheet);
                NotifyOfPropertyChange(() => CanViewOrAmendTimesheet);
            }
        }
        private TimesheetViewModel selectedTimesheet;

#endregion

#region Constructors

        public RootViewModel(ITimesheetDbContext dbCtx, IWindowManager winMgr)
        {
            if (dbCtx == null) throw new ArgumentNullException(nameof(dbCtx));
            if (winMgr == null) throw new ArgumentNullException(nameof(winMgr));

            this.dbContext = dbCtx;
            this.winManager = winMgr;

            this.Timesheets = new BindableCollection<TimesheetViewModel>();

            foreach (var ts in dbContext.Timesheets)
            {
                this.Timesheets.Add(ts.AsViewModel());           
            }
        }

#endregion

#region Actions

        public void CreateTimesheet()
        {
            this.winManager.ShowWindow(new TimesheetViewModel());
        }

        public bool CanCreateTimesheet
        {
            get
            {
                return true;
            }
        }

        public void ViewOrAmendTimesheet()
        {
            this.winManager.ShowWindow(this.selectedTimesheet);
        }

        public bool CanViewOrAmendTimesheet
        {
            get
            {
                return this.selectedTimesheet != null;
            }
        }

#endregion

    }
}
