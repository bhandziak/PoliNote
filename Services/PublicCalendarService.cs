using PoliNote.DTOs.PublicCalendar;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using PoliNote.ViewModels;

namespace PoliNote.Services;


public class PublicCalendarService
{
    private readonly HttpClient _client = ApiClient.Client;

    public async Task<List<PublicEventDto>> GetPublicEventsByDateAsync(DateTime date)
    {
        var dateString = date.ToString("o", CultureInfo.InvariantCulture);

        var url = $"/api/calendar/public?date={Uri.EscapeDataString(dateString)}";

        var events = await _client.GetFromJsonAsync<List<PublicEventDto>>(url);

        return events ?? new List<PublicEventDto>();
    }

    public async Task CreatePublicEventAsync(PublicEventRequestDto request)
    {
        var json = JsonSerializer.Serialize(request);
        System.Diagnostics.Debug.WriteLine(json);

        var response = await _client.PostAsJsonAsync(
            "api/calendar/public",
            request);

        response.EnsureSuccessStatusCode();
    }

    public async Task<PublicEventDto> GetPublicEventByIdAsync(Guid id)
{
    try
    {
        var response = await _client.GetAsync($"/api/calendar/public/{id}");

        if (!response.IsSuccessStatusCode)
        {
            // Możesz wyciągnąć więcej info z response.ReasonPhrase jeśli trzeba
            throw new Exception($"Błąd serwera: {response.StatusCode}");
        }

        var json = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<PublicEventDto>(json,
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
    }
    catch (Exception ex)
    {
        // Wyświetlenie alertu w interfejsie MAUI
        await Shell.Current.DisplayAlert("Błąd", $"Nie udało się pobrać wydarzenia: {ex.Message}", "OK");

        // Logowanie błędu dla programisty
        Console.WriteLine($"[GetPublicEventByIdAsync] Error: {ex.StackTrace}");

        // Zwracamy null lub rzucamy błąd dalej, zależnie od logiki Twojej aplikacji
        return null; 
    }
}
}
