Console.WriteLine("Code Analysis Agent");
Console.WriteLine("-------------------");

var ollama = new OllamaClient();

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
        string answer = await ollama.GenerateAsync(input);

        Console.WriteLine();
        Console.WriteLine("AI: " + answer);
        Console.WriteLine();
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error: {ex.Message}");
    }
}