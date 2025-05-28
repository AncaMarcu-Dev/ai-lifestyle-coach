using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace LifestyleCoach.Services
{
    public class OllamaService
    {
        private readonly HttpClient _httpClient;

        public OllamaService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("http://localhost:11434");
        }

        public async Task<string> SendPromptAsync(string prompt)
        {
            var requestPayload = new { prompt = prompt };

            var response = await _httpClient.PostAsJsonAsync("/api/generate", requestPayload);
            response.EnsureSuccessStatusCode();

            var responseData = await response.Content.ReadFromJsonAsync<OllamaResponse>();
            return responseData?.Text ?? string.Empty;
        }

        private class OllamaResponse
        {
            public string Text { get; set; }
        }
    }
}
