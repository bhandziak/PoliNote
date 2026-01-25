using PoliNote.ViewModels;

namespace PoliNote.Views;

[QueryProperty(nameof(EventIdRaw), "eventId")]
public partial class NotePage : ContentPage
{
    private readonly NoteViewModel _vm;

    public string EventIdRaw
    {
        set => _vm.Load(Guid.Parse(value));
    }

    public NotePage()
    {
        InitializeComponent();
        BindingContext = _vm = new NoteViewModel();
    }
}