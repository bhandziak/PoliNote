using PoliNote.ViewModels;

namespace PoliNote.Views;

[QueryProperty(nameof(UserId), "userId")]
public partial class AdminChangeRolePage : ContentPage
{
    private readonly AdminChangeRoleViewModel _vm;

    public Guid UserId
    {
        set => _vm.LoadAsync(value);
    }

    public AdminChangeRolePage()
    {
        InitializeComponent();
        BindingContext = _vm = new AdminChangeRoleViewModel();
    }
}