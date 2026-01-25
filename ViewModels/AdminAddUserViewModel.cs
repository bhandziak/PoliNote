using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PoliNote.DTOs.Users;
using PoliNote.Services;
using System.Collections.ObjectModel;

namespace PoliNote.ViewModels;

public class AdminAddUserViewModel : BaseViewModel
{
    private readonly AdminService _service = new();

    public string Username { get; set; } = "";
    public string Email { get; set; } = "";
    public string FirstName { get; set; } = "";
    public string LastName { get; set; } = "";
    public string Password { get; set; } = "";

    public ObservableCollection<UserRole> Roles { get; } =
        new() { UserRole.Student, UserRole.Informant, UserRole.Admin };

    public UserRole SelectedRole { get; set; } = UserRole.Student;

    public Command SaveCommand { get; }
    public Command CancelCommand { get; }

    public AdminAddUserViewModel()
    {
        SaveCommand = new Command(async () => await SaveAsync());
        CancelCommand = new Command(async () => await Shell.Current.GoToAsync(".."));

    }

    private async Task SaveAsync()
    {
        var dto = new CreateUserDto
        {
            Username = Username,
            Email = Email,
            FirstName = FirstName,
            LastName = LastName,
            Role = SelectedRole
        };

        await _service.CreateUserAsync(dto);
        await Shell.Current.GoToAsync("..");
    }
}
