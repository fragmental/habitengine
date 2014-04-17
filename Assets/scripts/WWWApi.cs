using UnityEngine;
using System;
using System.Collections;
using System.Reflection;

public class WWWApi : MonoBehaviour {
	//public string uid = "8ab6a17d-dfd4-428c-866d-92b68002927a";
	//public string key = "6b2c7136-c8db-4074-8576-91298e8d08ef";
	//public string api = "http://fragmental.no-ip.org:3000/api/v2/";
	public string uid = "b2f17791-3247-462b-8cfe-86e9f9bca28f";
	public string key = "45482a67-8c71-4595-bfa5-f19ddeca8d95";
	public string api = "https://beta.habitrpg.com/api/v2/user";
	public Hashtable apiJson;
	// Use this for initialization

/*Commented because broken	
 * void Start () {
	var form = new WWWForm();
	var headers = new Hashtable();
		headers.Add("x-api-key", key);
		headers.Add("x-api-user", uid);
	
	www = new WWW("http://localhost/getpostheaders", null, headers);
	yield return www;
	Debug.Log("2. " + www.text);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	*/

	IEnumerator Start()
	{
		Debug.Log ("Nothing is displaying");
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


