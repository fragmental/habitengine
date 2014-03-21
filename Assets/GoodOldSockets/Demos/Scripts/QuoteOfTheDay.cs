using System;
using System.Text;
using System.Threading;
using LostPolygon.System.Net;
using LostPolygon.System.Net.Sockets;
using UnityEngine;
using Random = System.Random;

namespace LostPolygon.Examples {

    [ExecuteInEditMode]
    public partial class QuoteOfTheDay : MonoBehaviour
    {
        #region QOTD server
        private class QotdServer : IDisposable {
            // TcpListener instance, encapsulating 
            // typical socket server interactions
            private TcpListener TcpListener;
            // A delegate of a log method
            private Action<string> LogCallback;

            // QOTD server constructor
            public QotdServer(int port, Action<string> logCallback) {
                LogCallback = logCallback;

                // Create a listening server that accepts connections from
                // any addresses on a given port
                TcpListener = new TcpListener(IPAddress.Any, port);
                // Switch the listener to a started state
                TcpListener.Start();
                // Set the callback that'll be called when a client connects to the server
                TcpListener.BeginAcceptTcpClient(OnAcceptTcpClient, TcpListener);

                AddToLog("Listening started.");
            }

            // Free the resources
            public void Dispose() {
                if (TcpListener != null) {
                    TcpListener.Stop();
                    TcpListener = null;

                    AddToLog("Listening canceled.");
                }
            }

            // Callback that gets called when a new incoming client
            // connection is established
            private void OnAcceptTcpClient(IAsyncResult asyncResult) {
                // Retrieve the TcpListener instance from IAsyncResult
                TcpListener listener = (TcpListener) asyncResult.AsyncState;
                if (listener == null)
                    return;

                // Restart the connection accept procedure
                listener.BeginAcceptTcpClient(OnAcceptTcpClient, listener);

                try {
                    // Retrieve newly connected TcpClient from IAsyncResult
                    TcpClient tcpClient = listener.EndAcceptTcpClient(asyncResult);
                    AddToLog(string.Format("Client {0} connected.", tcpClient.Client.RemoteEndPoint));
                    // Send a quote to the client
                    SendQotdToClient(tcpClient);
                    AddToLog(string.Format("Quote of the day sent to {0}.", tcpClient.Client.RemoteEndPoint));
                    AddToLog(string.Format("Disconnecting client {0}.", tcpClient.Client.RemoteEndPoint));
                    // Close the client connection - we don't need it anymore
                    tcpClient.Close();
                } catch (SocketException ex) {
                    AddToLog(string.Format("<color=red>Error accepting TCP connection: {0}</color>", ex.Message));
                } catch (ObjectDisposedException) {
                    // The listener was Stop()'d, disposing the underlying socket and
                    // triggering the completion of the callback. We're already exiting,
                    // so just ignore this.
                } catch (Exception ex) {
                    // Some other error occured. this should not happen
                    Debug.LogException(ex);
                    AddToLog(string.Format("<color=red>An error occured: {0}</color>", ex.Message));
                }
            }

            // Sends a random quote to a given TcpClient
            private void SendQotdToClient(TcpClient client) {
                // Get a random quote
                string quote = Quotes[(new Random()).Next(0, Quotes.Length)];
                // Convert quote string to a byte array
                byte[] buffer = Encoding.UTF8.GetBytes(quote);
                // Send it to the client
                client.GetStream().Write(buffer, 0, buffer.Length);
            }

            // Adds a formatted entry to the log
            private void AddToLog(string text) {
                LogCallback(string.Format("<color=green>[server]</color> <color=black>{0}</color>", text));
            }
        }
        #endregion

        #region QOTD client
        private class QotdClient : IDisposable {
            private TcpClient TcpClient;
            // A Thread that reads data from the server
            private Thread ThreadRead;
            // A delegate of a log method
            private Action<string> LogCallback;

            // QOTD client constructor
            public QotdClient(string host, ushort port, Action<string> logCallback) {
                LogCallback = logCallback;
                // Creating a new TcpClient instance
                TcpClient = new TcpClient();

                AddToLog(string.Format("Start connection to {0}:{1}...", host, port));
                // Start the asyncronous connection procedure
                TcpClient.BeginConnect(host, port, OnConnect, TcpClient);
            }

            // Free the resources
            public void Dispose() {
                if (TcpClient != null) {
                    TcpClient.Close();
                    TcpClient = null;
                    AddToLog(string.Format("Connection closed."));
                }    
            }

            // Called when a connection to a server is established
            private void OnConnect(IAsyncResult asyncResult) {
                // Retrieving TcpClient from IAsyncResult
                TcpClient tcpClient = (TcpClient) asyncResult.AsyncState;
                if (!tcpClient.Connected)
                    return;

                try {
                    // Finish the connection procedure
                    tcpClient.EndConnect(asyncResult);
                    AddToLog(string.Format("Connection to {0} successfull.", tcpClient.Client.RemoteEndPoint));

                    // Start data read thread
                    ThreadRead = new Thread(() => ThreadReadProcedure(tcpClient));
                    ThreadRead.Start();
                } catch (SocketException ex) {
                    AddToLog(string.Format("<color=red>Error at TCP connection: {0}</color>", ex.Message));
                } catch (ObjectDisposedException) {
                    // The listener was Stop()'d, disposing the underlying socket and
                    // triggering the completion of the callback. We're already exiting,
                    // so just return.
                } catch (Exception ex) {
                    // Some other error occured. This should not happen
                    Debug.LogException(ex);
                    AddToLog(string.Format("<color=red>An error occured: {0}</color>", ex.Message));
                }
            }

