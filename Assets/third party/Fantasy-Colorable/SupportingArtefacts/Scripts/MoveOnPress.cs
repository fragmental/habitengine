using UnityEngine;
using System.Collections;

/// <summary>
/// Attach this script to a UIButton to enhance the "press" effect by 
/// transforming any gameObject in the specified direction.
/// </summary>
/// 
public class MoveOnPress : MonoBehaviour {
	
	//// <summary>
	/// The components to transform.
	/// </summary>
	public Transform[] components;
	
	/// <summary>
	/// The transform amount. How much the components move on press.
	/// </summary>
	public Vector2 transformAmount;
	
	/// <summary>
	/// Handle the press event.
	/// </summary>
	public void OnPress(bool press) {
		if (press) {
			foreach (Transform t in components){
				t.Translate(transformAmount, Space.Self);
			}
		} else {
			foreach (Transform t in components){
				t.Translate(transformAmount * -1, Space.World);
			}		
		}
	}
}
