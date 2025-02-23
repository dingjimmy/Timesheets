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
        var copyrightNotice = $"\u00a9 {DateTime.Now.Year} Timesheets";
        
        await Expect(Page.GetByText(copyrightNotice))
            .ToHaveCountAsync(1);
    }

    [Then("the correct build number is displayed")]
    public async Task ThenTheCorrectBuildNumberIsDisplayed()
    {
        var version = FileVersionInfo
            .GetVersionInfo(Assembly.GetAssembly(typeof(SmokeTestSteps))!.Location)
            .ProductVersion;
        var buildNumber = version?.Split('+', StringSplitOptions.TrimEntries)[1];

        await Expect(Page.GetByText($"Build {buildNumber}"))
            .ToHaveCountAsync(1);
    }
}