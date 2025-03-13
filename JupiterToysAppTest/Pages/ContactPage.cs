using JupiterToysTestFramework.Driver;
using Microsoft.Playwright;

namespace JupiterToysAppTest.Pages;

public interface IContactPage
{
    Task FillForenameAsync(string forename);
    Task FillSurnameAsync(string surname);
    Task FillEmailAsync(string email);
    Task FillMessageAsync(string message);

    Task ClickSubmitAsync();

    Task<string> GetForenameErrorAsync();

    Task<string> GetEmailErrorAsync();

    Task<string> GetMessageErrorAsync();
    Task<bool> IsForenameErrorVisibleAsync();

    Task<bool> IsEmailErrorVisibleAsync();

    Task<bool> IsMessageErrorVisibleAsync();

    Task<string> GetSuccessMessageAsync();

    Task<string> GetSuccessNameAsync();
}

public class ContactPage(IPlaywrightDriver playwrightDriver) : IContactPage
{
    private readonly IPage _page = playwrightDriver.Page.Result;

    private const string forenameInput = "input[name='forename']";
    private const string surnameInput = "input[name='surname']";
    private const string emailInput = "input[name='email']";
    private const string messageInput = "textarea[name='message']";
    private const string submitButton = "text=Submit";

    private const string forenameError = "#forename-err";
    private const string emailError = "#email-err";
    private const string messageError = "#message-err";

    private const string successMessageContainer = ".alert.alert-success";
    private const string successNameContainer = ".alert.alert-success strong";


    public async Task FillForenameAsync(string forename)
    {
        await _page.FillAsync(forenameInput, forename);
    }

    public async Task FillSurnameAsync(string surname)
    {
        await _page.FillAsync(surnameInput, surname);
    }

    public async Task FillEmailAsync(string email)
    {
        await _page.FillAsync(emailInput, email);
    }

    public async Task FillMessageAsync(string message)
    {
        await _page.FillAsync(messageInput, message);
    }

    public async Task ClickSubmitAsync()
    {
        await _page.ClickAsync(submitButton);
    }

    public async Task<string> GetForenameErrorAsync()
    {
        return await _page.InnerTextAsync(forenameError);
    }

    public async Task<string> GetEmailErrorAsync()
    {
        return await _page.InnerTextAsync(emailError);
    }

    public async Task<string> GetMessageErrorAsync()
    {
        return await _page.InnerTextAsync(messageError);
    }

    public async Task<bool> IsForenameErrorVisibleAsync()
    {
        return await _page.IsVisibleAsync(forenameError);
    }

    public async Task<bool> IsEmailErrorVisibleAsync()
    {
        return await _page.IsVisibleAsync(emailError);
    }

    public async Task<bool> IsMessageErrorVisibleAsync()
    {
        return await _page.IsVisibleAsync(messageError);
    }

    public async Task<string> GetSuccessMessageAsync()
    {
        return await _page.InnerTextAsync(successMessageContainer);
    }

    public async Task<string> GetSuccessNameAsync()
    {
        return await _page.InnerTextAsync(successNameContainer);
    }
}