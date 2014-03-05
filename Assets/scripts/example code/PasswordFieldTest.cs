using UnityEngine;
using System.Collections;

public class PasswordFieldTest: MonoBehaviour {
	public string passwordToEdit = "My Password";
	void OnGUI() {
		passwordToEdit = GUI.PasswordField(new Rect(10, 10, 200, 20), passwordToEdit, "*"[0], 25);
	}
}