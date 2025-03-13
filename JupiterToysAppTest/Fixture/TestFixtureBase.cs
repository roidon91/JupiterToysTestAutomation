using JupiterToysTestFramework.Config;
using JupiterToysTestFramework.Driver;
using Microsoft.Playwright;
using Xunit.Abstractions;

namespace JupiterToysAppTest.Fixture;

public interface ITestFixtureBase
{
    void SetOutputHelper(ITestOutputHelper outputHelper);
    Task NavigateToUrl(string? url = null);
    Task TakeScreenshotAsync(string testName, string fileName);
    Task WaitForPageToLoadAsync();
    void Log(string message);
}

public class TestFixtureBase(IPlaywrightDriver playwrightDriver, TestSettings testSettings)
    : ITestFixtureBase
{
    private readonly Task<IPage> _page = playwrightDriver.Page;

    private ITestOutputHelper? _outputHelper;
    private readonly string _screenshotFolder = Path.Combine(Directory.GetCurrentDirectory(), "Screenshots");

    public async Task NavigateToUrl(string? url = null)
    {
        var finalUrl = url ?? testSettings.ApplicationUrl;
        Log($"Navigating to: {finalUrl}");
        await (await _page).GotoAsync(finalUrl);
    }

    public async Task TakeScreenshotAsync(string testName, string fileName)
    {
        if (!Directory.Exists(_screenshotFolder))
        {
            Directory.CreateDirectory(_screenshotFolder);
            Log($"Created screenshot folder: {_screenshotFolder}");
        }

        var timestamp = DateTime.UtcNow.ToString("yyyyMMdd_HHmmss");

        var fullFileName = $"{testName}_{fileName}_{timestamp}.png";

        var fullPath = Path.Combine(_screenshotFolder, fullFileName);

        Log($"Taking screenshot for test '{testName}': {fullPath}");

        await (await _page).ScreenshotAsync(new() { Path = fullPath, FullPage = true });
    }

    public async Task WaitForPageToLoadAsync()
    {
        Log("Waiting for page to fully load...");
        await (await _page).WaitForLoadStateAsync(LoadState.NetworkIdle);
        Log("Page load complete.");
    }

    public void Log(string message)
    {
        _outputHelper?.WriteLine($"[{DateTime.UtcNow:HH:mm:ss}] {message}");
    }

    public void SetOutputHelper(ITestOutputHelper outputHelper)
    {
        _outputHelper = outputHelper;
    }
}