using UnityEngine;
using System.Collections;

public class FocusTest3 : MonoBehaviour {

		private string str = "A String!";
		void OnGUI() {
			str = GUILayout.TextField(str, 10);
			Debug.Log("id: " + GUIUtility.keyboardControl);
		}
	}
