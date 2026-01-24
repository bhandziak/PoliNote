namespace PoliNote.Views;

public partial class AddPrivateEventPage : ContentPage
{
	public AddPrivateEventPage()
	{
		InitializeComponent();
        BindingContext = new AddPrivateEventViewModel();
    }

}