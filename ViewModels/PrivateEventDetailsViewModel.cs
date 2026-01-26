using Microsoft.Extensions.Logging;
using PoliNote.DTOs.Notes;
using PoliNote.DTOs.PrivateCalendar;
using PoliNote.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace PoliNote.ViewModels
{
    [QueryProperty(nameof(EventId), "id")]
    public class PrivateEventDetailsViewModel : BaseViewModel
    {
        private readonly PrivateCalendarService _service = new();
        private readonly NoteService _noteService = new();

        //public Guid EventId { get; set; }
        private Guid _eventId;

        private PrivateEventDetailsDto? _event;
        public PrivateEventDetailsDto? Event
        {
            get => _event;
            set => SetProperty(ref _event, value);
        }
        //public string NoteContent { get; set; } = "";

        private Guid _noteId;

        public Command AddNoteCommand { get; }
        public Command EditCommand { get; }
        public Command DeleteCommand { get; }
        public Command CloseCommand { get; }

        public PrivateEventDetailsViewModel()
        {
            //SaveNoteCommand = new Command(async () => await SaveNoteAsync());
            AddNoteCommand = new Command(async () => await OpenNoteAsync());
            EditCommand = new Command(async () => await EditEventAsync());
            DeleteCommand = new Command(async () => await DeleteAsync());
            CloseCommand = new Command(async () => await Shell.Current.GoToAsync(".."));
        }

        public async Task LoadAsync(Guid eventId)
        {
            IsBusy = true;

            _eventId = eventId;

            Event = await _service.GetByIdAsync(eventId);

            OnPropertyChanged(nameof(Event));

            IsBusy = false;
        }

        /*
        private async Task SaveNoteAsync()
        {
            await _service.UpdateNoteAsync(_eventId, new NoteRequestDto { Content = NoteContent } );

            await Shell.Current.DisplayAlert("OK", "Notatka zapisana", "OK");
        }
        */

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

        private async Task OpenNoteAsync()
        {
            await Shell.Current.GoToAsync(
                $"add-note?eventId={_eventId}"
            );
        }
        
        private async Task EditEventAsync()
        {
            await Shell.Current.GoToAsync(
                $"edit-private-event?id={_eventId}"
            );
        }
    }
}
