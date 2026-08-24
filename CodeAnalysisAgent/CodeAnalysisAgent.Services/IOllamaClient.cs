using CodeAnalysisAgent.Model;
namespace CodeAnalysisAgent.Services;

public interface IOllamaClient
{
    Task<string> GetResponseAsync(IReadOnlyList<ChatMessage> messages);
}