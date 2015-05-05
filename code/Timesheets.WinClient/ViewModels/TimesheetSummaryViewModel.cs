using Timesheets.Data;

namespace Timesheets.WinClient
{
    public class TimesheetSummaryViewModel
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

        #endregion


        #region Action Methods


        #endregion
    }
}
