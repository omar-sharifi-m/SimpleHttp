using System.Text.RegularExpressions;

namespace http
{
    class HTTP
    {
        private string data;
        private string path;
        private string host;
        private string pattern = "GET (.*) HTTP.*\nHost: (.*)";

        public HTTP(string _data)
        {
            data = _data;
            Match match = Regex.Match(data, pattern);
            path = match.Groups[1].ToString();
            host = match.Groups[2].ToString();


        }
        public string Path{
            get{
                if(path == "//"){
                    return "index.html";
                }
                return path;
            }

        }
        public string Host{
            get{
                return host; 
            }
        }
        
    }
}