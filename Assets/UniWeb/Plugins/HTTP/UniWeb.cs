using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;


namespace HTTP {
	public class UniWeb : MonoBehaviour {
		static UniWeb _instance = null;
		static public UniWeb Instance {
			get {
				if(_instance == null) {
					_instance = new GameObject("SimpleWWW", typeof(UniWeb)).GetComponent<UniWeb>();
					_instance.gameObject.hideFlags = HideFlags.HideAndDontSave;
				}
				return _instance;
			}
		}

		void Awake()
		{
			/*
			 * ***************
			 * TRC EDIT
			 * ***************
			 *  Remark: To prevent from adding a new instance if there is already one.
			 *	Author: Stephen Lautier
			 */
			if (_instance != null)
			{
				Destroy(gameObject);
				return;
			}
			// END TRC EDIT
		}
		
		public void Send(Request request, System.Action<HTTP.Request> requestDelegate) {
			StartCoroutine(_Send(request, requestDelegate));
		}
		
		public void Send(Request request, System.Action<HTTP.Response> responseDelegate) {
			StartCoroutine(_Send(request, responseDelegate));
		}
		
		IEnumerator _Send(Request request, System.Action<HTTP.Response> responseDelegate) {
			request.Send();
			while(!request.isDone)
				yield return new WaitForEndOfFrame();
			if(request.exception != null) {
				Debug.LogError(request.exception);	
			} else {
				responseDelegate(request.response);
			}
		}
		
		IEnumerator _Send(Request request, System.Action<HTTP.Request> requestDelegate) {
			request.Send();
			while(!request.isDone)
				yield return new WaitForEndOfFrame();
			requestDelegate(request);
		}
		
		List<System.Action> onQuit = new List<System.Action>();
		public void OnQuit(System.Action fn) {
			onQuit.Add(fn);	
		}
		void OnApplicationQuit() {
			/*
			 * ***************
			 * TRC EDIT
			 * ***************
			 *  Remark: Destroy permanently the component if there are multiple instances already, and its not the main singleton instance. 
			 *	Author: Stephen Lautier
			 */
			if (this != _instance)
			{
				DestroyImmediate(this.gameObject);
				return;
			}
			// END TRC EDIT
			foreach(var fn in onQuit) {
				try {
					fn();
				} catch(System.Exception e) {
					Debug.LogError(e);	
				}
			}
			
			/*
			 * ***************
			 * TRC EDIT
			 * ***************
			 *  Remark: Destroy permanently the component since on next launch it will be created again. 
			 *	Author: Stephen Lautier
			 */
			//Request.Shutdown(); // this is causing problems if there are still WWW requests on quitting.
			DestroyImmediate(this.gameObject);
			_instance = null;
			// END TRC EDIT
		}
	}
}