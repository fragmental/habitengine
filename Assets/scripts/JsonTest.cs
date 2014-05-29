using UnityEngine;
using System.Collections;
using SimpleJSON;

public class JsonTest : MonoBehaviour {
	private HabitDatav1 userData;
	public JSONArray dailies;

	// Use this for initialization
	void Start () {
		userData = new HabitDatav1(PlayerPrefs.GetString("jsonSave"));
		JSONArray dailies = userData.Dailies.AsArray;
		//StartCoroutine (Json());
		//var duck = dailies.ToString;
		Debug.Log ("first completed object is = " + dailies ["tasks"] ["dailys"] ["checklist"] [0] ["completed"].AsBool);
		//it's false.  How does it know which daily?  Does it just pick the first one?
	}
	
	// Update is called once per frame
	//IEnumerator Json () {
		//var checklistTest = dailies ["tasks"] ["dailys"] ["checklist"] [0] ["completed"].AsBool;
		//Debug.Log ("completed = " + ChecklistTest);
		//how do you pick which ones?
	
	//}
}
