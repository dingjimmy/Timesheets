using System;
using Caliburn.Micro;
using Timesheets.Data;

namespace Timesheets.WinClient
{
    public class EditorScreenViewModel : Screen
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public string Customer { get; set; }

        public DateTime PeriodStarts { get; set; }

        public DateTime PeriodEnds { get; set; }


        #region Constructors

        public EditorScreenViewModel()
        {
            CanSave = true;
            CanRemove = true;
        }

        public EditorScreenViewModel(Timesheet model)
        {
            this.ID = model.ID;
            this.Name = model.Name;
            this.Customer = model.Customer;
            this.PeriodStarts = model.PeriodStarts;
            this.PeriodEnds = model.PeriodEnds;

            CanSave = true;
            CanRemove = true;
        }

        #endregion


        #region Action Methods


        public void Save()
        {
            
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
