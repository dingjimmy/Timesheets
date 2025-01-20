using Microsoft.Playwright;
using Microsoft.Playwright.TestAdapter;
using Reqnroll;
using Reqnroll.BoDi;

namespace Timesheets.Tests.StepDefinitions;

[Binding]
public class PlaywrightHooks
{
    private readonly IObjectContainer _Container;
    
    public PlaywrightHooks(IObjectContainer container)
    {
        _Container = container;
    }

    [BeforeFeature]
    public static async Task SetupBrowser(FeatureContext featureContext)
    {
        // setup playwright
        var playwright = await Playwright.CreateAsync();
        playwright.Selectors.SetTestIdAttribute("data-testid");
        var browserName = PlaywrightSettingsProvider.BrowserName;
        featureContext["Playwright"] = playwright;
        
        // setup browser
        var browser = await playwright[browserName].LaunchAsync();
        featureContext["Browser"] = browser;
    }

    [BeforeScenario]
    public async Task SetupBrowserContext(FeatureContext featureContext)
    {
        var browser = featureContext["Browser"] as IBrowser;
        var browserContext = await browser!.NewContextAsync();
        
        var page = await browserContext.NewPageAsync();
        _Container.RegisterInstanceAs(page);
    }

    [AfterFeature]
    public static async Task TearDownBrowser(FeatureContext featureContext)
    {
        if (featureContext["Browser"] is IBrowser browser)
        {
            foreach (var context in browser.Contexts)
            {
                await context.CloseAsync().ConfigureAwait(false);
            }
            
            await browser.CloseAsync().ConfigureAwait(false);
            featureContext["Browser"] = null;
        }

        if (featureContext["Playwright"] is IPlaywright playwright)
        {
            playwright.Dispose();
            featureContext["Playwright"] = null;
        }
    }
}