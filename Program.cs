using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using http;
using requst;
using read;
namespace Final
{
    class Program
    {
        static void Main(string[] args)
        {
            

            read.Read read;
            int port = 80;
            IPAddress iPAddress = IPAddress.Any;
            if(Array.Exists(args,e => e== "--cache"))
            {
                read =new read.Read(Convert.ToInt32(args[Array.IndexOf(args,"--cache")+1]));
            }else{
                read = new read.Read();
            }
            
            if(Array.Exists(args,e => e == "--port"))
            {
               port = Convert.ToInt32(args[Array.IndexOf(args,"--port")+1]);
            }
            if(Array.Exists(args,e => e == "--addr"))
            {
                iPAddress = IPAddress.Parse(args[Array.IndexOf(args,"--addr")+1]);
            }

            TcpListener listener = new TcpListener(iPAddress,port);
            listener.Start();
            System.Console.WriteLine("Server Run in {0}:{1}",iPAddress.ToString(),port);
            while (true)
            {
                TcpClient client = listener.AcceptTcpClient();
                Thread thread = new Thread(() =>
                {
                    NetworkStream stream = client.GetStream();
                    Byte[] buffer = new Byte[2048];
                    int size;
                    string decodedata;
                    size = stream.Read(buffer, 0, buffer.Length);
                    decodedata = Encoding.UTF8.GetString(buffer,0,size);

                    http.HTTP http = new http.HTTP(decodedata);
                    // System.Console.WriteLine(decodedata);
                    if(File.Exists("root\\"+http.Path)){
                        System.Console.WriteLine(http.Path);
                        read.FileData file = read.reader("root\\"+http.Path);
                        requst.Resqust resqust = new requst.Resqust(file);
                        // requst.Resqust resqust = new requst.Resqust(decodedata);
                        // string respone =resqust.make_respone();
                        string respone = "";
                        buffer = Encoding.UTF8.GetBytes(respone);
                        stream.Write(buffer,0,buffer.Length);
                        stream.Write(file.Content,0,file.Size);
                    }else{
                        string respone  = requst.Resqust.notfound();
                        
                        stream.Write(Encoding.UTF8.GetBytes(respone));

                    }
                        stream.Close();
                        client.Close();
                });
                thread.Start();
            }

        }
    }
}