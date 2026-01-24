using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using PoliNote.Models;
using PoliNote.Services;

namespace PoliNote.ViewModels
{
    [QueryProperty(nameof(EventId), "id")]
    public class PrivateEventDetailsViewModel : BaseViewModel
    {
        private readonly PrivateCalendarService _service = new();

        public int EventId { get; set; }
        public PrivateEvent Event { get; set; }

        public Command SaveNoteCommand { get; }
        public Command DeleteCommand { get; }

        public PrivateEventDetailsViewModel()
        {
            SaveNoteCommand = new Command(async () =>
                await _service.UpdateNoteAsync(Event.Id, Event.Note));

            DeleteCommand = new Command(async () =>
            {
                await _service.DeleteAsync(Event.Id);
                await Shell.Current.GoToAsync("..");
            });
        }

        public async Task LoadAsync()
        {
            Event = await _service.GetByIdAsync(EventId);
            OnPropertyChanged(nameof(Event));
        }
    }
}
