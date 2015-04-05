using Caliburn.Micro;
using System;
using System.Windows;
using Timesheets.Data;

namespace Timesheets.WinClient
{
    public class RootViewModel : PropertyChangedBase
    {
        #region Fields

        private ITimesheetDbContext DbContext;

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
                NotifyOfPropertyChange(() => CanAmendTimesheet);
            }
        }
        private TimesheetViewModel selectedTimesheet;

        #endregion

        #region Constructors

        public RootViewModel(ITimesheetDbContext dbContext)
        {
            if (dbContext == null) throw new ArgumentNullException(nameof(dbContext));
            this.DbContext = dbContext;

            this.Timesheets = new BindableCollection<TimesheetViewModel>()
            {
                new TimesheetViewModel(),
                new TimesheetViewModel(),
                new TimesheetViewModel()
            };
        }

        #endregion

        #region Actions

        public void CreateTimesheet()
        {

        }

        public bool CanCreateTimesheet
        {
            get
            {
                return true;
            }
        }

        public void AmendTimesheet()
        {

        }

        public bool CanAmendTimesheet
        {
            get
            {
                return this.selectedTimesheet != null;
            }
        }

        #endregion

        #region Demo Stuff


        public bool CanSayHello(string givenName, string familyName)
        {
            return !string.IsNullOrWhiteSpace(givenName) && !string.IsNullOrWhiteSpace(familyName);
        }


        public void SayHello(string givenName, string familyName)
        {
            MessageBox.Show($"Hello {givenName} {familyName}!");
        }


        public bool CanSayGoodby(string givenName, string familyName)
        {
            return !string.IsNullOrWhiteSpace(givenName) && !string.IsNullOrWhiteSpace(familyName);
        }

        public void SayGoodby(string givenName, string familyName)
        {
            MessageBox.Show($"Goodby {givenName} {familyName}. :(");
        }


        #endregion

    }
}
