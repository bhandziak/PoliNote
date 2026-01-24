using PoliNote.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace PoliNote.Services;

public class PrivateCalendarService
{
    private readonly HttpClient _client = ApiClient.Client;

    public async Task<List<PrivateEvent>> GetByDateAsync(DateTime date)
    {
        var url = $"/api/calendar/private?date={date:yyyy-MM-dd}";
        return await _client.GetFromJsonAsync<List<PrivateEvent>>(url)
               ?? new();
    }

    public async Task<PrivateEvent> GetByIdAsync(int id)
    {
        return await _client.GetFromJsonAsync<PrivateEvent>(
            $"/api/calendar/private/{id}");
    }

    public async Task CreateAsync(CreatePrivateEventRequest request)
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

    public async Task UpdateNoteAsync(int id, string note)
    {
        await _client.PatchAsJsonAsync(
            $"/api/calendar/private/{id}",
            new { note });
    }

    public async Task DeleteAsync(int id)
    {
        await _client.DeleteAsync($"/api/calendar/private/{id}");
    }
}
