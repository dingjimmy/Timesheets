using Caliburn.Micro;
using System.Windows;
using Timesheets.Data;

namespace Timesheets.WinClient
{
    public class RootViewModel
    {

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

        public RootViewModel(ITimesheetDbContext dbContext)
        {
            var ctx = dbContext;
        }

    }
}
