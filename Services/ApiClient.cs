using System.Net;
using System.Net.Http.Json;

namespace PoliNote.Services;

public class ApiClient
{
    private static readonly CookieContainer _cookieContainer = new();
    private static HttpClient _httpClient;

    public static HttpClient Client
    {
        get
        {
            if (_httpClient != null)
                return _httpClient;

            var handler = new HttpClientHandler
            {
                CookieContainer = _cookieContainer,
                UseCookies = true
            };

            _httpClient = new HttpClient(handler)
            {
                BaseAddress = new Uri("https://localhost:7040/") //adres backendu ma byc
            };

            return _httpClient;
        }
    }
}