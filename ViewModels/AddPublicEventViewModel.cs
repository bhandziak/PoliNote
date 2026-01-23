using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PoliNote.Models;
using PoliNote.Services;

namespace PoliNote.ViewModels;

public class AddPublicEventViewModel : BaseViewModel
{
    private readonly PublicCalendarService _service = new();

    public string Title { get; set; }
    public string Description { get; set; }
    public string Location { get; set; }

    public DateTime Date { get; set; } = DateTime.Today;
    public TimeSpan StartTime { get; set; } = new(14, 0, 0);
    public TimeSpan EndTime { get; set; } = new(16, 0, 0);

    public Command SaveCommand { get; }

    public AddPublicEventViewModel()
    {
        SaveCommand = new Command(async () => await SaveAsync());
    }

    private async Task SaveAsync()
    {
        if (string.IsNullOrWhiteSpace(Title))
        {
            await Shell.Current.DisplayAlert(
                "Błąd",
                "Tytuł jest wymagany",
                "OK");
            return;
        }

        var request = new CreatePublicEventRequest
        {
            Title = Title,
            Description = Description,
            Location = Location,
            StartDate = Date.Date + StartTime,
            EndDate = Date.Date + EndTime
        };

        try
        {
            await _service.CreatePublicEventAsync(request);

            await Shell.Current.DisplayAlert(
                "OK",
                "Wydarzenie dodane",
                "OK");

            await Shell.Current.GoToAsync(".."); // powrót
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert(
                "Błąd",
                ex.Message,
                "OK");
        }
    }
}
