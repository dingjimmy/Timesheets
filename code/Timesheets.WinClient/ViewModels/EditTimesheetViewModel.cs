using System;
using System.Collections.Generic;
using Timesheets.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Timesheets.WinClient
{
    public class EditTimesheetViewModel
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public string Customer { get; set; }

        public DateTime PeriodStarts { get; set; }

        public DateTime PeriodEnds { get; set; }


        #region Constructors

        public EditTimesheetViewModel(ITimesheetDbContext dbContext)
        {
            CanSave = true;
            CanRemove = true;
        }

        #endregion


        #region Action Methods


        public void Save()
        {
            //System.Windows.MessageBox.Show("Save Timesheet");
        }

        public bool CanSave { get; set; }

        public void Remove()
        {
            System.Windows.MessageBox.Show("Remove Timesheet.");
        }

        public bool CanRemove { get; set; }


        #endregion

    }
}
