using PoliNote.ViewModels;

namespace PoliNote.Views;

public partial class PrivateCalendarPage : ContentPage
{
    public PrivateCalendarPage()
    {
        InitializeComponent();
        BindingContext = new PrivateCalendarViewModel();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        if (BindingContext is PrivateCalendarViewModel vm)
        {
            vm.LoadCommand.Execute(null);
        }
    }
}