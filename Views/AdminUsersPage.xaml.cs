using PoliNote.ViewModels;

namespace PoliNote.Views;

public partial class AdminUsersPage : ContentPage
{
    public AdminUsersPage()
    {
        InitializeComponent();
        BindingContext = new AdminUsersViewModel();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        ((AdminUsersViewModel)BindingContext).LoadCommand.Execute(null);
    }
}