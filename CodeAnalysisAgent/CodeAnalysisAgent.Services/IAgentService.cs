namespace CodeAnalysisAgent.Services;

public interface IAgentService
{
    Task<string> SendMessage(string message);
}
