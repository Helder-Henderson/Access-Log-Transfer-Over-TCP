using System.Diagnostics;
using System.Text.Json;
using TCPClient;

Connection connection = new(Settings.IP_HOST, Settings.PORT);
connection.StartConnection();

LogProcess _logProcessor = new();

List<string> messages = new();

var StopWath = new Stopwatch();
StopWath.Start();

try
{
    using var file = new StreamReader(Settings.PATH_FILE);
    string? line;

    while ((line = file.ReadLine()) != null)
    {
        try
        {
            if (_logProcessor.Regex.IsMatch(line))
            {
                var _ = _logProcessor.logProcessorValidation(line);
                var json_ = JsonSerializer.Serialize<Log>(_);
                messages.Add(json_);
            }
        }

        catch { break; }
    }

    await connection.SendMessageAsync(messages);
    messages.Clear();
    connection.CloseAll();
    file.Close();


    StopWath.Stop();
    Console.WriteLine($"Tempo de execução : {StopWath.Elapsed}");
}

catch { }