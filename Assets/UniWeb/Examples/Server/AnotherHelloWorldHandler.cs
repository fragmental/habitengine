using UnityEngine;
using System.Collections;

public class AnotherHelloWorldHandler : HTTP.Server.HttpRequestHandler {

    public override void GET (HTTP.Request request)
    {
        var response = request.response;
        var x = new Hashtable();
        x["boo"] = "ya!";

        response.Text = HTTP.JsonSerializer.Encode(x);
        response.headers.Set("Content-Type", "application/json");

    }
	
}
