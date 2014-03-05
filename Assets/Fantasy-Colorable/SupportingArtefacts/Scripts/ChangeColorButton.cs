using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ChangeColorButton : MonoBehaviour {
	
	public List<Color> primaryColors;
	public List<Color> secondaryColors;

	public ChangeColor colorChanger;
	
	private int currentColor;
	
	void Start() {
		colorChanger.UpdatePrimaryColor(primaryColors[0]);
		colorChanger.UpdateSecondaryColor(secondaryColors[0]);
	}
	
	
	public void OnClick () {
		currentColor++;
		colorChanger.UpdatePrimaryColor(primaryColors[currentColor % primaryColors.Count]);
		colorChanger.UpdateSecondaryColor(secondaryColors[currentColor % secondaryColors.Count]);

	}
}
