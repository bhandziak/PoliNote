using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PoliNote.DTOs.Subjects.Requests;
using PoliNote.DTOs.Subjects.Responses;
using System.Net.Http.Json;

namespace PoliNote.Services;

public class SubjectService
{
    private readonly HttpClient _client;

    public SubjectService()
    {
        _client = ApiClient.Client;
    }

    // 1️⃣ Lista przedmiotów (ekran główny "Przedmioty")
    public async Task<List<SubjectDto>> GetSubjectsAsync()
        => await _client.GetFromJsonAsync<List<SubjectDto>>(
            "/api/subjects") ?? new();

    // 2️⃣ Szczegóły przedmiotu + grupy
    public async Task<SubjectWithGroupsDto> GetSubjectByIdAsync(Guid subjectId)
        => await _client.GetFromJsonAsync<SubjectWithGroupsDto>(
            $"/api/subjects/{subjectId}")
           ?? throw new Exception("Nie udało się pobrać szczegółów przedmiotu");

    // 3️⃣ Wszystkie grupy (do podziału: zapisane / niezapisane)
    public async Task<List<SubjectGroupDto>> GetSubjectGroupsAsync()
        => await _client.GetFromJsonAsync<List<SubjectGroupDto>>(
            "/api/subjects/groups") ?? new();

    // 4️⃣ Szczegóły jednej grupy
    public async Task<SubjectGroupDetailsDto> GetSubjectGroupByIdAsync(Guid groupId)
        => await _client.GetFromJsonAsync<SubjectGroupDetailsDto>(
            $"/api/subjects/groups/{groupId}")
           ?? throw new Exception("Nie udało się pobrać grupy");

    public async Task<Guid> CreateSubjectAsync(SubjectRequestDto dto)
    {
        var response = await _client.PostAsJsonAsync("/api/subjects", dto);

        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync();
            throw new Exception(
                $"CreateSubject failed ({(int)response.StatusCode}): {error}"
            );
        }

        return await response.Content.ReadFromJsonAsync<Guid>();
    }

    public async Task CreateSubjectGroupAsync(Guid subjectId, SubjectGroupRequestDto dto)
    {
        var response = await _client.PostAsJsonAsync(
            $"/api/subjects/{subjectId}/groups",
            dto);

        response.EnsureSuccessStatusCode();
    }
}