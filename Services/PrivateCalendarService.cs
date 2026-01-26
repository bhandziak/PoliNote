using PoliNote.DTOs.PrivateCalendar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

using PoliNote.DTOs.Notes;

namespace PoliNote.Services;

public class PrivateCalendarService
{
    private readonly HttpClient _client = ApiClient.Client;

    public async Task<List<PrivateEventDto>> GetByDateAsync(DateTime date)
    {
        var url = $"/api/calendar/private?date={date:yyyy-MM-dd}";
        return await _client.GetFromJsonAsync<List<PrivateEventDto>>(url)
               ?? new();
    }

    public async Task<PrivateEventDetailsDto?> GetByIdAsync(Guid id)
    {
        try
        {
            var result = await _client.GetFromJsonAsync<PrivateEventDetailsDto>(
                $"/api/calendar/private/{id}"
            );

            if (result == null)
            {
                await Shell.Current.DisplayAlert(
                    "Błąd",
                    "Serwer zwrócił pustą odpowiedź.",
                    "OK"
                );
                return null;
            }

            await Shell.Current.DisplayAlert(
                "Sukces",
                "Szczegóły wydarzenia zostały pobrane.",
                "OK"
            );

            return result;
        }
        catch (HttpRequestException ex)
        {
            await Shell.Current.DisplayAlert(
                "Błąd sieci",
                $"Nie udało się pobrać wydarzenia.\n\n{ex.Message}",
                "OK"
            );
            return null;
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert(
                "Błąd",
                $"Wystąpił nieoczekiwany błąd:\n\n{ex.Message}",
                "OK"
            );
            return null;
        }
    }

    public async Task CreateAsync(PrivateEventRequestDto request)
    {
        var response = await _client.PostAsJsonAsync(
            "/api/calendar/private",
            request
        );

        if (!response.IsSuccessStatusCode)
        {
            var errorBody = await response.Content.ReadAsStringAsync();

            throw new Exception(
                $"HTTP {(int)response.StatusCode} ({response.StatusCode})\n{errorBody}"
            );
        }
    }

    public async Task<NoteDto?> GetNoteAsync(Guid eventId)
    {
        return await _client.GetFromJsonAsync<NoteDto>(
            $"/api/notes/{eventId}"
        );
    }

    public async Task UpdateNoteAsync(Guid eventId, NoteRequestDto request)
    {
        var response = await _client.PatchAsJsonAsync(
            $"/api/calendar/private/{eventId}/note",
            request
        );

        response.EnsureSuccessStatusCode();
    }


    public async Task DeleteAsync(Guid id)
    {
        await _client.DeleteAsync($"/api/calendar/private/{id}");
    }
}
