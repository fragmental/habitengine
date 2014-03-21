using UnityEngine;
using System.Collections;

public class EnterTest2 : MonoBehaviour {
	private string stringToEdit= "";
	void OnGUI() 
	{
		//utter bullshit that Unity doesn't make this easy.  This isn't going to work, because, I don't know of any way to specify which TextField is receiving the enter key.
		Event e = Event.current;
		
		//if (e.keyCode == KeyCode.Return) userHasHitReturn = true;
		
		//else if (false == userHasHitReturn)     stringToEdit = GUI.TextField(new Rect(0,0,100,50), stringToEdit, 25);
		stringToEdit = GUI.TextField(new Rect(0, 30, Screen.width * 24 / 100-20, 40), stringToEdit, 100);
		if (GUI.Button (new Rect (Screen.width * 24 / 100 - 35, 30 + 40, 40, 40), "+") || e.keyCode == KeyCode.Return ) 
		{
						Debug.Log ("Button Pressed");
		}
		if (GUI.Button (new Rect (Screen.width * 24 / 100 - 35, 30 + 80, 40, 40), "+") || e.keyCode == KeyCode.Return ) 
		{
			Debug.Log ("wrong button");
		}
	
	}
}
