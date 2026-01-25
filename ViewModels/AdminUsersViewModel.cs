using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PoliNote.DTOs.Users;
using PoliNote.Services;
using System.Collections.ObjectModel;

namespace PoliNote.ViewModels;

public class AdminUsersViewModel : BaseViewModel
{
    private readonly AdminUserService _service = new();

    public ObservableCollection<UserDto> Users { get; } = new();

    public string SearchText
    {
        get => _searchText;
        set { _searchText = value; OnPropertyChanged(); Filter(); }
    }
    private string _searchText = "";

    private UserRole? _roleFilter = null;

    public Command LoadCommand { get; }
    public Command SetFilterCommand { get; }
    public Command OpenChangeRoleCommand { get; }
    public Command DeleteUserCommand { get; }
    public Command OpenAddUserCommand { get; }

    private List<UserDto> _allUsers = new();

    public AdminUsersViewModel()
    {
        LoadCommand = new Command(async () => await LoadAsync());

        SetFilterCommand = new Command<string>(role =>
        {
            _roleFilter = role == "All" ? null : Enum.Parse<UserRole>(role);
            Filter();
        });

        OpenChangeRoleCommand = new Command<UserDto>(async user =>
            await Shell.Current.GoToAsync($"admin-change-role?userId={user.Id}"));

        DeleteUserCommand = new Command<UserDto>(async user =>
        {
            if (!await Shell.Current.DisplayAlert("Usuń", "Na pewno?", "Tak", "Nie"))
                return;

            await _service.DeleteAsync(user.Id);
            await LoadAsync();
        });

        OpenAddUserCommand = new Command(async () => await Shell.Current.GoToAsync("admin-add-user"));
    }

    private async Task LoadAsync()
    {
        IsBusy = true;

        _allUsers = await _service.GetAllAsync();
        Filter();

        IsBusy = false;
    }

    private void Filter()
    {
        Users.Clear();

        var query = _allUsers.AsEnumerable();

        if (_roleFilter != null)
            query = query.Where(u => u.Role == _roleFilter);

        if (!string.IsNullOrWhiteSpace(SearchText))
            query = query.Where(u =>
                u.Username.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ||
                u.Email.Contains(SearchText, StringComparison.OrdinalIgnoreCase));

        foreach (var u in query)
            Users.Add(u);
    }
}
