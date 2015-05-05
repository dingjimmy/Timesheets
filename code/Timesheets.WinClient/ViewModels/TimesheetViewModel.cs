using System;
using System.Collections.Generic;
using Timesheets.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Timesheets.WinClient
{
    public class TimesheetViewModel
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public string Customer { get; set; }

        public string Employee { get; set; }

        public DateTime PeriodStarts { get; set; }

        public DateTime PeriodEnds { get; set; }

        public ICollection<TimesheetEntryViewModel> Entries { get; set; }


        #region Constructors

        public TimesheetViewModel()
        {
            
        }

        #endregion


        #region Action Methods


        public void AddEntry()
        {
            System.Windows.MessageBox.Show("Add an Entry");
        }

        public bool CanAddEntry{ get; set; }


        public void RemoveEntry()
        {
            System.Windows.MessageBox.Show("Remove an Entry");
        }

        public bool CanRemoveEntry { get; set; }


        public void Print()
        {
            System.Windows.MessageBox.Show("Print Timesheet");
        }

        public bool CanPrint { get; set; }


        public void Edit()
        {
            System.Windows.MessageBox.Show("Edit Timesheet");
        }

        public bool CanEdit { get; set; }


        #endregion
    }
}
