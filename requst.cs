using read;
namespace requst{

class Resqust
    {
        private read.FileData data;
        public Resqust(read.FileData decodedata)
        {
            data = decodedata;
        }
    

        public string make_respone()
        {
            string res = 
            "HTTP/1.1 200 OK \n"+     
            String.Format("Content-type: {0} \n",data.Type)+    
            String.Format("Content-Length: {0}\n\n",data.Type);
            return res;
        }
        public static string notfound(){
            string res = 
            "HTTP/1.1 404 Not Found\r\n" +
            "Content-Type: text/plain\r\n" +
            "Content-Length: 13\r\n" +
            "\r\n" +
            "404 Not Found";
            return res;
        }
    }
}
