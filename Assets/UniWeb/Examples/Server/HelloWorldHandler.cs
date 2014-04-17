using UnityEngine;
using System.Collections;

public class HelloWorldHandler : HTTP.Server.HttpRequestHandler {

    public override void GET (HTTP.Request request)
    {
        var response = request.response;
        response.status = 200;
        response.message = "OK";
        response.Text = "Hello, World!";
    }
	
}
