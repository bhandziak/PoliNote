using Microsoft.Extensions.Logging;
using PoliNote.DTOs.Notes;
using PoliNote.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoliNote.ViewModels
{
    [QueryProperty(nameof(EventId), "eventId")]
    public class NoteViewModel : BaseViewModel
    {
        private readonly NoteService _service = new();

        //public Guid EventId { get; set; }
        public Guid? NoteId { get; private set; }

        private Guid _eventId;

        public string Content { get; set; } = "";

        public Command SaveCommand { get; }
        public Command CancelCommand { get; }

        public void Load(Guid eventId)
        {
            _eventId = eventId;
        }

        public NoteViewModel()
        {
            SaveCommand = new Command(async () => await SaveAsync());
            CancelCommand = new Command(async () => await Shell.Current.GoToAsync(".."));
        }

        public async Task LoadAsync()
        {
            var note = await _service.GetByEventIdAsync(_eventId); // jeśli masz
            if (note != null)
            {
                NoteId = note.Id;
                Content = note.Content;
            }
        }

        private async Task SaveAsync()
        {
            if (NoteId.HasValue)
            {
                await _service.UpdateAsync(NoteId.Value, new NoteRequestDto
                {
                    Content = Content
                });
            }
            else
            {
                await _service.CreateForEventAsync(_eventId, new NoteRequestDto
                {
                    Content = Content
                });
            }

            await Shell.Current.GoToAsync("..");
        }
    }
}
