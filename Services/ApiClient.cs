using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;

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
                BaseAddress = new Uri("http://192.168.0.110:5275/")
            };

            return _client;
        }
    }

    public static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true,
        Converters =
        {
            new JsonStringEnumConverter(JsonNamingPolicy.CamelCase)
        }
    };
}