using System.Net.Http.Json;

public class OllamaClient
{
    private readonly HttpClient _httpClient;

    public OllamaClient()
    {
        _httpClient = new HttpClient
        {
            BaseAddress = new Uri("http://localhost:11434")
        };
    }

    public async Task<string> GenerateAsync(string prompt)
    {
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