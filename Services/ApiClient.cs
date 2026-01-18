using System.Net;
using System.Net.Http.Json;

namespace PoliNote.Services;

public static class ApiClient
{
    private static readonly CookieContainer CookieContainer = new();
    private static HttpClient _client;

    public static HttpClient Client
    {
        get
        {
            if (_client != null)
                return _client;

            var handler = new HttpClientHandler
            {
                CookieContainer = CookieContainer,
                UseCookies = true,

                // DEV ONLY – pozwala na localhost HTTPS
                ServerCertificateCustomValidationCallback =
                    HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
            };

            _client = new HttpClient(handler)
            {
                BaseAddress = new Uri("https://localhost:7040/")
            };

            return _client;
        }
    }
}