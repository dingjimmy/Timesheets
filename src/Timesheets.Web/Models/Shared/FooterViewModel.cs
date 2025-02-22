using System.Diagnostics;
using System.Reflection;

namespace Timesheets.Web.Models.Shared;

public class FooterViewModel
{
    public string DeploymentYear { get; }
    public string? DeploymentNumber { get; }

    public FooterViewModel()
    {
        DeploymentYear = DateTime.Now.Year.ToString();
        DeploymentNumber = FileVersionInfo.GetVersionInfo(Assembly.GetAssembly(typeof(FooterViewModel))!.Location).ProductBuildPart.ToString();
    }
}