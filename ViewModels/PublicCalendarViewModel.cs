using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Collections.ObjectModel;
using PoliNote.Models;
using PoliNote.Services;

namespace PoliNote.ViewModels;

public class PublicCalendarViewModel : BaseViewModel
{
    private readonly PublicCalendarService _service = new();

    public ObservableCollection<PublicEvent> Events { get; } = new();

    private DateTime _selectedDate = DateTime.Today;
    public DateTime SelectedDate
    {
        get => _selectedDate;
        set
        {
            if (SetProperty(ref _selectedDate, value))
            {
                LoadEvents();
            }
        }
    }

    private bool _isBusy;
    public bool IsBusy
    {
        get => _isBusy;
        set => SetProperty(ref _isBusy, value);
    }

    public PublicCalendarViewModel()
    {
        LoadEvents();
    }

    private async void LoadEvents()
    {
        if (IsBusy)
            return;

        IsBusy = true;

        try
        {
            var events = await _service.GetPublicEventsByDateAsync(SelectedDate);

            Events.Clear();
            foreach (var ev in events)
                Events.Add(ev);
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert(
                "Błąd",
                $"Nie udało się pobrać wydarzeń:\n{ex.Message}",
                "OK");
        }
        finally
        {
            IsBusy = false;
        }
    }
}
