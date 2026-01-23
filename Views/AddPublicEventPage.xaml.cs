using PoliNote.ViewModels;

namespace PoliNote.Views;

public partial class AddPublicEventPage : ContentPage
{
    public AddPublicEventPage()
    {
        InitializeComponent();
        BindingContext = new AddPublicEventViewModel();
    }

    private async void OnCancelClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("..");
    }
}