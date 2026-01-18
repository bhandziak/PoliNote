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
                await Shell.Current.DisplayAlert("OK", "Zalogowano", "OK");
                // TODO: nawigacja do głównej części aplikacji
            }
            else
            {
                await Shell.Current.DisplayAlert("Błąd", "Niepoprawne dane", "OK");
            }
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Błąd", ex.Message, "OK");
        }
        finally
        {
            IsBusy = false;
        }
    }
}