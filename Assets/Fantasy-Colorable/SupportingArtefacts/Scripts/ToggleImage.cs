using UnityEngine;
using System.Collections;

/// <summary>
/// Attach this script to a UIButton to make it behave like a
/// toggle button which switches images (as opposed to the UICheckbox
/// that adds a new image on top of the existing image).
/// </summary>
public class ToggleImage : MonoBehaviour {
	
	/// <summary>
	/// The state the button starts in.
	/// </summary>
	public bool startingState = true;
	
	/// <summary>
	/// The name of the sprite to show when this button is true (on).
	/// </summary>
	public string trueSpriteName;
	
	/// <summary>
	/// The name of the sprite to show when this button is false(off).
	/// </summary>	
	public string falseSpriteName;
	
	/// <summary>
	/// The sprite to show/change.
	/// </summary> 
	public UISprite sprite;
	
	/// <summary>
	/// Unity start hook, set the true sprite.
	/// </summary>
	void Start() {
		State = startingState;
		if (State){
			sprite.spriteName = trueSpriteName;	
		} else {
			sprite.spriteName = falseSpriteName;
		}
	}
	
	/// <summary>
	/// Gets a value indicating whether this <see cref="ToggleImage"/> is enabled or not.
	/// </summary>
	public bool State {
		get; private set;	
	}
	
	/// <summary>
	/// Handle the click event by switching state.
	/// </summary>
	public void OnClick(){
		State = !State;
		if (State){
			sprite.spriteName = trueSpriteName;	
		} else {
			sprite.spriteName = falseSpriteName;
		}
	}
	
}
