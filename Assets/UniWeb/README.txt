UniWeb
------


UniWeb allows you to use a common HTTP api across Unity Web players, iOS
and desktop builds.


FAQ 1: CROSSDOMAIN.XML AND WEB PLAYER PROBLEMS?
----------------------------------------
NB: To use UniWeb in the Web Player, you must have a server running on
the host which supplies a crossdomain.xml file. See: http://bit.ly/h6QY0M



How to do a HTTP GET request.
-----------------------------

var request = new HTTP.Request("GET", url);
//set headers
request.headers.Set("Hello", "World");
yield return request.Send();
if(request.exception != null) 
    Debug.LogError(request.exception);
else {
    var response = request.response;
    //inspect response code
    Debug.Log(response.status);
    //inspect headers
    Debug.Log(response.headers.Get("Content-Type"));
    //Get the body as a byte array
    Debug.Log(response.bytes);
    //Or as a string
    Debug.Log(response.Text);
}


How to do a HTTP POST request.
------------------------------

A post request is much the same as the GET request, however you assign
a value to the request.bytes field, or the request.Text property.

var request = new HTTP.Request("POST", url);
request.Text = "Hello from UniWeb!";
request.Send();



How to post forms.
------------------

var w = new WWWForm();
w.AddField("hello", "world");
w.AddBinaryData("file", new byte[] { 65,65,65,65 });
var r = new HTTP.Request (url, w);
yield return r.Send();



Support
-------

Support is available from support@differentmethods.com.

