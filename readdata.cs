using System.Drawing;
using System.Dynamic;
using System.Net.Http.Headers;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace read
{
    class FileData
    {
        Byte[] content;
        string type;
        int size;
        public FileData(Byte[] _content, string _type)
        {
            content = _content;
            type = _type;
            size = _content.Length;

        }
        public Byte[] Content
        {
            get
            {
                return content;
            }
        }
        public string Type
        {
            get
            {
                return type;
            }
        }
        public int Size
        {
            get
            {
                return size;
            }
        }
    }
    class Read
    {
        private bool is_cache = false;
        private Dictionary<string, FileData> cache;
        private int max_size;
        public Read(int _max_size = 10000)
        {

            is_cache =true;
            max_size = _max_size;
            if (is_cache)
            {
                cache = new Dictionary<string, FileData>();
            }
        }

        public Read()
        {
            is_cache = false;
        }

        public FileData reader(string path)
        {
            if (is_cache)
            {
                return readfile_with_cache(path);
            }
            return readfile(path);
        }

        private FileData readfile(string path)
        {
            FileData fileData = new FileData(File.ReadAllBytes(path), Path.GetExtension(path));
            return fileData;
        }

        private FileData readfile_with_cache(string path)
        {
            if (cache.ContainsKey(path))
            {
                System.Console.WriteLine("Read From Cache ");
                return cache[path];
            }
            FileData data = readfile(path);
            if (data.Size < max_size)
            {
                cache.Add(path, data);
            }
            return data;
        }

    }
}