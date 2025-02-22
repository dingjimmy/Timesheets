using Microsoft.Playwright;
using Microsoft.Playwright.TestAdapter;
using Reqnroll;
using Reqnroll.BoDi;

namespace Timesheets.Tests.StepDefinitions;

[Binding]
public class PlaywrightHooks
{
    private const string _BaseUrlLocalDev = "https://localhost:7133";
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
        var options = PlaywrightSettingsProvider.LaunchOptions;
        var browser = await playwright[browserName].LaunchAsync(options);
        featureContext["Browser"] = browser;
    }

    [BeforeScenario]
    public async Task SetupBrowserContext(FeatureContext featureContext)
    {
        var browser = featureContext["Browser"] as IBrowser;
        
        var options = new BrowserNewContextOptions()
        {
            BaseURL = Environment.GetEnvironmentVariable("BASE_URL")
        };
        
        var browserContext = await browser!.NewContextAsync(options);
        _Container.RegisterInstanceAs(browserContext);
        
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