using UnityEngine;
using System.Collections;

using HTTP.SocketIO;

//See UniWebServers/Node/SocketIOServerExample.js for the equivalent server.

[RequireComponent(typeof(SocketIOConnection))]
public class SocketIOClientExample : MonoBehaviour {


	IEnumerator Start () {
        //get the socket io component.
        var io = GetComponent<SocketIOConnection>();

        //register a method to handle the "news" event.
        io.On("news", HandleNewsEvent);

        //wait for it to become ready to use.
        while(!io.Ready) {
            yield return null;
        }

        //send an event to the server.
        io.Emit("my other event", "boo!");


    }

    void HandleNewsEvent (SocketIOConnection socket, SocketIOMessage msg)
    {
        Debug.Log("Received Event: " + msg.eventName + "(" + msg.args + ")");
    }
}
