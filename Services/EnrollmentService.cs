using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace PoliNote.Services;

public class EnrollmentService
{
    private readonly HttpClient _client;

    public EnrollmentService()
    {
        _client = ApiClient.Client;
    }

    /// <summary>
    /// Zapis do grupy
    /// </summary>
    public async Task EnrollAsync(Guid subjectGroupId)
    {
        var response = await _client.PostAsync(
            $"/api/subjects/groups/{subjectGroupId}/enroll",
            null
        );

        response.EnsureSuccessStatusCode();
    }

    /// <summary>
    /// Wypis z grupy
    /// </summary>
    public async Task UnEnrollAsync(Guid subjectGroupId)
    {
        var response = await _client.DeleteAsync(
            $"/api/subjects/groups/{subjectGroupId}/unenroll"
        );

        response.EnsureSuccessStatusCode();
    }

    /// <summary>
    /// Aktualizacja liczby nieobecności
    /// </summary>
    public async Task UpdateAbsencesAsync(Guid subjectGroupId, int absences)
    {
        var response = await _client.PatchAsync(
            $"/api/subjects/groups/{subjectGroupId}/absences",
            JsonContent.Create(absences)
        );

        response.EnsureSuccessStatusCode();
    }
}
