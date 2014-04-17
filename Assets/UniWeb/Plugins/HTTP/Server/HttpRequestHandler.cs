using System;
using UnityEngine;

namespace HTTP.Server
{
    public class HttpRequestHandler : MonoBehaviour
    {
        public string path = "/";

        public virtual void GET(Request request) {
        }

        public virtual void PUT(Request request) {
        }

        public virtual void POST(Request request) {
        }

        public virtual void DELETE(Request request) {
        }

        public void Dispatch(Request request) {


            //default values.
            request.response.status = 200;
            request.response.message = "OK";
            var method = request.method.ToUpper();

            if(method == "GET") GET (request);
            if(method == "PUT") PUT (request);
            if(method == "POST") POST (request);
            if(method == "DELETE") DELETE (request);

           
        }

    }
}

