using PoliNote.ViewModels;

namespace PoliNote.Views;

[QueryProperty(nameof(EventIdRaw), "eventId")]
[QueryProperty(nameof(DateRaw), "date")]
public partial class NotePage : ContentPage
{
    private readonly NoteViewModel _vm;

    private Guid? _eventId;
    private DateTime? _date;

    public string EventIdRaw
    {
        set
        {
            if (Guid.TryParse(value, out var guid))
            {
                _eventId = guid;
                TryLoad();
            }
        }
    }

    public string DateRaw
    {
        set
        {
            if (DateTime.TryParse(value, out var date))
            {
                _date = date;
                TryLoad();
            }
        }
    }

    public NotePage()
    {
        InitializeComponent();
        _vm = new NoteViewModel();
        BindingContext = _vm;
    }

    private void TryLoad()
    {
        if (_eventId.HasValue && _date.HasValue)
        {
            _vm.Load(_eventId.Value, _date.Value);
        }
    }
}