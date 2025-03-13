using JupiterToysTestFramework.Driver;
using Microsoft.Playwright;

namespace JupiterToysAppTest.Pages;

public interface IHomePage
{
    Task ClickShopAsync();
    Task ClickContactAsync();
}

public class HomePage(IPlaywrightDriver playwrightDriver) : IHomePage
{
    private readonly IPage _page = playwrightDriver.Page.Result;

    private const string shopButton = "text=Shop";
    private const string contactButton = "text=Contact";
    private const string loginButton = "text=Login";

    public async Task ClickShopAsync()
    {
        await _page.ClickAsync(shopButton);
    }

    public async Task ClickContactAsync()
    {
        await _page.ClickAsync(contactButton);
    }

    public async Task ClickLoginAsync()
    {
        await _page.ClickAsync(loginButton);
    }
}