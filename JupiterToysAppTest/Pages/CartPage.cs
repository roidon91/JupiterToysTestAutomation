using JupiterToysTestFramework.Driver;
using Microsoft.Playwright;

namespace JupiterToysAppTest.Pages;

public interface ICartPage
{
    Task<int> GetCartItemCountAsync();
    Task<decimal> GetSubtotalAsync(int rowIndex);
    Task<decimal> GetTotalAsync();
}

public class CartPage : ICartPage
{
    private readonly IPage _page;

    private readonly string cartItemCount = "span.cart-count";
    private readonly string checkoutButton = "#checkout";
    private readonly string emptyCartMessage = "#empty-cart-msg";
    private readonly string totalLocator = "tfoot .total";

    public CartPage(IPlaywrightDriver playwrightDriver) => _page = playwrightDriver.Page.Result;

    public async Task<int> GetCartItemCountAsync()
    {
        await _page.WaitForSelectorAsync(cartItemCount); // Ensure element is present
        string countText = await _page.InnerTextAsync(cartItemCount);
        return int.Parse(countText.Trim());
    }

    public async Task ClickCheckoutAsync()
    {
        await _page.ClickAsync(checkoutButton);
    }

    public async Task<bool> IsEmptyCartMessageVisibleAsync()
    {
        return await _page.IsVisibleAsync(emptyCartMessage);
    }

    public async Task<decimal> GetSubtotalAsync(int rowIndex)
    {
        var cartRows = await _page.QuerySelectorAllAsync("tbody .cart-item");

        if (rowIndex - 1 < 0 || rowIndex - 1 >= cartRows.Count)
            throw new IndexOutOfRangeException("Invalid row index for subtotal.");

        var subtotalCell = await cartRows[rowIndex - 1].QuerySelectorAsync("td:nth-child(4)");
        if (subtotalCell == null)
            throw new Exception("Subtotal cell not found.");

        await subtotalCell.WaitForElementStateAsync(ElementState.Visible);

        var subtotalText = await subtotalCell.InnerTextAsync();

        if (!decimal.TryParse(subtotalText.Trim('$'), out var subtotal))
            throw new Exception($"Failed to parse subtotal: '{subtotalText}'");

        return subtotal;
    }

    public async Task<decimal> GetTotalAsync()
    {
        string totalText = await _page.InnerTextAsync(totalLocator);
        return decimal.Parse(totalText.Replace("Total:", "").Trim());
    }
}