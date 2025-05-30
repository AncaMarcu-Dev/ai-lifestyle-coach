
using System.Text.Json.Serialization;

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
            var requestPayload = new
            {
                model = "llama3.2:1b",
                messages = new[]
    {
        new { role = "user", content = prompt }
    },
                stream = false
            };



            var response = await _httpClient.PostAsJsonAsync("/api/chat", requestPayload);
            response.EnsureSuccessStatusCode();
            string rawResponse = await response.Content.ReadAsStringAsync();
            Console.WriteLine(rawResponse);

            var responseData = await response.Content.ReadFromJsonAsync<OllamaResponse>();
            return responseData?.Choices[0].Message.Content ?? string.Empty;
        }
        public class OllamaResponse
        {
            [JsonPropertyName("id")]
            public string Id { get; set; }

            [JsonPropertyName("object")]
            public string Object { get; set; }

            [JsonPropertyName("created")]
            public int Created { get; set; }

            [JsonPropertyName("choices")]
            public List<Choice> Choices { get; set; }
        }

        public class Choice
        {
            [JsonPropertyName("index")]
            public int Index { get; set; }

            [JsonPropertyName("message")]
            public Message Message { get; set; }

            [JsonPropertyName("finish_reason")]
            public string FinishReason { get; set; }
        }

        public class Message
        {
            [JsonPropertyName("role")]
            public string Role { get; set; }

            [JsonPropertyName("content")]
            public string Content { get; set; }
        }
    }
}
