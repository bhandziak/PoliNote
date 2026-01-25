using PoliNote.ViewModels;

namespace PoliNote.Views;

public partial class AdminAddClassPage : ContentPage
{
    public AdminAddClassPage()
    {
        InitializeComponent();
        BindingContext = new AdminAddClassViewModel();
    }
}