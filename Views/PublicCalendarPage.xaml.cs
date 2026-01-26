using PoliNote.ViewModels;
using PoliNote.DTOs.PublicCalendar;

namespace PoliNote.Views;

public partial class PublicCalendarPage : ContentPage
{
	public PublicCalendarPage()
	{
		InitializeComponent();
	}

    private async void OnAddEventClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("add-public-event");
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        if (BindingContext is PublicCalendarViewModel vm)
        {
            // Wymusza odœwie¿enie wydarzeñ
            vm.GetType()
              .GetMethod("LoadEvents", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
              ?.Invoke(vm, null);
        }
    }

    private async void OnEventSelected(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is PublicEventDto ev)
        {
            await Shell.Current.GoToAsync(
                $"public-event-details?id={ev.Id.ToString()}"
            );

            // reset zaznaczenia (wa¿ne!)
            ((CollectionView)sender).SelectedItem = null;
        }
    }
}