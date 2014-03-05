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
        public int status { get; private set; }

        public string message { get; private set; }

        public float progress { get; private set; }

        public readonly Headers headers = new Headers ();

        public AssetBundleCreateRequest AssetBundleCreateRequest() {
            return AssetBundle.CreateFromMemory (Bytes); 
        }

        public string Text {
            get {
                return System.Text.Encoding.UTF8.GetString (Bytes);
            }
        }

        public byte[] Bytes {
            get {
                return bytes;
            }
        }
        #endregion
        #region constructors
        public Response (Request request)
        {

        }
        #endregion
        #region implementation
        string ReadLine (Stream stream)
        {
            var line = new List<byte> ();
            while (true) {
                int c = -1;
                try {
                    c = stream.ReadByte ();
                } catch (IOException) {
                    throw new HTTPException ("Terminated Stream");
                }
                if (c == -1) {
                    throw new HTTPException ("Unterminated Stream");
                }
                var b = (byte)c;
                if (b == EOL [1]) {
                    break;
                }
                line.Add (b);
            }
            var s = ASCIIEncoding.ASCII.GetString (line.ToArray ()).Trim ();
            return s;
        }

        string[] ReadKeyValue (Stream stream)
        {
            string line = ReadLine (stream);
            if (line == "") {
                return null;
            } else {
                var split = line.IndexOf (':');
                if (split == -1) {
                    return null;
                }
                var parts = new string[2];
                parts [0] = line.Substring (0, split).Trim ();
                parts [1] = line.Substring (split + 1).Trim ();
                return parts;
            }
            
        }

        void ReadChunks (Stream inputStream, Stream output)
        {
            byte[] buffer = new byte[8192];
            

            while (true) {
                // Collect Body
                var hexLength = ReadLine (inputStream);
                if (hexLength == "0") {
                    break;
                }
                var length = int.Parse (hexLength, NumberStyles.AllowHexSpecifier);
                progress = 0;
                var contentLength = length;
                while (length > 0) {
                    var count = inputStream.Read (buffer, 0, Mathf.Min (buffer.Length, length));
                    output.Write (buffer, 0, count);
                    progress = Mathf.Clamp01 (1 - ((float)length / (float)contentLength));
                    length -= count;
                }
                progress = 1;
                //forget the CRLF.
                inputStream.ReadByte ();
                inputStream.ReadByte ();
                    
            }
            CollectHeaders (inputStream); //Trailers
        }

        void ReadBody (Stream inputStream, Stream output)
        {
            // Read Body
            byte[] buffer = new byte[8192];
            int contentLength = 0;
            
            if (int.TryParse (headers.Get ("Content-Length"), out contentLength)) {
                if (contentLength > 0) {
                    var remaining = contentLength;
                    while (remaining > 0) {
                        var count = inputStream.Read (buffer, 0, buffer.Length);
                        if (count == 0) {
                            break;
                        }
                        remaining -= count;
                        output.Write (buffer, 0, count);
                        progress = Mathf.Clamp01 (1.0f - ((float)remaining / (float)contentLength));
                    }
                }
            } else {
                var count = inputStream.Read (buffer, 0, buffer.Length);
                while (count > 0) {
                    output.Write (buffer, 0, count);
                    count = inputStream.Read (buffer, 0, buffer.Length);
                }
                progress = 1;
            }
        }

        void CollectHeaders (Stream inputStream)
        {
            while (true) {
                // Collect Headers
                string[] parts = ReadKeyValue (inputStream);
                if (parts == null) {
                    break;
                }
                headers.Add (parts [0], parts [1]);
            }
        }

        public void ReadFromStream (Stream inputStream)
        {
            progress = 0;

            if (inputStream == null) {
                throw new HTTPException ("Cannot read from server, server probably dropped the connection.");
            }
            var top = ReadLine (inputStream).Split (' ');
            status = -1;
            int _status = -1;
            if (!(top.Length > 0 && int.TryParse (top [1], out _status))) {
                throw new HTTPException ("Bad Status Code, server probably dropped the connection.");
            }
            status = _status;
            message = string.Join (" ", top, 2, top.Length - 2);

            CollectHeaders (inputStream);

            if (status == 101) {
                progress = 1;
                return;
            }

            if (status == 204) {
                progress = 1;
                return;
            }

            var chunked = string.Compare (headers.Get ("Transfer-Encoding"), "chunked", true) == 0;

            if(!chunked && !headers.Contains("Content-Length")) {
                progress = 1;
                return;
            }

            using (var output = new MemoryStream ()) {
                if (chunked) {
                    ReadChunks (inputStream, output);
                } else {
                    ReadBody (inputStream, output);
                }

                ProcessReceivedBytes (output);
            }
        }

        void ProcessReceivedBytes (MemoryStream output)
        {
            #if USE_GZIP
            var zipped = string.Compare (headers.Get ("Content-Encoding"), "gzip", true) == 0;
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

