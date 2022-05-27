using System.Net;
using System.Net.Sockets;
using System.Text.Json;

namespace TCPServer
{
    internal struct Connection
    {
        public TcpClient TcpClient { get; set; }
        private TcpListener TcpListener { get; set; }
        private string IP { get; set; }
        private int Port { get; set; }
        private MongoDB Database { get; set; }

        public Connection(string ip, int port)
        {
            IP = ip;
            Port = port;
            TcpClient = new TcpClient();
            TcpListener = new TcpListener(IPAddress.Parse(ip), port);
            Database = new MongoDB();
        }

        public void StartListen()
        {
            TcpListener.Start();
        }

        public void StartConnection()
        {
            if (TcpClient.Connected)
                Console.WriteLine("A Client already is connection");
            else
            {
                try
                {
                    TcpClient = TcpListener.AcceptTcpClient();
                }
                catch
                {
                    Console.WriteLine("Tentative connect failed");
                    StartConnection();
                }
            }
        }

        public void CloseAllConnection()
        {
            TcpClient.Dispose();
            TcpClient.Close();
            TcpListener.Stop();
        }

        public Task<long>? GetMessage()
        {
            Task<long>? count = null;
            NetworkStream networkStream = TcpClient.GetStream();
            StreamReader streamReader = new(networkStream);
            var dataDeserialized = new Log();
            try
            {
                while (!streamReader.EndOfStream)
                {
                    string? dataLine = streamReader.ReadLine();
                    try
                    {                        
                        if (dataLine != null)
                            dataDeserialized = JsonSerializer.Deserialize<Log>(dataLine);
                        
                        if (dataDeserialized != null)
                            Database.InsertToList(dataDeserialized);
                    }
                    catch { continue; }
                }
                count = Database.StartBulkInsert();
                return count;
            }
            catch { return count; }
        }
    }
}