using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using HTTP;
using System.Linq;

namespace HTTP.SocketIO
{
    public class SocketIOConnection : MonoBehaviour
    {
        public delegate void EventHandler (SocketIOConnection socket,SocketIOMessage msg);

        public string url;
        public bool reconnect;
        public string sid;
        public float heartbeatTimeout;
        public float closingTimeout;
        public string[] transports;
        Dictionary<string, EventHandler> handlers = new Dictionary<string, EventHandler> ();
        WebSocket socket;
        int msgUid = 0;
    
        /// <summary>
        /// Gets a value indicating whether this <see cref="SocketIOConnection"/> is ready.
        /// </summary>
        /// <value>
        /// <c>true</c> if ready; otherwise, <c>false</c>.
        /// </value>
        public bool Ready {
            get {
                return socket != null;
            }
        }

        void OnApplicationQuit ()
        {
            socket.Close (WebSocket.CloseEventCode.CloseEventCodeGoingAway, "Bye.");
        }

        public void On (string eventName, EventHandler fn)
        {
            handlers [eventName] = fn;
        }
    
        /// <summary>
        /// Send a raw SocketIOMessage to the server.
        /// </summary>
        /// <param name='msg'>
        /// Message.
        /// </param>
        public int Send (SocketIOMessage msg)
        {
            msg.id = msgUid++;
            if (socket == null) {
                Debug.LogError ("Socket.IO is not initialised yet!");
                return -1;
            } else {
                socket.Send (msg.ToString ());
                return msg.id.Value;
            }
        }
    
        /// <summary>
        /// Send the specified payload as a JSON message to the server.
        /// </summary>
        /// <param name='payload'>
        /// Payload.
        /// </param>
        public int Send (object payload)
        {
            var m = new SocketIOMessage ();
            m.type = SocketIOMessage.FrameType.JSONMESSAGE;
            m.data = HTTP.JsonSerializer.Encode (payload);
            return Send (m);
        }
    
    
        /// <summary>
        /// Sends an event to the server.
        /// </summary>
        /// <param name='eventName'>
        /// Event name.
        /// </param>
        /// <param name='args'>
        /// Arguments.
        /// </param>
        public int Emit (string eventName, params object[] args)
        {
            var m = new SocketIOMessage ();
            m.type = SocketIOMessage.FrameType.EVENT;
            var payload = new Hashtable ();
            payload ["name"] = eventName;
            payload ["args"] = args;
            m.data = HTTP.JsonSerializer.Encode (payload);
            return Send (m);
        }
    
        void Start ()
        {
            Application.runInBackground = true;
            if (!url.EndsWith ("/")) {
                url = url + "/";
            }
            Dispatch ("connecting", null);
            StartCoroutine (EstablishConnection (
        delegate() {
                Dispatch ("connect", null);
            },
        delegate {
                Dispatch ("connect_failed", null);
            }));
        }

        void Reconnect ()
        {
            Dispatch ("reconnecting", null);
            StartCoroutine (EstablishConnection (
            delegate() {
                Dispatch ("reconnect", null);
            },
        delegate {
                Dispatch ("reconnect_failed", null);
            }));
        }

        IEnumerator EstablishConnection (System.Action success, System.Action failed)
        {
            var req = new HTTP.Request ("POST", url + "socket.io/1/");
            yield return req.Send ();
            if (req.exception == null) {
                if (req.response.status == 200) {   
                    var parts = (from i in req.response.Text.Split (':') select i.Trim ()).ToArray ();
                    sid = parts [0];
                    float.TryParse (parts [1], out heartbeatTimeout);
                    float.TryParse (parts [2], out closingTimeout);
                    transports = (from i in parts [3].Split (',') select i.Trim ().ToLower ()).ToArray ();
                }
                if (transports.Contains ("websocket")) {
                    socket = new WebSocket ();
                    StartCoroutine (socket.Dispatcher ());
                    socket.Connect (url + "socket.io/1/websocket/" + sid);
                    socket.OnTextMessageRecv += HandleSocketOnTextMessageRecv;
                    socket.OnDisconnect += delegate() {
                        Dispatch ("disconnect", null);
                    };
                    success ();
                } else {
                    failed ();
                    Debug.LogError ("Websocket is not supported with this server.");    
                }
            } else {
                failed ();
            }
        }

        void Dispatch (string eventName, SocketIOMessage msg)
        {
            EventHandler fn = null;
            if (handlers.TryGetValue (eventName, out fn)) {
                fn (this, msg);
            }
        }

        void HandleSocketOnTextMessageRecv (string message)
        {
        
            var msg = SocketIOMessage.FromString (message);
            msg.socket = this;
        
            switch (msg.type) {
            case SocketIOMessage.FrameType.DISCONNECT:
                StopCoroutine ("Hearbeat");
                Dispatch ("disconnect", null);
                if (reconnect) {
                    Reconnect ();
                }
                break;
            case SocketIOMessage.FrameType.CONNECT:
                if (msg.endPoint == null)
                    StartCoroutine ("Heartbeat");
                break;
            case SocketIOMessage.FrameType.HEARTBEAT:
            //TODO
                break;
            case SocketIOMessage.FrameType.MESSAGE:
                Dispatch ("message", msg);
                break;
            case SocketIOMessage.FrameType.JSONMESSAGE:
                Dispatch ("json_message", msg);
                break;
            case SocketIOMessage.FrameType.EVENT:
                Dispatch (msg.eventName, msg);
                break;
            case SocketIOMessage.FrameType.ACK:
            //TODO
                break;
            case SocketIOMessage.FrameType.ERROR:
                Dispatch ("error", msg);
                break;
            case SocketIOMessage.FrameType.NOOP:
                break;
            default: 
                break;
            }
        }
    
        IEnumerator Heartbeat ()
        {
            var beat = new SocketIOMessage ();
            beat.type = SocketIOMessage.FrameType.HEARTBEAT;
            var delay = new WaitForSeconds (heartbeatTimeout * 0.8f);
            while (socket.connected) {
                socket.Send (beat.ToString ());
                yield return delay;
            }
        }

    }
}
