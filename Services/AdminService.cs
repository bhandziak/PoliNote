using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net.Http.Json;
using PoliNote.DTOs.Users;

namespace PoliNote.Services;

public class AdminService
{
    private readonly HttpClient _client;

    public AdminService()
    {
        _client = ApiClient.Client;
    }

    /// <summary>
    /// Lista wszystkich użytkowników (admin)
    /// </summary>
    public async Task<List<UserDto>> GetUsersAsync()
    {
        return await _client.GetFromJsonAsync<List<UserDto>>(
                   "/api/admin/users")
               ?? new List<UserDto>();
    }

    /// <summary>
    /// Utworzenie nowego użytkownika (aktywny od razu)
    /// </summary>
    public async Task CreateUserAsync(CreateUserDto dto)
    {
        var response = await _client.PostAsJsonAsync(
            "/api/admin/users",
            dto
        );

        response.EnsureSuccessStatusCode();
    }

    /// <summary>
    /// Zmiana roli użytkownika
    /// </summary>
    public async Task ChangeRoleAsync(Guid userId, UserRole newRole)
    {
        var dto = new ChangeRoleDto
        {
            NewRole = newRole
        };

        var response = await _client.PatchAsJsonAsync(
            $"/api/admin/users/{userId}/role",
            dto
        );

        response.EnsureSuccessStatusCode();
    }

    /// <summary>
    /// Usunięcie użytkownika (soft delete)
    /// </summary>
    public async Task DeleteUserAsync(Guid userId)
    {
        var response = await _client.DeleteAsync(
            $"/api/admin/users/{userId}"
        );

        response.EnsureSuccessStatusCode();
    }
}