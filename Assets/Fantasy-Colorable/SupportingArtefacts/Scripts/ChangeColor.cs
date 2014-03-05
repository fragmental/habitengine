using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Change colour of multiple GUi components.
/// </summary>
public class ChangeColor : MonoBehaviour {

	public List <UIWidget> primaryColored;
	public List <UIWidget> secondaryColored;
	
	public List <UIWidget> primaryFont;
	public List <UIWidget> secondaryFont;
	
	public void UpdatePrimaryColor(Color color) {
		UpdateColor(primaryColored, color);
	}
	
	public void UpdateSecondaryColor(Color color) {
		UpdateColor(secondaryColored, color);
	}
	
	public void UpdatePrimaryFontColor(Color color) {
		UpdateColor(primaryFont, color);
	}
	
	public void UpdateSecondaryFontColor(Color color) {
		UpdateColor(secondaryFont, color);
	}
	
	public void UpdateColor(List <UIWidget> widgets, Color color) {
		foreach (UIWidget w in widgets) {
			w.color = color;
		}
	}
	
}
