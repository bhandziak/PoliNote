using System.Net;
using System.Net.Http.Json;

namespace PoliNote.Services;

public class AuthService
{
    private readonly HttpClient _client = ApiClient.Client;

    public async Task<bool> LoginAsync(string username, string password)
    {
        var response = await _client.PostAsJsonAsync(
            "api/auth/login",
            new
            {
                username,
                password
            });

        return response.StatusCode == HttpStatusCode.OK;
    }

    public async Task LogoutAsync()
    {
        await _client.PostAsync("api/auth/logout", null);
    }
}