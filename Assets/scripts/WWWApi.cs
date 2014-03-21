/*using UnityEngine;
using System;
using System.Collections;
using System.Reflection;

public class WWWApi : MonoBehaviour {
	public string uid = "f78c4fdf-f404-4922-8d83-cbcf941119c4";
	public string key = "345a363b-6092-422f-9dfb-9caf3373c4e0";
	public string api = "http://fragmental.no-ip.org:3000/api/v2/";
	public Hashtable apiJson;
	// Use this for initialization

	/*void Start () {
	var form = new WWWForm();
	var headers = new Hashtable();
		headers.Add("x-api-key", "b2f17791-3247-462b-8cfe-86e9f9bca28f");
		headers.Add(""x-api-user", uid);
	
	www = new WWW("http://localhost/getpostheaders", null, headers);
	yield return www;
	Debug.Log("2. " + www.text);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	*/
/*
	IEnumerator Start()
	{
		var form = new WWWForm();
		var headers = new Hashtable();
		headers.Add("x-api-key", key);
		headers.Add("x-api-user", uid);
		//status
		//WWW www = new WWW(api + "status");

		//full user object
		WWW www = new WWW(api + "user", null, headers);

		yield return www;
		Debug.Log("response = " + www.text);
		//Hashtable apiJson = www.text as Hashtable;
		Type t = www.GetType();
		//Type t2 = nextLevel.GetType();

		Debug.Log("type is " + t.FullName);
		
		//foreach 
		
	}
	
}
*/

