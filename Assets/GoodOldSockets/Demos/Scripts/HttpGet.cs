using System;
using System.Text;
using LostPolygon.System.Net;
using LostPolygon.System.Net.Sockets;
using System.Threading;
using UnityEngine;

namespace LostPolygon.Examples {

    [ExecuteInEditMode]
    public class HttpGet : MonoBehaviour {
        // Returns a connected socket on success
        // or null on failure
        private Socket ConnectSocket(string server, int port) {
            Socket s = null;
            IPHostEntry hostEntry = null;

            // Get host related information.
            hostEntry = Dns.GetHostEntry(server);

            // Loop through the AddressList to obtain the supported AddressFamily. This is to avoid
            // an exception that occurs when the host IP Address is not compatible with the address family
            // (typical in the IPv6 case).
            foreach (IPAddress address in hostEntry.AddressList) {
                IPEndPoint ipEndPoint = new IPEndPoint(address, port);
                // Trying to create a socket and connect
                Socket tempSocket =
                    new Socket(ipEndPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

                tempSocket.Connect(ipEndPoint);

                if (tempSocket.Connected) {
                    s = tempSocket;
                    break;
                }
            }
            return s;
        }

        // Requests the page content for the specified server and port.
        private string SocketSendReceive(string server, int port, string page) {
            // Constructing a HTTP GET request
            string request = string.Format("GET {0} HTTP/1.1\r\nHost: " + server +
                                           "\r\nConnection: Close\r\n\r\n", page);

            // Creating send/receive buffers
            Byte[] bytesSent = Encoding.ASCII.GetBytes(request);
            Byte[] bytesReceived = new Byte[256];

            // Create a socket connection with the specified server and port.
            Socket s = ConnectSocket(server, port);

            if (s == null)
                return ("Connection failed");

            // Send request to the server.
            s.Send(bytesSent, bytesSent.Length, 0);

            // Receive the server home page content.
            int bytes;
            string content = string.Empty;

            // The following will block until te page is transmitted.
            do {
                bytes = s.Receive(bytesReceived, bytesReceived.Length, 0);
                content = content + Encoding.UTF8.GetString(bytesReceived, 0, bytes);
            } while (bytes > 0);

            return content;
        }

        // The current state of the page retrieval thread
        enum HttpGetResultType {
            InProgress,
            FinishedSuccess,
            FinishedError
        }

        // The main page retrieval procedure
        private void ThreadGet(string server, int port, string page, Action<HttpGetResultType, string> callback) {
            callback(HttpGetResultType.InProgress, string.Empty);
            try {
                // Try to receive the page and return it
                string result = SocketSendReceive(server, port, page);
                callback(HttpGetResultType.FinishedSuccess, result);
            } catch (Exception e) {
                // Returning the exception message
                callback(HttpGetResultType.FinishedError, e.Message);
            }
        }

        // This is triggered by ThreadGet() on events
        private void OnThreadFinishedCallback(HttpGetResultType resultType, string content) {
            switch (resultType) {
                case HttpGetResultType.InProgress:
                    threadState = "<color=black>retrieving page...</color>";
                    break;
                case HttpGetResultType.FinishedSuccess:
                    threadState = "<color=black>page received succesfully.</color>";
                    pageContent = string.Format("<color=#333333>{0}</color>", content);
                    break;
                case HttpGetResultType.FinishedError:
                    threadState = @"<color=black>an error occured while retrieving the page.</color>";
                    pageContent = string.Format("<color=red>{0}</color>", content);
                    break;
            }
        }

        private void RequestPage(string uri, Action<HttpGetResultType, string> callback) {
            // Parse the URL
            Uri link = new Uri("http://" + uri);

            // Abort the current retrieval thread, if any
            if (currentGetThread != null && currentGetThread.IsAlive)
                currentGetThread.Abort();

            // Start a page retrieval thread
            currentGetThread = new Thread(() => ThreadGet(link.Host, link.Port, link.PathAndQuery, callback));
            currentGetThread.IsBackground = true;
            currentGetThread.Start();
        }

        public GUISkin Skin;
        private string address = "unity3d.com";
        private string pageContent = string.Empty;
        private string threadState = "<color=black>none.</color>";

        private Vector2 logPosition = Vector2.zero;
        private Thread currentGetThread; // The page retrieval thread

        // Some GUI stuff
        private void OnGUI()
        {
            #region GUI stuff
            GUI.skin = Skin;
            float width = Mathf.Min(350f, Screen.width);
            float height = Mathf.Min(550f, Screen.height);
            float scaleFactor = SocketExamplesTools.UpdateScaleMobile(height, width);

            GUILayout.BeginArea(
                new Rect(
                    Screen.width / 2f / scaleFactor - width / 2f,
                    Screen.height / 2f / scaleFactor - height / 2f, 
                    width, 
                    height
                    ),
                "HTTP GET example", "Window"
                );

            GUILayout.BeginVertical();

            GUILayout.Label(
                "<color=black>" +
                "This example demonstrates usage of TCP sockets and threads " +
                "in order to request and receive a page over HTTP protocol (commonly used in Web). " +
                "</color>"
                );

            GUILayout.Space(5f);
            GUILayout.BeginHorizontal();
            GUILayout.Label("<color=black>http://</color>", GUILayout.Width(35f), GUILayout.Height(29f));
            address = GUILayout.TextField(address);
            GUILayout.EndHorizontal();
            GUILayout.Space(5f);

            #endregion
            if (GUILayout.Button("Request page", GUILayout.Height(30f))) {
                pageContent = string.Empty;
                // Request a page
                RequestPage(address, OnThreadFinishedCallback);
            }
            #region GUI stuff
            GUILayout.Space(5f);

            GUILayout.Label("<color=black><b>Current state:</b></color> " + threadState);
            SocketExamplesTools.TouchScroll(ref logPosition);
            logPosition = GUILayout.BeginScrollView(logPosition);
            GUILayout.Label(pageContent);
            GUILayout.EndScrollView();
            GUILayout.EndVertical();

            GUILayout.EndArea();
            #endregion
        }
    }
}