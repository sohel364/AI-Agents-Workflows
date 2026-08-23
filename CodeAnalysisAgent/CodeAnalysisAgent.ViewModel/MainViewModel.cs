using CodeAnalysisAgent.Model;
using CodeAnalysisAgent.Services;

namespace CodeAnalysisAgent.ViewModel;

public class MainViewModel
{
    public List<ChatMessage> Messages { get; set; } = new();
    private readonly IOllamaClient _ollamaClient;

    public MainViewModel(IOllamaClient ollamaClient)
    {
        _ollamaClient = ollamaClient;
    }

    public async Task<string> SendMessageAsync(string message)
    {
        // Add user message to the list
        Messages.Add(new ChatMessage { Role = "user", Content = message });

        // Get response from the AI model
        string aiResponse = await _ollamaClient.GetResponseAsync(message);

        // Add AI response to the list
        Messages.Add(new ChatMessage { Role = "assistant", Content = aiResponse });

        return aiResponse;
    }    
}
