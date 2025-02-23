using System.Diagnostics;
using System.Reflection;

namespace Timesheets.Web.Models.Shared;

public class FooterViewModel
{
    public string CurrentYear => SiteInfo.CurrentYear;
    public string BuildNumber => SiteInfo.BuildNumber;

}

public static class SiteInfo
{
    public static string CurrentYear { get; }
    public static string BuildNumber { get; }

    static SiteInfo()
    {
        CurrentYear = DateTime.Now.Year.ToString();

        var version = FileVersionInfo
            .GetVersionInfo(typeof(FooterViewModel).Assembly!.Location)
            .ProductVersion ?? "1.0.0+197001010000";
        BuildNumber = version.Split('+', StringSplitOptions.TrimEntries)[1];
    }  
}