using Microsoft.Extensions.Logging;
using PoliNote.ViewModels;

namespace PoliNote.Views;

[QueryProperty(nameof(EventId), "id")]
public partial class PublicEventDetailsPage : ContentPage
{
    private readonly PublicEventDetailsViewModel _vm;

    public Guid EventId
    {
        set => _ = _vm.LoadEventAsync(value);
    }

    public PublicEventDetailsPage(PublicEventDetailsViewModel vm)
    {
        InitializeComponent();
        BindingContext = _vm = vm;
    }

}