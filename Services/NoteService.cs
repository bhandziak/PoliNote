using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PoliNote.DTOs.Notes;
using System.Net.Http.Json;

namespace PoliNote.Services
{
    public class NoteService
    {
        private readonly HttpClient _client = ApiClient.Client;

        /// <summary>
        /// Pobranie notatki dla wydarzenia prywatnego
        /// </summary>
        public async Task<NoteDto?> GetByEventIdAsync(Guid eventId)
        {
            try
            {
                return await _client.GetFromJsonAsync<NoteDto>(
                    $"/api/calendar/private/{eventId}/note"
                );
            }
            catch (HttpRequestException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null; // brak notatki – normalna sytuacja
            }
        }

        /// <summary>
        /// Utworzenie notatki dla wydarzenia prywatnego
        /// </summary>
        public async Task CreateForEventAsync(Guid eventId, NoteRequestDto request)
        {
            var response = await _client.PostAsJsonAsync(
                $"/api/calendar/private/{eventId}/note",
                request
            );

            response.EnsureSuccessStatusCode();
        }

        /// <summary>
        /// Aktualizacja notatki
        /// </summary>
        public async Task UpdateAsync(Guid noteId, NoteRequestDto request)
        {
            var response = await _client.PatchAsJsonAsync(
                $"/api/note/{noteId}",
                request
            );

            response.EnsureSuccessStatusCode();
        }

        /// <summary>
        /// Usunięcie notatki
        /// </summary>
        public async Task DeleteAsync(Guid noteId)
        {
            var response = await _client.DeleteAsync(
                $"/api/note/{noteId}"
            );

            response.EnsureSuccessStatusCode();
        }
    }
}
