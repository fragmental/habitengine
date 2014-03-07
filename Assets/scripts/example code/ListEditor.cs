/*using UnityEngine;

using UnityEditor;

using System.Collections.Generic;



public class ListEditor : EditorWindow {
	
	
	
	List<string> values;
	
	
	
	string editingValue;
	
	string lastFocusedControl;
	
	
	
	[MenuItem("Window/List Editor")]
	
	static void ShowEditor() {
		
		ListEditor editor = EditorWindow.GetWindow<ListEditor>();   
		
		editor.Init();
		
	}
	
	
	
	void Init() {
		
		values = new List<string>();    
		
	}
	
	
	
	void OnGUI() {
		
		EditorGUILayout.HelpBox("Simple dynamic list editor.\nPress Enter to apply field changes.", MessageType.Info);
		
		List<string> editedValues = new List<string>();
		
		string newValue;
		
		
		
		foreach (string val in values) {
			
			newValue = val;
			
			
			
			if (ShowField("field " + val, ref newValue)) {
				
				if (string.IsNullOrEmpty(newValue))
					
					continue;
				
				
				
				if (values.IndexOf(newValue) >= 0)
					
					newValue = val;
				
			}
			
			
			
			editedValues.Add(newValue);
			
		}
		
		
		
		newValue = "";
		
		
		
		if (ShowField("new field", ref newValue)) {
			
			if (!string.IsNullOrEmpty(newValue) && values.IndexOf(newValue) < 0)
				
				editedValues.Add(newValue);
			
		}
		
		
		
		values = editedValues;
		
	}
	
	
	
	bool ShowField(string name, ref string val) {
		
		GUI.SetNextControlName(name);
		
		
		
		if (GUI.GetNameOfFocusedControl() != name) {
			
			
			
			if (Event.current.type == EventType.Repaint && string.IsNullOrEmpty(val)) {
				
				GUIStyle style = new GUIStyle(GUI.skin.textField);
				
				style.normal.textColor = new Color(0.5f, 0.5f, 0.5f, 0.75f);
				
				EditorGUILayout.TextField("Enter a new item", style);
				
			}
			
			else
				
				EditorGUILayout.TextField(val);
			
			
			
			return false;
			
		}
		
		
		
		//Debug.Log("Focusing " + GUI.GetNameOfFocusedControl());   // Uncomment to show which control has focus.
		
		
		
		if (lastFocusedControl != name) {
			
			lastFocusedControl = name;
			
			editingValue = val;
			
		}
		
		
		
		bool applying = false;
		
		
		
		if (Event.current.isKey) {
			
			switch (Event.current.keyCode) {
				
			case KeyCode.Return:
				
			case KeyCode.KeypadEnter:
				
				val = editingValue;
				
				applying = true;
				
				Event.current.Use();    // Ignore event, otherwise there will be control name conflicts!
				
				break;
				
			}
			
		}
		
		
		
		editingValue = EditorGUILayout.TextField(editingValue);     
		
		return applying;
		
	}
	
}
*/