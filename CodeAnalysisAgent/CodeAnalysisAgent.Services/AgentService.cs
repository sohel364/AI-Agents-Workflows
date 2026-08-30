using CodeAnalysisAgent.Model;

namespace CodeAnalysisAgent.Services;

public class AgentService : IAgentService
{
	private readonly IOllamaClient _ollamaClient;
	private readonly List<ChatMessage> _messages = new();

	public AgentService(IOllamaClient ollamaClient)
	{
		_ollamaClient = ollamaClient;
	}

	public async Task<string> SendMessage(string message)
	{
		_messages.Add(new ChatMessage
		{
			Role = "user",
			Content = message
		});

		string response = await _ollamaClient.GetResponseAsync(_messages);

		_messages.Add(new ChatMessage
		{
			Role = "assistant",
			Content = response
		});
		return response;
	}
}
