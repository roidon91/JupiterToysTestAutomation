using JupiterToysTestFramework.Driver;
using Microsoft.Playwright;

namespace JupiterToysAppTest.Pages;

public interface IShopPage
{
    Task ClickCartAsync();
    Task BuyProductAsync(string productName, int quantity);
}

public class ShopPage : IShopPage
{
    private readonly IPage _page;

    private readonly string cartButton = "#nav-cart";
    private readonly string buyButton = "text=Buy";
    private readonly string productList = "div.products";

    public ShopPage(IPlaywrightDriver playwrightDriver) => _page = playwrightDriver.Page.Result;

    public async Task ClickCartAsync()
    {
        await _page.ClickAsync(cartButton);
    }

    public async Task BuyProductAsync(string productName, int quantity)
    {
        for (var i = 0; i < quantity; i++)
        {
            await _page.ClickAsync($"text={productName} >> .. >> {buyButton}");
        }
    }

    public async Task<bool> IsProductListVisibleAsync()
    {
        return await _page.IsVisibleAsync(productList);
    }
}