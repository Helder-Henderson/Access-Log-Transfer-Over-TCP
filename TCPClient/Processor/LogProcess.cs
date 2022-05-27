using System.Text.RegularExpressions;

namespace TCPClient
{
    public struct LogProcess
    {
        public string Pattern;
        public Regex Regex;
        public LogProcess()
        {
            Pattern = "^(\\S+) (\\S+) (\\S+) \\[([^\\]]+)\\] \"([A-Z]+) ([^ \"]+)? HTTP/[0-9.]+\" ([0-9]{3}) ([0-9]+|-) \"([^\"]*)\" \"([^\"]*)\" \"([^\"]*)\"";
            Regex = new Regex(Pattern, RegexOptions.Compiled);
        }
        public Log logProcessorValidation(string line)
        {
            Match _ = Regex.Match(line);

            return new Log()
            {
                TypeRequest = (_.Groups[(int)PatternCommonFormat.TYPE_REQUEST].Value == "-" || _.Groups[(int)PatternCommonFormat.TYPE_REQUEST].Value.Length == 0) ? null : _.Groups[(int)PatternCommonFormat.TYPE_REQUEST].Value,

                IpAddress = (_.Groups[(int)PatternCommonFormat.IP_CLIENT].Value == "-" || _.Groups[(int)PatternCommonFormat.IP_CLIENT].Value.Length == 0) ? null : _.Groups[(int)PatternCommonFormat.IP_CLIENT].Value,

                DateAndTime = _.Groups[(int)PatternCommonFormat.DATETIME].Value.Substring(0,20),

                StatusCode = (_.Groups[(int)PatternCommonFormat.STATUSCODE].Value.Length == 0 || _.Groups[(int)PatternCommonFormat.STATUSCODE].Value == "-") ? null : Int32.Parse(_.Groups[(int)PatternCommonFormat.STATUSCODE].Value),

                Size = (_.Groups[(int)PatternCommonFormat.SIZE].Value.Length == 0 || _.Groups[(int)PatternCommonFormat.SIZE].Value == "-") ? null : Int32.Parse(_.Groups[(int)PatternCommonFormat.SIZE].Value),

                User = (_.Groups[(int)PatternCommonFormat.USER].Value.Length == 0 || _.Groups[(int)PatternCommonFormat.USER].Value == "-") ? null : _.Groups[(int)PatternCommonFormat.USER].Value,                

                Url = (_.Groups[(int)PatternCommonFormat.URL].Value == "-" || _.Groups[(int)PatternCommonFormat.URL].Value.Length == 0) ? null : _.Groups[(int)PatternCommonFormat.URL].Value
            };

        }

    }
}
