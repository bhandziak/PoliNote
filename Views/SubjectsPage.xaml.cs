

using PoliNote.ViewModels;

namespace PoliNote.Views;

public partial class SubjectsPage : ContentPage
{
    private readonly SubjectsViewModel _vm;

    public SubjectsPage()
    {
        InitializeComponent();
        BindingContext = _vm = new SubjectsViewModel();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _vm.LoadCommand.Execute(null);
    }
}