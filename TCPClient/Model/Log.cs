namespace TCPClient
{
    [Serializable]
    public struct Log
    {
        public string? IpAddress { get; set; }
        public string? DateAndTime { get; set; }
        public string? Url { get; set; }
        public Int32? StatusCode { get; set; }        
        public Int32? Size { get; set; }
        public string? TypeRequest { get; set; }
        public string? User { get; set; }
        
        public Log(string ipAddress, string? user, string? dateAndTime, string typeRequest, string? url, int? statusCode, int? size)
        {
            User = user;
            TypeRequest = typeRequest;
            IpAddress = ipAddress;
            DateAndTime = dateAndTime;
            Url = url;
            StatusCode = statusCode;            
            Size = size;
        }
    }
}
