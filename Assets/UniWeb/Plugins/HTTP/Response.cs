#define USE_GZIP
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Globalization;

#if USE_GZIP
using Ionic.Zlib;
#endif
using UnityEngine;

namespace HTTP
{
    public class Response : BaseHTTP
    {
        #region public properties
        public int status { get; set; }

        public string message { get; set; }

        public float progress;

        public readonly Headers headers = new Headers ();


        public AssetBundleCreateRequest AssetBundleCreateRequest() {
            return AssetBundle.CreateFromMemory (Bytes); 
        }

        public string Text {
            get {
                return HTTPProtocol.enc.GetString (Bytes, 0, Bytes.Length);
            }
            set {
                Bytes = HTTPProtocol.enc.GetBytes(value);
            }
        }

        public byte[] Bytes {
            get {
                return bytes;
            }
            set {
                bytes = value;
            }
        }
        #endregion
        #region constructors
        public Response (Request request)
        {

        }
        #endregion
        #region implementation




        public void ReadFromStream (Stream inputStream)
        {
            progress = 0;

            if (inputStream == null) {
                throw new HTTPException ("Cannot read from server, server probably dropped the connection.");
            }
            var top = HTTPProtocol.ReadLine (inputStream).Split (' ');
            status = -1;
            int _status = -1;
            if (!(top.Length > 0 && int.TryParse (top [1], out _status))) {
                throw new HTTPException ("Bad Status Code, server probably dropped the connection.");
            }
            status = _status;
            message = string.Join (" ", top, 2, top.Length - 2);

            HTTPProtocol.CollectHeaders (inputStream, headers);

            if (status == 101) {
                progress = 1;
                return;
            }

            if (status == 204) {
                progress = 1;
                return;
            }

            var chunked = headers.Get ("Transfer-Encoding").ToLower() == "chunked";

            /*
            if(!chunked && !headers.Contains("Content-Length")) {
                progress = 1;
                return;
            }
            */

            using (var output = new MemoryStream ()) {
                if (chunked) {
                    HTTPProtocol.ReadChunks (inputStream, output, ref progress);
                    HTTPProtocol.CollectHeaders (inputStream, headers); //Trailers
                } else {
                    HTTPProtocol.ReadBody (inputStream, output, headers, false, ref progress);
                }

                ProcessReceivedBytes (output);
            }
        }

        void ProcessReceivedBytes (MemoryStream output)
        {
            #if USE_GZIP
            var zipped = headers.Get ("Content-Encoding").ToLower() == "gzip";
            lock (output) {
                if (zipped) {
                    bytes = new byte[0];
                    using (var gz = new GZipStream (output, CompressionMode.Decompress)) {
                        var buffer = new byte[1024];
                        var count = -1;
                        output.Seek (0, SeekOrigin.Begin);
                        while (count != 0) {
                            count = gz.Read (buffer, 0, buffer.Length);
                            var offset = bytes.Length;
                            Array.Resize<byte> (ref bytes, offset + count);
                            Array.Copy (buffer, 0, bytes, offset, count);
                        }
                    }
                } else {
                    bytes = output.ToArray ();
                }
            }
            #else                           
            lock (output) {
                bytes = output.ToArray ();

            }
            #endif
        }



        byte[] bytes;
        #endregion
    }
    
}

