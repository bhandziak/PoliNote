using PoliNote.DTOs.PrivateCalendar;
using PoliNote.ViewModels;

namespace PoliNote.Views;

public partial class PrivateCalendarPage : ContentPage
{
    public PrivateCalendarPage()
    {
        InitializeComponent();
        BindingContext = new PrivateCalendarViewModel();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        (BindingContext as PrivateCalendarViewModel)?.LoadCommand.Execute(null);

    }

    private async void OnEventSelected(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is PrivateEventDto ev)
        {
            await Shell.Current.GoToAsync(
                $"private-event-details?id={ev.Id}");

            ((CollectionView)sender).SelectedItem = null;
        }
    }
}