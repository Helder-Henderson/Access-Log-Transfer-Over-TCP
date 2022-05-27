using System.Net.Sockets;

namespace TCPClient
{
    internal struct Connection
    {
        private TcpClient _tcpClient { get; set; }
        private string IPHost { get; set; }
        private int Port { get; set; }

        public Connection(string ipHost, int port)
        {
            IPHost = ipHost;
            Port = port;
            _tcpClient = new TcpClient();
        }

        public void StartConnection()
        {
            if (_tcpClient.Connected)
                Console.WriteLine("Um Cliente já está conectado");
            else
            {
                try
                {
                    _tcpClient.Connect(IPHost, Port);
                }
                catch
                {
                    Console.WriteLine("Tentativa de conexão mal sucedida");
                    StartConnection();
                }
            }
        }

        public async Task<bool> SendMessageAsync(List<string> messages)
        {
            if (_tcpClient != null)
            {
                NetworkStream networkStream = _tcpClient.GetStream();
                StreamWriter streamWriter = new(networkStream);
                streamWriter.AutoFlush = true;

                try
                {
                    if (networkStream.CanWrite)
                    {
                        foreach (string message in messages)
                        {
                            await streamWriter.WriteLineAsync(message);
                        }
                    }
                    return true;
                }

                catch { return false; }
            }
            return true;
        }

        public void CloseAll()
        {
            _tcpClient.Dispose();
            _tcpClient.Close();
        }
    }
}
