using CodeAnalysisAgent.Model;
using CodeAnalysisAgent.Services;

namespace CodeAnalysisAgent.ViewModel;

public class MainViewModel
{    
    private readonly IAgentService _agentService;
    public List<ChatMessage> Messages { get; set; } = new();

    public MainViewModel(IAgentService agentService)
    {
		_agentService = agentService;
    }

    public async Task<string> SendMessageAsync(string message)
    {
        return await _agentService.SendMessage(message);
    }
}
