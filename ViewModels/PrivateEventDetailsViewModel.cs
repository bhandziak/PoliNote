using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using PoliNote.DTOs.PrivateCalendar;
using PoliNote.Services;
using PoliNote.DTOs.Notes;



namespace PoliNote.ViewModels
{
    //[QueryProperty(nameof(EventId), "id")]
    public class PrivateEventDetailsViewModel : BaseViewModel
    {
        private readonly PrivateCalendarService _service = new();

        //public Guid EventId { get; set; }
        private Guid _eventId;

        public PrivateEventDetailsDto Event { get; private set; }
        public string NoteContent { get; set; } = "";

        public Command SaveNoteCommand { get; }
        public Command DeleteCommand { get; }
        public Command CloseCommand { get; }

        public PrivateEventDetailsViewModel()
        {
            SaveNoteCommand = new Command(async () => await SaveNoteAsync());
            DeleteCommand = new Command(async () => await DeleteAsync());
            CloseCommand = new Command(async () => await Shell.Current.GoToAsync(".."));
        }

        public async Task LoadAsync(Guid eventId)
        {
            IsBusy = true;

            _eventId = eventId;

            Event = await _service.GetByIdAsync(eventId);
            var note = await _service.GetNoteAsync(eventId);

            NoteContent = note?.Content ?? "";

            OnPropertyChanged(nameof(Event));
            OnPropertyChanged(nameof(NoteContent));

            IsBusy = false;
        }

        private async Task SaveNoteAsync()
        {
            await _service.UpdateNoteAsync(_eventId, new NoteRequestDto { Content = NoteContent } );

            await Shell.Current.DisplayAlert("OK", "Notatka zapisana", "OK");
        }

        private async Task DeleteAsync()
        {
            if (!await Shell.Current.DisplayAlert(
                    "Usuwanie",
                    "Czy na pewno chcesz usunąć wydarzenie?",
                    "Tak",
                    "Nie"))
                return;

            await _service.DeleteAsync(_eventId);
            await Shell.Current.GoToAsync("..");
        }
    }
}
