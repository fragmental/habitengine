using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UniExtensions;

namespace HTTP.Server
{
    public class HttpServer : MonoBehaviour
    {

        public int port = 8080;
        public bool startServerOnLoad = true;

        public bool logRequests = true;

        Socket listener;
        HttpRequestHandler[] routes;

        public void StartServing ()
        {
            MagicThread.Start (ServeHTTP (), false);
        }

        void Start ()
        {
            Application.runInBackground = true;
            routes = GetComponentsInChildren<HttpRequestHandler> ();
            if (startServerOnLoad)
                StartServing ();
        }

        void OnApplicationQuit ()
        {
            Shutdown ();
        }

        void Shutdown ()
        {
            try {
                listener.Shutdown (SocketShutdown.Both);
                listener.Close ();
            } catch (ThreadAbortException) {
                return;
            } catch (SocketException) {
                return;
            }
        }

        void RouteRequest(Request request) {
            var path = request.uri.AbsolutePath;
            var found = false;
            foreach(var r in routes) {
                if(r.path == path) {
                    found = true;
                    r.Dispatch(request);
                }
            }
            if(!found) {
                request.response.status = 404;
                request.response.message = "Not Found";
                request.response.Text = "Not found!";
            }

        }

        IEnumerator ServeHTTP ()
        {

            yield return null;

            listener = new Socket (AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            var endPoint = new IPEndPoint (IPAddress.Any, port);
            listener.Bind (endPoint);
            listener.Listen (5);
            var background = new BackgroundTask();
            var foreground = new ForegroundTask();

            var host = "http://localhost:" + port.ToString();
            while (true) {

                yield return background;
                Request request = null;
                NetworkStream stream = null;
                try {
                    var client = listener.Accept ();
                    stream = new NetworkStream (client);
                    request = Request.BuildFromStream (host, stream);
                } catch (HTTPException) {
                    Shutdown ();
//                    return false;
                } catch(ThreadAbortException) {
                    Shutdown ();
//                    return false;
                } catch (Exception e) {
                    Debug.LogError ("Exception in server thread: " + e.ToString ());
                    Shutdown ();
//                    return false;
                }

                yield return foreground;
                RouteRequest(request) ;
                yield return background;
                try {
                    request.response.headers.Set("Connection", "Close");
                    HTTPProtocol.WriteResponse(stream, request.response.status, request.response.message, request.response.headers, request.response.Bytes);
                    stream.Flush();
                    stream.Dispose();
                } catch (HTTPException) {
                    Shutdown ();
//                    return false;
                } catch(ThreadAbortException) {
                    Shutdown ();
//                    return false;
                } catch (Exception e) {
                    Debug.LogError ("Exception in server thread: " + e.ToString ());
                    Shutdown ();
//                    return false;
                }
                if(logRequests) {
                    Debug.Log(string.Format("{0} {1} {2} \"{3}\" {4}", System.DateTime.Now.ToString("yyyy/mm/dd H:mm:ss zzz"), request.response.status, request.method.ToUpper(), request.uri, request.response.Bytes.Length));
                }


            }

        }


    }
}