            // Receives data until connection is interrupted
            private void ThreadReadProcedure(TcpClient tcpClient) {
                // A string that will contain the received data
                string data = string.Empty;
                // A temporary byte[] buffer for receiving data
                byte[] buffer = new byte[256];
                // Number of bytes received last time
                int receivedBytes;

                // Receive the data until the end
                while ((receivedBytes = tcpClient.Client.Receive(buffer)) != 0) {
                    // Add newly-received data to a string
                    data += Encoding.UTF8.GetString(buffer, 0, receivedBytes);
                }

                // No more data to read, display the quote
                AddToLog(string.Format("Quote Of The Day:\r\n<b><size=15>{0}</size></b>", data));
                AddToLog(string.Format("Disconnected from {0}.", tcpClient.Client.RemoteEndPoint));
            }

            // Adds a formatted entry to the log
            private void AddToLog(string text) {
                LogCallback(string.Format("<color=blue>[client]</color> <color=black>{0}</color>", text.Trim()));
            }
        }
        #endregion

        public GUISkin Skin;
        private QotdServer server;
        private QotdClient client;
        private ushort serverPort = 5555;
        private ushort clientPort = 5555;
        private string clientHost = "127.0.0.1";
        private string log = "";
        private Vector2 logPosition = Vector2.zero;

        private void OnGUI() {
            #region GUI stuff
            if (Skin != null)
                GUI.skin = Skin;

            float width = Mathf.Min(390f, Screen.width);
            float height = Mathf.Min(650f, Screen.height);
            float scaleFactor = SocketExamplesTools.UpdateScaleMobile(height, width);

            GUILayout.BeginArea(
                new Rect(
                    Screen.width / 2f / scaleFactor - width / 2f,
                    Screen.height / 2f / scaleFactor - height / 2f, 
                    width, 
                    height
                    ),
                "Quote Of The Day", "Window"
                );

            GUILayout.BeginVertical();

            GUILayout.Label(
                "<color=black>" +
                "This example demonstrates usage of TCP sockets and asynchronous socket operations " +
                "in order to implement a very simple server and client applications. " +
                "The server, when a client has connected, just responds with a text " +
                "message and immediately closes the connection. " +
                "The client just reads and displays that message." +
                "</color>"
                );

            GUILayout.Box("Server");

                GUILayout.Space(5f);
                GUILayout.BeginHorizontal();
                    GUILayout.Label("<color=black>Port: </color>", GUILayout.Width(30f), GUILayout.Height(30f));
                    ushort.TryParse(GUILayout.TextField(serverPort.ToString(), GUILayout.Width(50f), GUILayout.Height(30f)), out serverPort);
                    serverPort = serverPort < (ushort) 4096 ? (ushort) 4096 : serverPort;
            #endregion
                    string buttonText = server == null ? "Start server" : "Stop server";
                    // Server start/stop
                    if (GUILayout.Button(buttonText, GUILayout.Height(30f))) {
                        if (server == null) {
                            server = new QotdServer(serverPort, AddToLog);
                        } else {
                            server.Dispose();
                            server = null;
                        }
                    }
            #region GUI stuff
                GUILayout.EndHorizontal();
                GUILayout.Space(5f);

            GUILayout.Box("Client");

                GUILayout.Space(5f);
                GUILayout.BeginHorizontal();
                    GUILayout.Label("<color=black>Port: </color>", GUILayout.Width(30f), GUILayout.Height(30f));
                    ushort.TryParse(GUILayout.TextField(clientPort.ToString(), GUILayout.Width(50f), GUILayout.Height(30f)), out clientPort);
                    GUILayout.Label("<color=black>Host: </color>", GUILayout.Width(35f),  GUILayout.Height(30f));
                    clientHost = GUILayout.TextField(clientHost, GUILayout.Height(30f));
                GUILayout.EndHorizontal();
            #endregion
                // Client start
                if (GUILayout.Button("Connect", GUILayout.Height(30f))) {
                    if (client != null) {
                        client.Dispose();
                        client = null;
                    }

                    client = new QotdClient(clientHost, clientPort, AddToLog);
                }
            #region GUI stuff
                GUILayout.Space(5f);

            GUILayout.Box("Log");
            SocketExamplesTools.TouchScroll(ref logPosition);
            logPosition = GUILayout.BeginScrollView(logPosition);
            GUILayout.Label(log);
            GUILayout.EndScrollView();

            GUILayout.EndVertical();
            GUILayout.EndArea();
            #endregion
        }

        private void AddToLog(string text) {
            log = text + "\r\n" + log;
            logPosition.y = 0f;
        }
    }


}
