using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ScrollExample : MonoBehaviour {
    public float test1xpos = 10;
    public float test1ypos = 300;
    public float test1xsize = 100;
    public float test1ysize = 100;
    public float test2xpos = 0;
    public float test2ypos = 0;
    public float test2xsize = 220;
    public float test2ysize = 200;

	public List<Color> primaryColors;
	public List<Color> secondaryColors;
	private bool hasUpdatedGui = false;

    public Vector2 scrollPosition = Vector2.zero;
    void OnGUI() {
		if (!hasUpdatedGui) {
			ColoredGUISkin.Instance.UpdateGuiColors(primaryColors[0], secondaryColors[0]);
			hasUpdatedGui = true;
		}
		
		GUI.skin = ColoredGUISkin.Skin;

        scrollPosition = GUI.BeginScrollView(new Rect(test1xpos, test1ypos, test1xsize, test1ysize), scrollPosition, new Rect(test2xpos, test2ypos, test2xsize, test2ysize));
        GUI.Button(new Rect(0, 0, 100, 20), "Top-left");
        GUI.Button(new Rect(120, 0, 100, 20), "Top-right");
        GUI.Button(new Rect(0, 180, 100, 20), "Bottom-left");
        GUI.Button(new Rect(120, 180, 100, 20), "Bottom-right");
        GUI.EndScrollView();
    }
}
