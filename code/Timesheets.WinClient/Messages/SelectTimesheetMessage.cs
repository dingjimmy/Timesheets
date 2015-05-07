namespace Timesheets.WinClient.Messages
{
    public class SelectTimesheetMessage
    {
        public int TimesheetID { get; private set; }

        public SelectTimesheetMessage(int timesheetId)
        {
            this.TimesheetID = timesheetId;
        }
    }
}
