using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Demo GUI skin components
/// </summary>
public class DemoGuiSkin : MonoBehaviour {
	
	public List<Color> primaryColors;
	public List<Color> secondaryColors;
	public List <Texture2D> skillsTextures;

	private bool hasUpdatedGui = false;
	private int currentColor;
	
	private bool toggleValue;
	private float sliderValue;
	private string textValue ="Text field";
	private float scrollValue = 15.0f;
	
	private Rect dragWindowRect =  new Rect(10,10,300,300);
	private Rect portraitWindowRect =  new Rect(710,60,285,128);
	
	/// <summary>
	/// Unity GUI hook.
	/// </summary>
	void OnGUI() {
		if (!hasUpdatedGui) {
			ColoredGUISkin.Instance.UpdateGuiColors(primaryColors[0], secondaryColors[0]);
			hasUpdatedGui = true;
		}
		GUI.skin = ColoredGUISkin.Skin;
		
		// Window 
		dragWindowRect = GUI.Window (0, dragWindowRect, DoDragWindow, "Drag Window");
		
		// Alt Window 
		GUI.Window (1, new Rect(400,400,200,208), DoWindow, "Alternate Window", ColoredGUISkin.Skin.customStyles[4]);
		
		// Portrait 
		portraitWindowRect = GUI.Window (2, portraitWindowRect, DoPortraitWindow, "", ColoredGUISkin.Skin.customStyles[2]);
		
		// Switch Scene Button
		if (GUI.Button (new Rect (900,10,120,48), "To NGUI")) {
			Application.LoadLevel(0);	
		}
		
		// Scroll bar
		scrollValue = GUI.VerticalScrollbar(new Rect (900,300,24,240), scrollValue, 10, 40, 0);
		
		// Skills Box
		GUI.Window (3, new Rect(300,680,424,86), DoSkillsWindow, "", ColoredGUISkin.Skin.box);
	}
	
	void DoDragWindow (int windowID) {
		
		// Ornament
		GUI.DrawTexture(new Rect(20,4,31,40), ColoredGUISkin.Skin.customStyles[0].normal.background);
		
		GUILayout.Space(32);

		// Button
    	if (GUILayout.Button("Button")) {
       		Debug.Log("Clicked the button with text");
		}

		// Button
    	if (GUILayout.Button("Color")) {
       		currentColor++;
			ColoredGUISkin.Instance.UpdateGuiColors(primaryColors[currentColor % primaryColors.Count],secondaryColors[currentColor % secondaryColors.Count]);
		}
		
		// Text
		textValue = GUILayout.TextField(textValue, 50);

		GUI.DragWindow();
	}
	
	
	void DoWindow (int windowID) {
		
		GUI.DrawTexture(new Rect(20,30,160,8), ColoredGUISkin.Skin.customStyles[1].normal.background);
		
		GUILayout.Space(32);
		GUILayout.BeginHorizontal();
		GUILayout.FlexibleSpace();
		toggleValue = MyToggle(toggleValue);
		GUILayout.FlexibleSpace();
		GUILayout.EndHorizontal();
		
		GUILayout.BeginHorizontal();
		GUILayout.FlexibleSpace();
		GUILayout.Label("Toggle");
		GUILayout.FlexibleSpace();
		GUILayout.EndHorizontal();		
		GUILayout.Space(32);
		
		sliderValue = GUILayout.HorizontalSlider(sliderValue, 0, 1);
		
		GUILayout.BeginHorizontal();
		GUILayout.FlexibleSpace();
		GUILayout.Label("Slider");
		GUILayout.FlexibleSpace();
		GUILayout.EndHorizontal();
		
	}
	
	void DoPortraitWindow (int windowID) {
		
		GUI.DrawTexture(new Rect(0,0,285,128), ColoredGUISkin.Skin.customStyles[3].normal.background);
		GUI.Label (new Rect(120, 15, 200,32), "Portrait (Draggable)");
		GUI.DragWindow();
	}
	
	void DoSkillsWindow(int windowID) {
		GUILayout.BeginHorizontal();
		GUILayout.Box ("", GUILayout.Height(60));	
		Rect skillRect = GUILayoutUtility.GetLastRect();
		skillRect.height -= 19; skillRect.width -= 19; skillRect.x += 10; skillRect.y += 9;
		//GUI.DrawTexture(skillRect, skillsTextures[0]);
		GUILayout.Box ("", GUILayout.Height(60));
		skillRect = GUILayoutUtility.GetLastRect();
		skillRect.height -= 19; skillRect.width -= 19; skillRect.x += 10; skillRect.y += 9;
		//GUI.DrawTexture(skillRect, skillsTextures[1]);
		GUILayout.Box ("", GUILayout.Height(60));
		skillRect = GUILayoutUtility.GetLastRect();
		skillRect.height -= 19; skillRect.width -= 19; skillRect.x += 10; skillRect.y += 9;
		//GUI.DrawTexture(skillRect, skillsTextures[2]);
		GUILayout.Box ("", GUILayout.Height(60));
		GUILayout.Box ("", GUILayout.Height(60));
		GUILayout.EndHorizontal();
	}
	
	public bool MyToggle(bool variable) {
		
		// Different style toggle button, it looks like a button with a toggle on it
		GUILayout.Box ("", ColoredGUISkin.Skin.button, GUILayout.Width(ColoredGUISkin.Skin.toggle.fixedWidth + ColoredGUISkin.Skin.button.padding.left + ColoredGUISkin.Skin.button.padding.right),
											  GUILayout.Height(ColoredGUISkin.Skin.toggle.fixedHeight + ColoredGUISkin.Skin.button.padding.top + ColoredGUISkin.Skin.button.padding.bottom));
		Rect toggleRect = GUILayoutUtility.GetLastRect();
		toggleRect.x += ColoredGUISkin.Skin.button.padding.left;
		toggleRect.y += ColoredGUISkin.Skin.button.padding.top;
		return GUI.Toggle(toggleRect, variable, "");
	}
}
