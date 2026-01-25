using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PoliNote.DTOs.Users;
using PoliNote.Services;

namespace PoliNote.ViewModels;

public class AdminChangeRoleViewModel : BaseViewModel
{
    private readonly AdminUserService _service = new();

    private Guid _userId;

    public string Username { get; private set; } = "";
    public string Email { get; private set; } = "";

    public bool IsStudent { get; set; }
    public bool IsInformant { get; set; }
    public bool IsAdmin { get; set; }

    public Command SaveCommand { get; }
    public Command CancelCommand { get; }

    public AdminChangeRoleViewModel()
    {
        SaveCommand = new Command(async () => await SaveAsync());
        CancelCommand = new Command(async () => await Shell.Current.GoToAsync(".."));
    }

    public async Task LoadAsync(Guid userId)
    {
        _userId = userId;

        var user = await _service.GetByIdAsync(userId);

        Username = user.Username;
        Email = user.Email;

        IsStudent = user.Role == UserRole.Student;
        IsInformant = user.Role == UserRole.Informant;
        IsAdmin = user.Role == UserRole.Admin;

        OnPropertyChanged(nameof(Username));
        OnPropertyChanged(nameof(Email));
        OnPropertyChanged(nameof(IsStudent));
        OnPropertyChanged(nameof(IsInformant));
        OnPropertyChanged(nameof(IsAdmin));
    }

    private async Task SaveAsync()
    {
        var role =
            IsAdmin ? UserRole.Admin :
            IsInformant ? UserRole.Informant :
            UserRole.Student;

        await _service.ChangeRoleAsync(_userId, role);
        await Shell.Current.GoToAsync("..");
    }
}
