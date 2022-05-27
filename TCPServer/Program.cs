using System.Diagnostics;
using TCPServer;

//init Listening and connection with Client
Connection connection = new Connection(Settings.IP, Settings.PORT);
connection.StartListen();
connection.StartConnection();

//Start timer
var StopWath = new Stopwatch();
StopWath.Start();

do
{
    var insertCounted = connection.GetMessage();
    connection.CloseAllConnection();

    StopWath.Stop();
    if (insertCounted != null)
    {
        Console.WriteLine($"Linhas populadas no banco:{await insertCounted}");
        Console.WriteLine($"Tempo de execução : {StopWath.Elapsed}");
    }

} while (connection.TcpClient.Connected);