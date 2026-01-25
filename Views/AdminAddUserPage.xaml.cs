using PoliNote.ViewModels;

namespace PoliNote.Views;

public partial class AdminAddUserPage : ContentPage
{
    public AdminAddUserPage()
    {
        InitializeComponent();
        BindingContext = new AdminAddUserViewModel();
    }
}