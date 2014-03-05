using UnityEngine;
using System.Collections;

public class UniWebExamples : MonoBehaviour
{

    void Start ()
    {
        StartCoroutine (FetchAssetBundle());
        StartCoroutine (FetchImage ());
        StartCoroutine (PostForm ());
        StartCoroutine (TimeoutExample ());
        WebSocketExample ();
    }


    void OnGUI ()
    {
        if (GUILayout.Button ("Restart")) {
            Start ();
        }

    }

    IEnumerator FetchAssetBundle() {
        var r = new HTTP.Request("GET", "http://differentmethods.com/~simon/uniwebtest.unity3d");
        yield return r.Send();
        if(r.exception == null) {
            Debug.Log(r.response.status);
            var abcr = r.response.AssetBundleCreateRequest();
            yield return abcr;
            Debug.Log(abcr.assetBundle);
        } else {
            Debug.LogError(r.exception);
        }
    }

    void WebSocketExample ()
    {
        var ws = new HTTP.WebSocket ();
        StartCoroutine (ws.Dispatcher ());

        ws.Connect ("http://echo.websocket.org");
        
        ws.OnTextMessageRecv += (e) => {
            Debug.Log ("Reply came from server -> " + e);
        };

        ws.Send ("Hello");
        ws.Send ("Hello again!");
        ws.Send ("Goodbye");
    }


    IEnumerator PostForm ()
    {
        var form = new WWWForm ();
        form.AddField ("hello", "world");
        form.AddBinaryData ("file", new byte[] { 65,65,65,65 });
        var r = new HTTP.Request ("http://google.com/", form);
        yield return r.Send ();
        if (r.exception != null) {
            Debug.Log (r.exception);
        } else {
            Debug.Log ("Response Text:");
            Debug.Log (r.response.Text);
        }
        

    }

    IEnumerator FetchImage ()
    {

        var url = "http://www.differentmethods.com/wp-content/uploads/2011/05/react.jpg";
        var r = new HTTP.Request ("GET", url);
        yield return r.Send ();

        if (r.exception == null) {
            Debug.Log (r.response.status);
            var tex = new Texture2D (512, 512);
            tex.LoadImage (r.response.Bytes);
            renderer.material.SetTexture ("_MainTex", tex);
        } else {
            Debug.Log(r.exception);
        }
    }

    IEnumerator TimeoutExample ()
    {
        
        float initialTime = Time.realtimeSinceStartup;
        
        var r = new HTTP.Request ("GET", "http://krogh.no/wp-content/uploads/2011/09/earth-huge.png");
        r.acceptGzip = false;
        r.useCache = false;
        r.timeout = 1;
        yield return r.Send ();

        Debug.Log ("Time: " + (Time.realtimeSinceStartup - initialTime) + "s");

        if (r.exception != null) {
            if (r.exception is System.TimeoutException) {
                Debug.Log ("Request timed out.");
            } else {
                Debug.Log ("Exception occured in request.");
            }

        } else {
            Debug.Log ("Result received within timeout.");

        }
        

            

        
    }
}
