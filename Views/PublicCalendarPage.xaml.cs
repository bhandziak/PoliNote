using PoliNote.ViewModels;

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
}