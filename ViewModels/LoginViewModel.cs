using System.Windows.Input;

namespace PoliNote.ViewModels;

public class LoginViewModel
{
    public string Username { get; set; }
    public string Password { get; set; }

    public ICommand LoginCommand { get; }

    public LoginViewModel()
    {
        LoginCommand = new Command(OnLogin);
    }

    private async void OnLogin()
    {
        await Application.Current.MainPage.DisplayAlert(
            "Login",
            $"Username: {Username}",
            "OK");
    }
}