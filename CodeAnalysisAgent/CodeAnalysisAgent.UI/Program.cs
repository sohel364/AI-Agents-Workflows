using CodeAnalysisAgent.ViewModel;
using CodeAnalysisAgent.Services;

using System.Net.Http;

Console.WriteLine("Code Analysis Agent");
Console.WriteLine("-------------------");

IOllamaClient  ollamaClient = new OllamaClient(new HttpClient());
IAgentService agentService = new AgentService(ollamaClient);

var viewModel = new MainViewModel(agentService);

while (true)
{
    Console.Write("You: ");

    string? input = Console.ReadLine();

    if (string.IsNullOrWhiteSpace(input))
        continue;

    if (input.Equals("exit", StringComparison.OrdinalIgnoreCase))
        break;

    try
    {
        string answer = await viewModel.SendMessageAsync(input);

        Console.WriteLine();
        Console.WriteLine("AI: " + answer);
        Console.WriteLine();
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error: {ex.Message}");
    }
}