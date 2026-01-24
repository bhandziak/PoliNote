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
    [QueryProperty(nameof(EventId), "id")]
    public class PrivateEventDetailsViewModel : BaseViewModel
    {
        private readonly PrivateCalendarService _service = new();

        public Guid EventId { get; set; }

        public PrivateEventDetailsDto Event { get; private set; }
        public string NoteContent { get; set; } = "";

        public Command SaveNoteCommand { get; }
        public Command DeleteCommand { get; }

        public PrivateEventDetailsViewModel()
        {
            SaveNoteCommand = new Command(async () => await SaveNoteAsync());
            DeleteCommand = new Command(async () => await DeleteAsync());
        }

        public async Task LoadAsync()
        {
            IsBusy = true;

            Event = await _service.GetByIdAsync(EventId);

            var note = await _service.GetNoteAsync(EventId);
            NoteContent = note?.Content ?? "";

            OnPropertyChanged(nameof(Event));
            OnPropertyChanged(nameof(NoteContent));

            IsBusy = false;
        }

        private async Task SaveNoteAsync()
        {
            await _service.UpdateNoteAsync(
                EventId,
                new NoteRequestDto { Content = NoteContent }
            );

            await Shell.Current.DisplayAlert("OK", "Notatka zapisana", "OK");
        }

        private async Task DeleteAsync()
        {
            await _service.DeleteAsync(EventId);
            await Shell.Current.GoToAsync("..");
        }
    }
}
