using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using PoliNote.DTOs.PrivateCalendar;
using PoliNote.Services;
using PoliNote.ViewModels;

public class AddPrivateEventViewModel : BaseViewModel
{
    private readonly PrivateCalendarService _service = new();

    public string Title { get; set; }
    public string Description { get; set; }
    public string Location { get; set; }

    public DateTime Date { get; set; } = DateTime.Today;
    public TimeSpan StartTime { get; set; } = new(8, 0, 0);
    public TimeSpan EndTime { get; set; } = new(10, 0, 0);

    public Command SaveCommand { get; }
    public Command CancelCommand { get; }

    public AddPrivateEventViewModel()
    {
        IsBusy = false;

        SaveCommand = new Command(async () => await SaveAsync());
        CancelCommand = new Command(async () => await OnCancel());
    }

    private async Task SaveAsync()
    {
        if (string.IsNullOrWhiteSpace(Title))
        {
            await Shell.Current.DisplayAlert("Błąd", "Tytuł jest wymagany", "OK");
            return;
        }

        var fullDateTime = Date.Date.Add(StartTime);

        var timeString = $"{StartTime.Hours:D2}:{StartTime.Minutes:D2}";

        var request = new PrivateEventRequestDto
        {
            Title = Title.Trim(),
            Description = Description?.Trim(),
            Date = Date.Date,
            Time = StartTime,          
            Location = Location?.Trim(),
            EventType = "Exam"
        };

        try
        {
            await _service.CreateAsync(request);
            await Shell.Current.DisplayAlert("OK", "Wydarzenie dodane", "OK");
            await Shell.Current.GoToAsync("..");
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Błąd", ex.Message, "OK");
        }
    }

    private async Task OnCancel()
    {
        await Shell.Current.GoToAsync("..");
    }
}
