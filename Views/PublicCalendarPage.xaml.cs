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
}