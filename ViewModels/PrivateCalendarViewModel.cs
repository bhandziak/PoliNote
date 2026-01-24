using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PoliNote.DTOs.PrivateCalendar;
using PoliNote.Services;
using PoliNote.ViewModels;

namespace PoliNote.ViewModels
{
    public class PrivateCalendarViewModel : BaseViewModel
    {
        private readonly PrivateCalendarService _service = new();

        public ObservableCollection<PrivateEventDto> Events { get; } = new();

        public DateTime SelectedDate { get; set; } = DateTime.Today;

        public Command LoadCommand { get; }
        public Command AddCommand { get; }

        public PrivateCalendarViewModel()
        {
            LoadCommand = new Command(async () => await LoadAsync());
            AddCommand = new Command(async () =>
            {
                await Shell.Current.GoToAsync("add-private-event");
            });
        }

        private async Task LoadAsync()
        {
            IsBusy = true;

            Events.Clear();
            var items = await _service.GetByDateAsync(SelectedDate);

            foreach (var e in items)
                Events.Add(e);

            IsBusy = false;
        }
    }
}