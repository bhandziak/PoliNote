using PoliNote.ViewModels;

namespace PoliNote.Views;

[QueryProperty(nameof(UserId), "userId")]
public partial class AdminChangeRolePage : ContentPage
{
    private readonly AdminChangeRoleViewModel _vm;

    private string _userId;
    public string UserId
    {
        set
        {
            _userId = value;

            if (Guid.TryParse(value, out var guid))
            {
                _vm.LoadAsync(guid);
            }
        }
    }

    public AdminChangeRolePage()
    {
        InitializeComponent();
        BindingContext = _vm = new AdminChangeRoleViewModel();
    }
}