using System.Diagnostics;
using System.Reflection;
using System.Text.RegularExpressions;
using Microsoft.Playwright;
using Reqnroll;

namespace Timesheets.Tests.StepDefinitions;

[Binding]
public class SmokeTestSteps(IPage page): PlaywrightStepDefinitions(page)
{
    
    [Given("user has navigated to the Timesheets home page")]
    public async Task GivenUserHasNavigatedToTheTimesheetsHomePage()
    {
        await Page.GotoAsync("/");
    }
    
    [Then("the copyright notice is displayed")]
    public async Task ThenTheCopyrightNoticeIsDisplayed()
    {
        var copyrightNotice = $"\u00a9 {DateTime.Now.Year} - Timesheets";
        
        await Expect(Page.GetByText(copyrightNotice))
            .ToHaveCountAsync(1);
    }

    [Then("the correct deployment number is displayed")]
    public async Task ThenTheCorrectDeploymentVersionIsDisplayed()
    {
        var fileVer = FileVersionInfo.GetVersionInfo(Assembly.GetAssembly(typeof(SmokeTestSteps))!.Location);
        var deploymentVersion = fileVer.ProductBuildPart;

        await Expect(Page.GetByText($"Deployment No: {deploymentVersion}"))
            .ToHaveCountAsync(1);
    }
}