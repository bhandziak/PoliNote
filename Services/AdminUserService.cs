using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net.Http.Json;
using PoliNote.DTOs.Users;

namespace PoliNote.Services;

public class AdminUserService
{
    private readonly HttpClient _client;

    public AdminUserService()
    {
        _client = ApiClient.Client;
    }

    /// <summary>
    /// Pobranie wszystkich użytkowników (admin)
    /// GET /api/admin/users
    /// </summary>
    public async Task<List<UserDto>> GetAllAsync()
    {
        return await _client.GetFromJsonAsync<List<UserDto>>(
                   "/api/admin/users",
                   ApiClient.JsonOptions
                   )
               ?? new List<UserDto>();
    }

    /// <summary>
    /// Pobranie pojedynczego użytkownika po Id
    /// (używane np. w ekranie zmiany roli)
    /// </summary>
    public async Task<UserDto> GetByIdAsync(Guid userId)
    {
        var users = await GetAllAsync();
        return users.First(u => u.Id == userId);
    }

    /// <summary>
    /// Utworzenie nowego użytkownika
    /// POST /api/admin/users
    /// </summary>
    public async Task CreateAsync(CreateUserDto dto)
    {
        var response = await _client.PostAsJsonAsync(
            "/api/admin/users",
            dto);

        response.EnsureSuccessStatusCode();
    }

    /// <summary>
    /// Zmiana roli użytkownika
    /// PATCH /api/admin/users/{userId}/role
    /// </summary>
    public async Task ChangeRoleAsync(Guid userId, UserRole newRole)
    {
        var dto = new ChangeRoleDto
        {
            NewRole = newRole
        };

        var response = await _client.PatchAsJsonAsync(
            $"/api/admin/users/{userId}/role",
            dto);

        response.EnsureSuccessStatusCode();
    }

    /// <summary>
    /// Usunięcie użytkownika (soft delete)
    /// DELETE /api/admin/users/{userId}
    /// </summary>
    public async Task DeleteAsync(Guid userId)
    {
        var response = await _client.DeleteAsync(
            $"/api/admin/users/{userId}");

        response.EnsureSuccessStatusCode();
    }
}
