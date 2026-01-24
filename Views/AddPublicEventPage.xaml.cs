using PoliNote.ViewModels;

namespace PoliNote.Views;

public partial class AddPublicEventPage : ContentPage
{
    public AddPublicEventPage()
    {
        InitializeComponent();
        BindingContext = new AddPublicEventViewModel();
    }

}