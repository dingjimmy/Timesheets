// ReSharper disable MemberCanBeProtected.Global
using Microsoft.Playwright;

namespace Timesheets.Tests.StepDefinitions;

public class PlaywrightStepDefinitions
{
    public IPage Page { get; }
    
    public static void SetDefaultExpectTimeout(float timeout) => Assertions.SetDefaultExpectTimeout(timeout);
    
    public ILocatorAssertions Expect(ILocator locator) => Assertions.Expect(locator);

    public IPageAssertions Expect(IPage page) => Assertions.Expect(page);

    public IAPIResponseAssertions Expect(IAPIResponse response) => Assertions.Expect(response);

    protected PlaywrightStepDefinitions(IPage page)
    {
        Page = page;
    }
}