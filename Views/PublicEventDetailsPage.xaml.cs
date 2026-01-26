using Microsoft.Extensions.Logging;
using PoliNote.ViewModels;

namespace PoliNote.Views;

[QueryProperty(nameof(EventId), "id")]
public partial class PublicEventDetailsPage : ContentPage
{
    private readonly PublicEventDetailsViewModel _vm;

    public string EventId
    {
        set
        {
            if (Guid.TryParse(value, out var guid))
            {
                _ = _vm.LoadEventAsync(guid);
            }
        }
    }

    public PublicEventDetailsPage()
    {
        InitializeComponent();
        _vm = new PublicEventDetailsViewModel(new Services.PublicCalendarService());
        BindingContext = _vm;
    }

}