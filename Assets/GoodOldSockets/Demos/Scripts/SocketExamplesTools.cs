using System;
using System.Runtime.InteropServices;
using UnityEngine;
using LostPolygon.System.Net;
using LostPolygon.System.Net.Sockets;

namespace LostPolygon.Examples {
    public static class SocketExamplesTools {
        public static bool IsRuntimePlatformMobile() {
            return Application.platform == RuntimePlatform.Android
                   || Application.platform == RuntimePlatform.IPhonePlayer
               #if !UNITY_4_0 && !UNITY_4_1
            || Application.platform == RuntimePlatform.WP8Player 
            || Application.platform == RuntimePlatform.BB10Player
               #endif
                ;
        }

        public static float UpdateScaleMobile(float baseHeight, float baseWidth) {
            if (!IsRuntimePlatformMobile())
                return 1f;

            float scaleFactor = Mathf.Max(Mathf.Min(Screen.width / baseWidth, Screen.height / baseHeight) * 0.94f, 1f);

            Vector3 scale;
            scale.x = scaleFactor;
            scale.y = scaleFactor;
            scale.z = 1f;

            GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, scale);

            return scaleFactor;
        }

        public static void TouchScroll(ref Vector2 scrollPosition) {
            if (Input.touchCount > 0) {
                Touch touch = Input.touches[0];
                if (touch.phase == TouchPhase.Moved) {
                    scrollPosition.y += touch.deltaPosition.y; 
                    scrollPosition.y = Mathf.Max(0f, scrollPosition.y);
                }
            }
        }

        public static IPAddress GetHostIpAddress(string host) {
            IPAddress[] addressList = Dns.GetHostAddresses(host);
            foreach (IPAddress address in addressList) {
                if (address.AddressFamily == AddressFamily.InterNetwork)
                    return address;
            }

            return null;
        }

        public static class StructTools
        {
            /// <summary>
            /// converts byte[] to struct
            /// </summary>
            public static T RawDeserialize<T>(byte[] rawData, int position)
            {
                int rawsize = Marshal.SizeOf(typeof(T));
                if (rawsize > rawData.Length - position)
                    throw new ArgumentException("Not enough data to fill struct. Array length from position: "+(rawData.Length-position) + ", Struct length: "+rawsize);
                IntPtr buffer = Marshal.AllocHGlobal(rawsize);
                Marshal.Copy(rawData, position, buffer, rawsize);
                T retobj = (T)Marshal.PtrToStructure(buffer, typeof(T));
                Marshal.FreeHGlobal(buffer);
                return retobj;
            }
        
            /// <summary>
            /// converts a struct to byte[]
            /// </summary>
            public static byte[] RawSerialize(object anything)
            {
                int rawSize = Marshal.SizeOf(anything.GetType());
                IntPtr buffer = Marshal.AllocHGlobal(rawSize);
                Marshal.StructureToPtr(anything, buffer, false);
                byte[] rawDatas = new byte[rawSize];
                Marshal.Copy(buffer, rawDatas, 0, rawSize);
                Marshal.FreeHGlobal(buffer);
                return rawDatas;
            }
        }
    }
}
