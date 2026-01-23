using PoliNote.ViewModels;
using PoliNote.Services;

namespace PoliNote.Views;

public partial class LoginPage : ContentPage
{
    public LoginPage()
    {
        InitializeComponent();
        BindingContext = new LoginViewModel();
    }
}