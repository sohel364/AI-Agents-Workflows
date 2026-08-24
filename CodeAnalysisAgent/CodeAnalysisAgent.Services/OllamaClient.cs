using CodeAnalysisAgent.Model;
namespace CodeAnalysisAgent.Services;

using System.Net.Http.Json;

public class OllamaClient : IOllamaClient
{
    private readonly HttpClient _httpClient;

    public OllamaClient(HttpClient httpClient)
    {
        _httpClient = httpClient;   
        _httpClient.BaseAddress = new Uri("http://localhost:11434");
    }

    public async Task<string> GetResponseAsync(IReadOnlyList<ChatMessage> messages)
    {
        string prompt = string.Join("\n", messages.Select(m => $"{m.Role}: {m.Content}"));

        var request = new
        {
            model = "llava:7b",
            system = """
                    You are CodeAnalysisAgent, a helpful software development assistant.
                    You specialize in C++, C#, Windows and software debugging.
                    Give clear and practical answers.
                    """,
            prompt = prompt,
            stream = false
        };

        var response = await _httpClient.PostAsJsonAsync(
            "/api/generate",
            request);

        response.EnsureSuccessStatusCode();

        var result =
            await response.Content.ReadFromJsonAsync<OllamaResponse>();

        return result?.Response ?? "";
    }
}

public class OllamaResponse
{
    public string? Response { get; set; }
}