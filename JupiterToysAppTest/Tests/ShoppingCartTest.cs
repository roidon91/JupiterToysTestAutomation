using FluentAssertions;
using JupiterToysAppTest.Fixture;
using JupiterToysAppTest.Pages;
using Xunit.Abstractions;

namespace JupiterToysAppTest.Tests;

public class ShoppingCartTest
{
    private readonly ITestFixtureBase _testFixture;

    private readonly IHomePage _homePage;
    private readonly ICartPage _cartPage;
    private readonly IShopPage _shopPage;

    public ShoppingCartTest(
        ITestFixtureBase testFixture,
        ITestOutputHelper output,
        IHomePage homePage,
        ICartPage cartPage,
        IShopPage shopPage)
    {
        _testFixture = testFixture;
        _homePage = homePage;
        _cartPage = cartPage;
        _shopPage = shopPage;
        _testFixture.SetOutputHelper(output);
    }

    /// <summary>
    /// Validates that the shopping cart correctly calculates item subtotals and total price.
    /// **Steps:**
    /// 1. Navigate to the shop and add items.
    /// 2. Go to the cart and verify item count, subtotals, and total price.
    /// 3. Capture screenshots for debugging.
    /// **Outcome:** 
    /// Ensures the cart's price calculation is accurate.
    /// </summary>
    [Fact]
    public async Task TestCartCalculatesSubtotalAndTotalCorrectly()
    {
        const string testName = nameof(TestCartCalculatesSubtotalAndTotalCorrectly);
        _testFixture.Log("Navigating to application URL...");
        await _testFixture.NavigateToUrl();

        await _testFixture.TakeScreenshotAsync(testName, "Before_Adding_Items");

        _testFixture.Log("Clicking on Shop page...");
        await _homePage.ClickShopAsync();

        _testFixture.Log("Adding items to cart...");
        await _shopPage.BuyProductAsync("Stuffed Frog", 2);
        await _shopPage.BuyProductAsync("Fluffy Bunny", 5);
        await _shopPage.BuyProductAsync("Valentine Bear", 3);

        await _testFixture.TakeScreenshotAsync(testName, "After_Adding_Items");

        _testFixture.Log("Navigating to Cart...");
        await _shopPage.ClickCartAsync();

        await _testFixture.WaitForPageToLoadAsync();
        _testFixture.Log("Cart page loaded successfully.");

        var itemCount = await _cartPage.GetCartItemCountAsync();
        _testFixture.Log($"Cart contains {itemCount} items.");
        itemCount.Should().Be(10, because: "the user added a total of 10 items to the cart");

        _testFixture.Log("Fetching subtotals...");
        var subtotal1 = await _cartPage.GetSubtotalAsync(1);
        var subtotal2 = await _cartPage.GetSubtotalAsync(2);
        var subtotal3 = await _cartPage.GetSubtotalAsync(3);

        _testFixture.Log($"Subtotal 1: {subtotal1}, Subtotal 2: {subtotal2}, Subtotal 3: {subtotal3}");
        subtotal1.Should().BeGreaterThan(0, because: "each product should have a valid subtotal");
        subtotal2.Should().BeGreaterThan(0, because: "each product should have a valid subtotal");
        subtotal3.Should().BeGreaterThan(0, because: "each product should have a valid subtotal");

        var expectedTotal = subtotal1 + subtotal2 + subtotal3;

        _testFixture.Log("Waiting for total to load...");
        await _testFixture.WaitForPageToLoadAsync();

        var actualTotal = await _cartPage.GetTotalAsync();
        _testFixture.Log($"Expected Total: {expectedTotal}, Actual Total: {actualTotal}");
        actualTotal.Should().Be(expectedTotal, because: "the total price should match the sum of all subtotals");

        await _testFixture.TakeScreenshotAsync(testName, "Final_Cart_Page");

        _testFixture.Log("Test completed successfully.");
    }
}