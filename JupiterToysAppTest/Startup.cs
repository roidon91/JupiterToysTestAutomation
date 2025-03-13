using JupiterToysAppTest.Fixture;
using JupiterToysAppTest.Pages;
using JupiterToysTestFramework.Config;
using JupiterToysTestFramework.Driver;
using Microsoft.Extensions.DependencyInjection;

namespace JupiterToysAppTest;

public abstract class Startup
{
    /// <summary>
    /// Configures the dependency injection (DI) container for the test automation framework.
    /// This method registers all required services and dependencies used across the tests.
    /// 
    /// - **Singleton Services**: 
    ///   - `ConfigReader.ReadConfig()`: Loads the test configuration settings.
    /// 
    /// - **Scoped Services** (New instance for each test case execution):  
    ///   - `IPlaywrightDriver`: Manages Playwright browser sessions.
    ///   - `IPlaywrightDriverInitializer`: Handles the initialization of the Playwright driver.
    ///   - `IHomePage`, `IContactPage`, `ICartPage`, `IShopPage`: Page object models for different sections of the website.
    ///   - `ITestFixtureBase`: Provides common test utilities such as navigation, logging, and screenshots.
    ///
    /// This ensures **proper dependency management** across test executions, allowing for efficient test setup and teardown.
    /// </summary>
    public static void ConfigureServices(IServiceCollection services)
    {
        services
            .AddSingleton(ConfigReader.ReadConfig())
            .AddScoped<IPlaywrightDriver, PlaywrightDriver>()
            .AddScoped<IPlaywrightDriverInitializer, PlaywrightDriverInitializer>()
            .AddScoped<IHomePage, HomePage>()
            .AddScoped<IContactPage, ContactPage>()
            .AddScoped<ICartPage, CartPage>()
            .AddScoped<IShopPage, ShopPage>()
            .AddScoped<ITestFixtureBase, TestFixtureBase>();
    }
}