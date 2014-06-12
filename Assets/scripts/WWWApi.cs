using UnityEngine;
using System;
using System.Collections;
using System.Reflection;

public class WWWApi : MonoBehaviour {
 	//private string uid = "8ab6a17d-dfd4-428c-866d-92b68002927a";
//	private string key = "6b2c7136-c8db-4074-8576-91298e8d08ef";
//	private string api = "http://fragmental.no-ip.org:3000/api/v2/";
	private string uid = "b2f17791-3247-462b-8cfe-86e9f9bca28f";
	private string key = "45482a67-8c71-4595-bfa5-f19ddeca8d95";
	private string api = "https://beta.habitrpg.com/api/v2/";
	public Hashtable apiJson;


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
		Debug.Log (api + "user");
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


