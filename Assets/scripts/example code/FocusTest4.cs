using UnityEngine;
using System.Collections;

public class FocusTest4 : MonoBehaviour {

	public string login = "username";
	public string login2 = "no action here";
	void OnGUI() {
		GUI.SetNextControlName ("user");
		login = GUI.TextField (new Rect (10, 10, 130, 20), login);
		GUI.SetNextControlName ("pass");
		login2 = GUI.TextField (new Rect (10, 40, 130, 20), login2);
		if (Event.current.isKey && Event.current.keyCode == KeyCode.Return && GUI.GetNameOfFocusedControl () == "user")
			Debug.Log (GUI.GetNameOfFocusedControl());

		if (GUI.Button (new Rect (150, 10, 50, 20), "Login"))
			Debug.Log (GUI.GetNameOfFocusedControl());
	}
}
