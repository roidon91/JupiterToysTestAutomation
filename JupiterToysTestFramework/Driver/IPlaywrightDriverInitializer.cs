using JupiterToysTestFramework.Config;
using Microsoft.Playwright;

namespace JupiterToysTestFramework.Driver;

public interface IPlaywrightDriverInitializer
{
    Task<IBrowser> GetChromeDriverAsync(TestSettings testSettings);
    Task<IBrowser> GetChromiumDriverAsync(TestSettings testSettings);
    Task<IBrowser> GetFirefoxDriverAsync(TestSettings testSettings);
    Task<IBrowser> GetWebKitDriverAsync(TestSettings testSettings);
}