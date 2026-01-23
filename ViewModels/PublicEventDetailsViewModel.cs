using PoliNote.Models;
using PoliNote.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PoliNote.ViewModels;

public class PublicEventDetailsViewModel : BaseViewModel
{
    private readonly PublicCalendarService _calendarService;

    public PublicEvent Event { get; private set; }

    public PublicEventDetailsViewModel(PublicCalendarService calendarService)
    {
        _calendarService = calendarService;
    }

    public async Task LoadEventAsync(int eventId)
    {
        Event = await _calendarService.GetPublicEventByIdAsync(eventId);
        OnPropertyChanged(nameof(Event));
    }
}