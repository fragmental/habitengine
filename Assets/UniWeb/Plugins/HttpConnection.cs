
using System;
using System.Net.Sockets;
using System.IO;

namespace HTTP
{
    public class HttpConnection : IDisposable
    {
        public string host;
        public int port;
        public TcpClient client = null;
        public Stream stream = null;
        
        public HttpConnection ()
        {
            
        }
        
        public void Connect ()
        {
            client = new TcpClient ();
            client.Connect (host, port);
        }

        public void Dispose ()
        {
            stream.Dispose ();
            client.Close ();
            client.Client.Close ();
        }
        
    }
}

