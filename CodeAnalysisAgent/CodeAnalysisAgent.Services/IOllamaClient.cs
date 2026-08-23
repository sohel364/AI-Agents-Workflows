namespace CodeAnalysisAgent.Services;

public interface IOllamaClient
{
    Task<string> GetResponseAsync(string prompt);
}