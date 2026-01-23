using PoliNote.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace PoliNote.Services;


public class PublicCalendarService
{
    private readonly HttpClient _client = ApiClient.Client;

    public async Task<List<PublicEvent>> GetPublicEventsByDateAsync(DateTime date)
    {
        var dateString = date.ToString("dd.MM.yyyy", CultureInfo.InvariantCulture);

        var url = $"api/calendar/public?date={dateString}";

        var events = await _client.GetFromJsonAsync<List<PublicEvent>>(url);

        return events ?? new List<PublicEvent>();
    }

    public async Task CreatePublicEventAsync(CreatePublicEventRequest request)
    {
        var response = await _client.PostAsJsonAsync(
            "api/calendar/public",
            request);

        response.EnsureSuccessStatusCode();
    }
}
