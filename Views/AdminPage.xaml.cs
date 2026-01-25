using PoliNote.ViewModels;

namespace PoliNote.Views;

public partial class AdminPage : ContentPage
{
    public AdminPage()
    {
        InitializeComponent();
        BindingContext = new AdminViewModel();
    }
}