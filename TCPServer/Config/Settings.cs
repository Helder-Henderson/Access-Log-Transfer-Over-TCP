namespace TCPServer
{
    internal struct Settings
    {
        //DATABASE Settings
        public static string ConnectionString = "mongodb://helder:%40mongo123@localhost:27017/admin";
        public static string DatabaseName = "TCP";
        public static string ColletionName = "AccessLogTCP";

        //Server Settings
        public static string IP = "127.0.0.1";
        public static int PORT = 8000;

    }
}
