using System.Windows.Input;
using PoliNote.Services;

namespace PoliNote.ViewModels;

public class LoginViewModel : BaseViewModel
{
    private readonly AuthService _authService = new();

    private string _username;
    private string _password;
    private bool _isBusy;

    public string Username
    {
        get => _username;
        set => SetProperty(ref _username, value);
    }

    public string Password
    {
        get => _password;
        set => SetProperty(ref _password, value);
    }

    public bool IsBusy
    {
        get => _isBusy;
        set => SetProperty(ref _isBusy, value);
    }

    public ICommand LoginCommand { get; }

    public LoginViewModel()
    {
        LoginCommand = new Command(async () => await OnLogin(), () => !IsBusy);
    }

    private async Task OnLogin()
    {
        if (IsBusy)
            return;

        IsBusy = true;

        try
        {
            var success = await _authService.LoginAsync(Username, Password);

            if (success)
            {
                await Shell.Current.DisplayAlert(
                    "OK",
                    "Zalogowano poprawnie (cookie zapisane)",
                    "OK");

                await Shell.Current.GoToAsync("//public-calendar");
            }
            else
            {
                await Shell.Current.DisplayAlert(
                    "Błąd",
                    "Niepoprawne dane logowania",
                    "OK");
            }
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert(
                "Wyjątek",
                ex.Message,
                "OK");
        }
        finally
        {
            IsBusy = false;
        }
    }
}