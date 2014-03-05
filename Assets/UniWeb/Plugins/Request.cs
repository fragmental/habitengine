#define USE_GZIP
//#define USE_COOKIES
#define USE_SSL
using UnityEngine;
using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading;

#if USE_SSL
using System.Net.Security;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography;

#endif
using System.IO;
using System.Net;

namespace HTTP
{
    public class Request : BaseHTTP
    {

        #region public fields
        public bool isDone = false;
        public Exception exception = null;

        public Response response { get; private set; }

        public int maximumRedirects = 8;
        public bool acceptGzip = true;
        public bool useCache = false;
        public readonly Headers headers = new Headers ();
        public bool enableCookies = true;
        public float timeout = 0;



#if USE_COOKIES     
        public readonly static CookieContainer cookies = new CookieContainer ();
#endif
#endregion
        #region public properties
        public Uri uri { get; private set; }

        public HttpConnection upgradedConnection { get; private set; }

        public float Progress {
            get { return response == null ? 0 : response.progress; }
        }

        public string Text {
            set { bytes = value == null ? null : System.Text.Encoding.UTF8.GetBytes (value); }
        }
#endregion
        #region public interface
        public Coroutine Send (System.Action<Request> OnDone)
        {
            this.OnDone = OnDone;
            return Send ();
        }

        public Coroutine Send ()
        {
            BeginSending ();
            return UniWeb.Instance.StartCoroutine (_Wait ());   
        }
#endregion
        #region constructors
        public Request (string method, string uri)
        {
            this.method = method;
            this.uri = new Uri (uri);
        }

        public Request (string method, string uri, bool useCache)
        {
            this.method = method;
            this.uri = new Uri (uri);
            this.useCache = useCache;
        }

        public Request (string uri, WWWForm form)
        {
            this.method = "POST";
            this.uri = new Uri (uri);
            this.bytes = form.data;
            foreach (string k in form.headers.Keys) {
                headers.Set (k, (string)form.headers [k]);
            }
        }

        public Request (string method, string uri, byte[] bytes)
        {
            this.method = method;
            this.uri = new Uri (uri);
            this.bytes = bytes;
        }
#endregion
        #region implementation
        
#if USE_SSL
        static bool ValidateServerCertificate (object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            //This is where you implement logic to determine if you trust the certificate.
            //By default, we trust all certificates.
            return true;
        }
#endif
        
        HttpConnection CreateConnection (string host, int port, bool useSsl)
        {

            var connection = new HttpConnection () { host = host, port = port };
            connection.Connect ();
            if (useSsl) {
#if USE_SSL
                connection.stream = new SslStream (connection.client.GetStream (), false, ValidateServerCertificate);
                var ssl = connection.stream as SslStream;
                ssl.AuthenticateAsClient (uri.Host);
#endif
            } else {
                connection.stream = connection.client.GetStream ();
            }
            return connection;
        }
                
        IEnumerator Timeout ()
        {
            yield return new WaitForSeconds (timeout);
            if (!isDone) {
                exception = new TimeoutException ("Web request timed out");
                isDone = true;
            }
        }

        void AddHeadersToRequest ()
        {
            if (useCache) {
                string etag = "";
                if (etags.TryGetValue (uri.AbsoluteUri, out etag)) {
                    headers.Set ("If-None-Match", etag);
                }
            }
            var hostHeader = uri.Host;
            if (uri.Port != 80 && uri.Port != 443) {
                hostHeader += ":" + uri.Port.ToString ();
            }
            headers.Set ("Host", hostHeader);
#if USE_GZIP            
            if (acceptGzip) {
                headers.Set ("Accept-Encoding", "gzip");
            }
#endif
#if USE_COOKIES
            if (enableCookies && uri != null) {
                try {
                    var c = cookies.GetCookieHeader (uri);
                    if (c != null && c.Length > 0) {
                        headers.Set ("Cookie", c);
                    }
                } catch (NullReferenceException) {
                    //Some cookies make the .NET cookie class barf. MEGH again.
                } catch (IndexOutOfRangeException) {
                    //Another weird exception that comes through from the cookie class. 
                }
            }
#endif
        }

        void BeginSending ()
        {

            isDone = false;

            if (timeout > 0) {
                UniWeb.Instance.StartCoroutine (Timeout ());
            }

            ThreadPool.QueueUserWorkItem (new WaitCallback (delegate(object t) {
                try {
                    var retryCount = 0;
                    HttpConnection connection = null;
                    while (retryCount < maximumRedirects) {
                        AddHeadersToRequest ();
                        connection = CreateConnection (uri.Host, uri.Port, uri.Scheme.ToLower () == "https");
                        WriteToStream (connection.stream);
                        response = new Response (this);
                        response.ReadFromStream (connection.stream);


#if USE_COOKIES
                        if (enableCookies) {
                            foreach (var i in response.headers.GetAll("Set-Cookie")) {
                                try {
                                    cookies.SetCookies (uri, i);
                                } catch (System.Net.CookieException) {
                                    //Some cookies make the .NET cookie class barf. MEGH.
                                }
                            }
                        }
#endif

                        switch (response.status) {
                        case 101:
                            upgradedConnection = connection;
                            break;
                        case 304:
                            break;
                        case 307:
                            uri = new Uri (response.headers.Get ("Location"));
                            retryCount ++;
                            continue;
                        case 302:
                        case 301:
                            method = "GET";
                            uri = new Uri (response.headers.Get ("Location"));
                            retryCount ++;
                            continue;
                        default:
                            break;
                        }
                        break;
                    }

                    if (upgradedConnection == null) {   
                        connection.Dispose ();
                    }

                    if (useCache && response != null) {
                        var etag = response.headers.Get ("etag");
                        if (etag.Length > 0) {
                            etags [uri.AbsoluteUri] = etag;
                        }
                    }
                } catch (Exception e) {
                    exception = e;
                    response = null;
                }
                isDone = true;
            }));
        }

        void WriteToStream (Stream outputStream)
        {
            var stream = new BinaryWriter (outputStream);
            bool hasBody = false;
            
            stream.Write (ASCIIEncoding.ASCII.GetBytes (method.ToUpper () + " " + uri.PathAndQuery + " " + protocol));
            stream.Write (EOL);
            if (uri.UserInfo != null && uri.UserInfo != "") {
                if (!headers.Contains ("Authorization")) {
                    headers.Set ("Authorization", "Basic " + System.Convert.ToBase64String (System.Text.ASCIIEncoding.ASCII.GetBytes (uri.UserInfo)));  
                }
            }
            if (!headers.Contains ("Accept")) {
                headers.Add("Accept", "*/*");
            }
            if (bytes != null && bytes.Length > 0) {
                headers.Set ("Content-Length", bytes.Length.ToString ());
                // Override any previous value
                hasBody = true;
            } else {
                headers.Pop ("Content-Length");
            }
            
            headers.Write (stream);
            
            stream.Write (EOL);
            
            if (hasBody) {
                stream.Write (bytes);
            }
            
        }

        IEnumerator _Wait ()
        {
            while (!isDone) {
                yield return null; 
            }
            if (OnDone != null) {
                OnDone (this);
            }
        }
        
#if USE_SSL
        static void AOTStrippingReferences ()
        {
            new System.Security.Cryptography.RijndaelManaged ();
        }
#endif

        byte[] bytes;
        string method;
        string protocol = "HTTP/1.1";
        static Dictionary<string, string> etags = new Dictionary<string, string> ();
        System.Action<HTTP.Request> OnDone;
#endregion
    }


}
