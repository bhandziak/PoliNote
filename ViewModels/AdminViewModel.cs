using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoliNote.ViewModels;

public class AdminViewModel : BaseViewModel
{
    public Command OpenUsersCommand { get; }
    public Command OpenAddClassCommand { get; }

    public AdminViewModel()
    {
        OpenUsersCommand = new Command(async () =>
            await Shell.Current.GoToAsync("admin-users"));

        OpenAddClassCommand = new Command(async () =>
            await Shell.Current.GoToAsync("admin-add-class"));
    }
}
