using FluentAssertions;
using JupiterToysAppTest.Fixture;
using JupiterToysAppTest.Pages;
using JupiterToysTestFramework.Config;
using JupiterToysTestFramework.Driver;
using Xunit;
using Xunit.Abstractions;

namespace JupiterToysAppTest.Tests;

public class ContactFormValidationTest
{
    private readonly ITestFixtureBase _testFixture;
    private readonly IPlaywrightDriver _playwrightDriver;
    private readonly TestSettings _testSettings;
    private readonly IHomePage _homePage;
    private readonly IContactPage _contactPage;

    public ContactFormValidationTest(
        IPlaywrightDriver playwrightDriver,
        ITestFixtureBase testFixture,
        ITestOutputHelper output,
        TestSettings testSettings,
        IHomePage homePage,
        IContactPage contactPage)
    {
        _playwrightDriver = playwrightDriver;
        _testFixture = testFixture;
        _testSettings = testSettings;
        _homePage = homePage;
        _contactPage = contactPage;

        _testFixture.SetOutputHelper(output);
    }


    /// <summary>
    /// **TestContactFormDisplaysErrorsForMissingFields**
    /// - Navigates to the contact page.
    /// - Submits an empty form and verifies required field errors.
    /// - Fills the form with valid data and ensures errors disappear.
    /// - Captures screenshots before and after form submission.
    /// - Ensures proper validation of the form.
    /// </summary>
    [Fact]
    public async Task TestContactFormDisplaysErrorsForMissingFields()
    {
        const string testName = nameof(TestContactFormSuccessfulSubmission);
        _testFixture.Log("Navigating to application URL...");
        await _testFixture.NavigateToUrl();

        _testFixture.Log("Clicking on Contact page...");
        await _homePage.ClickContactAsync();

        await _testFixture.TakeScreenshotAsync(testName, "Before_Submit_ContactForm");

        _testFixture.Log("Submitting empty contact form...");
        await _contactPage.ClickSubmitAsync();

        _testFixture.Log("Verifying validation messages...");
        (await _contactPage.GetForenameErrorAsync()).Should().Be("Forename is required",
            because: "Forename is a mandatory field");

        (await _contactPage.GetEmailErrorAsync()).Should().Be("Email is required",
            because: "Email is a required field for the contact form");

        (await _contactPage.GetMessageErrorAsync()).Should().Be("Message is required",
            because: "A message is required to submit the contact form");


        _testFixture.Log("Filling contact form...");
        await _contactPage.FillForenameAsync("Roy");
        await _contactPage.FillEmailAsync("Test@gmail.com");
        await _contactPage.FillMessageAsync("This is a test message.");


        await _testFixture.TakeScreenshotAsync(testName, "After_Fill_ContactForm");

        _testFixture.Log("Verifying that error messages are gone...");
        (await _contactPage.IsForenameErrorVisibleAsync()).Should()
            .BeFalse("the forename error should disappear after entering valid data");
        (await _contactPage.IsEmailErrorVisibleAsync()).Should()
            .BeFalse("the email error should disappear after entering valid data");
        (await _contactPage.IsMessageErrorVisibleAsync()).Should()
            .BeFalse("the message error should disappear after entering valid data");

        _testFixture.Log("Test completed successfully.");
    }

    /// <summary>
    /// **TestContactFormSuccessfulSubmission**
    /// - Runs multiple test iterations with different inputs.
    /// - Fills and submits the contact form.
    /// - Verifies the success message contains the correct name.
    /// - Captures screenshots at key steps.
    /// - Ensures form submission works correctly.
    /// </summary>
    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(4)]
    [InlineData(5)]
    public async Task TestContactFormSuccessfulSubmission(int testCountNumber)
    {
        const string testName = nameof(TestContactFormSuccessfulSubmission);
        _testFixture.Log($"Running Contact Form Submission Test #{testCountNumber}");
        await _testFixture.NavigateToUrl();

        _testFixture.Log("Clicking on Contact page...");
        await _homePage.ClickContactAsync();


        await _testFixture.TakeScreenshotAsync(testName, $"Before_Fill_ContactForm_Test{testCountNumber}");

        _testFixture.Log($"Filling contact form with Test #{testCountNumber} data...");
        await _contactPage.FillForenameAsync($"XYZ{testCountNumber}");
        await _contactPage.FillEmailAsync($"XYZ{testCountNumber}@example.com");
        await _contactPage.FillMessageAsync("This is a test message.");

        await _testFixture.TakeScreenshotAsync(testName, $"Before_Submit_ContactForm_Test{testCountNumber}");

        _testFixture.Log("Submitting contact form...");
        await _contactPage.ClickSubmitAsync();


        await _testFixture.WaitForPageToLoadAsync();

        _testFixture.Log("Validating success message...");
        var expectedSuccessMessage = $"Thanks XYZ{testCountNumber}, we appreciate your feedback.";
        (await _contactPage.GetSuccessMessageAsync()).Should().Contain(expectedSuccessMessage,
            because: "the contact form should display a success message after valid submission");

        (await _contactPage.GetSuccessNameAsync()).Should().Be($"Thanks XYZ{testCountNumber}",
            because: "the name in the success message should match the submitted forename");

        await _testFixture.TakeScreenshotAsync(testName, $"After_Success_ContactForm_Test{testCountNumber}");

        _testFixture.Log($"Contact Form Submission Test #{testCountNumber} completed successfully.");
    }
}