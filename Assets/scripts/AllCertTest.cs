using UnityEngine;
using System.Collections;
using HTTP;
using System.Net;

public class AllCertTest : MonoBehaviour {

	//This is a test to see if I can bypass the ssl certification error.

	private string url = "http://beta.habitrpg.com/api/v2";
	private string cUrl = "https://beta.habitrpg.com/api/v2/status";
	//private string cUrl = "http://fragmental.no-ip.org:3000/api/v2/status";
	void Start () {
	//ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(AcceptAllCertifications);
	StartCoroutine (CheckServer());
	}

	public bool AcceptAllCertifications(object sender, System.Security.Cryptography.X509Certificates.X509Certificate certification, System.Security.Cryptography.X509Certificates.X509Chain chain, System.Net.Security.SslPolicyErrors sslPolicyErrors)
	{
		return true;
	}

	public IEnumerator CheckServer()
	{
		var request = new HTTP.Request("POST", cUrl);
		
		
		//request.headers.Set("Content-Type", "application/json");
		request.Send();
		while (!request.isDone) yield return new WaitForEndOfFrame();
		
		
		if (request.exception != null)
		{
			Debug.LogError(request.exception);
			//error = true;
			
		}
		else
		{
			var response = request.response;
			//inspect response code
			Debug.Log(response.status);
			//inspect headers
			Debug.Log(response.headers.Get("Content-Type"));
			//Get the body as a byte array
			//Debug.Log(response.bytes);
			//Or as a string
			//Debug.Log(response.Text);
			//string authResponse = response.Text;
			//Type t = authResponse.GetType();
			//			Hashtable authResponse = JsonSerializer.Decode(response.Text) as Hashtable;
			//Debug.Log("Type is " + t.FullName);
			//Debug.Log(authObject["id"]);
			
			if (response.status == 200)
			{
				Debug.Log("Server says it's up");
				//serverUp = true;				
			}
			else
			{
				Debug.Log("Server says it's down");
				//serverUp = false;
			}
		}
	}
	// Update is called once per frame
	void Update () {
	
	}
}
