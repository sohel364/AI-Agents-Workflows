using CodeAnalysisAgent.Model;
using CodeAnalysisAgent.Services;

namespace CodeAnalysisAgent.ViewModel;

public class MainViewModel
{    
    private readonly IOllamaClient _ollamaClient;
    public List<ChatMessage> Messages { get; set; } = new();

    public MainViewModel(IOllamaClient ollamaClient)
    {
        _ollamaClient = ollamaClient;
    }

    public async Task<string> SendMessageAsync(string message)
    {
        // Add user message to the list
        Messages.Add(new ChatMessage { Role = "user", Content = message });

        // Get response from the AI model
        string aiResponse = await _ollamaClient.GetResponseAsync(Messages);

        // Add AI response to the list
        Messages.Add(new ChatMessage { Role = "assistant", Content = aiResponse });

        return aiResponse;
    }    
}
