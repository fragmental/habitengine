using UnityEngine;
using System.Collections;

public class EnterTest : MonoBehaviour 
{

	bool userHasHitReturn = false;
		
	string stringToEdit = "";
	/*Another example
	  if (if (Event.current.type == EventType.KeyDown && Event.current.character == '\n')


or

if (Event.current.type == EventType.KeyDown && (Event.current.keyCode == KeyCode.Enter || Event.current.keyCode == KeyCode.Return))	
		*/
		
	void OnGUI() 
	{
			
		Event e = Event.current;
		
		if (e.keyCode == KeyCode.Return) userHasHitReturn = true;
		
		else if (false == userHasHitReturn)     stringToEdit = GUI.TextField(new Rect(0,0,100,50), stringToEdit, 25);
		
	}
		

}
