using System;
using System.Net.Sockets;
using System.IO;


namespace HTTP
{
    public class HttpConnection : IDisposable
    {
        public string host;
        public int port;
#if UNITY_WP8
        public SocketEx.TcpClient client = null;
#else
        public TcpClient client = null;
#endif
        public Stream stream = null;
        
        public HttpConnection ()
        {
            
        }
        
        public void Connect ()
        {
#if UNITY_WP8
            client = new SocketEx.TcpClient ();
#else
            client = new TcpClient ();
#endif

            client.Connect (host, port);
        }

        public void Dispose ()
        {
            stream.Dispose ();
#if UNITY_WP8
            client.Dispose ();
#else

#endif


        }
        
    }
}

