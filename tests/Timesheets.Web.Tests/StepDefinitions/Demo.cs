using System.Text.RegularExpressions;
using Microsoft.Playwright;
using Reqnroll;

namespace Timesheets.Tests.StepDefinitions;

[Binding]
public class Demo(IPage page) : PlaywrightStepDefinitions(page)
{
    [Given("user has navigated to playwright home page")]
    public async Task GivenUserHasNavigatedToPlaywrightHomePage()
    {
        await Page.GotoAsync("https://playwright.dev");
    }

    [When("they click on the 'get started' link")]
    public async Task WhenTheyClickOnTheGetStartedLink()
    {
        // Click the get started link.
        await Page.GetByRole(AriaRole.Link, new() { Name = "Get started" }).ClickAsync();
    }

    [Then("the page title must contain the word 'playwright'")]
    public async Task ThenThePageTitleMustContainTheWordPlaywright()
    {
        // Expect a title "to contain" a substring.
        await Expect(Page).ToHaveTitleAsync(new Regex("Playwright"));
    }

    [Then("the 'Installation' page is loaded")]
    public async Task ThenTheInstallationPageIsLoaded()
    {
        // Expects page to have a heading with the name of Installation.
        await Expect(Page.GetByRole(AriaRole.Heading, new() { Name = "Installation" })).ToBeVisibleAsync();
    }
}