
namespace LifestyleCoach.Services
{
    public class OllamaService
    {
        private readonly HttpClient _httpClient;

        public OllamaService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> SendPromptAsync(string prompt)
        {
            var requestPayload = new { prompt };

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
