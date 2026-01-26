using PoliNote.ViewModels;

namespace PoliNote.Views;


[QueryProperty(nameof(EventId), "id")]
public partial class PrivateEventDetailsPage : ContentPage
{
    private readonly PrivateEventDetailsViewModel _vm;

    public PrivateEventDetailsPage()
    {
        InitializeComponent();
        BindingContext = _vm = new PrivateEventDetailsViewModel();
    }

    public string EventId
    {
        set
        {
            if (Guid.TryParse(value, out var guid))
            {
                _vm.LoadAsync(guid);
            }
        }
    }

}