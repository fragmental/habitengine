using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.IO;
using System;



public class GuiTest: MonoBehaviour {
	/*from index.coffee in habitrpg-shared:

  if value < -20
    classes += ' color-worst'
  else if value < -10
    classes += ' color-worse'
  else if value < -1
    classes += ' color-bad'
  else if value < 1
    classes += ' color-neutral'
  else if value < 5
    classes += ' color-good'
  else if value < 10
    classes += ' color-better'
  else
    classes += ' color-best'
  return classes

The 'value' is literally just the value of the task as shown in the api, or export, or graph view on the task.
*/

/*IEnumerator taskColor()
{
if (taskType=habit)
{
	if (habitDelta(i)< -20)
	{
	GUI.backgroundColor = colorWorst;
	}
	else if (habitDelta(i)< -10)
	{
	GUI.backgroundColor = colorWorse;
	}
	else if (habitDelta(i)< -1)
	{
	GUI.backgroundColor = colorBad;
	}
	else if (habitDelta(i)< 1)
	{
	GUI.backgroundColor = colorNeutral;
	}
	else if (habitDelta(i)< 5)
	{
	GUI.backgroundColor = colorGood;
	}
	else if (habitDelta(i)< 10)
	{
	GUI.backgroundColor = colorBetter;
	}
	else
	GUI.backgroundColor = colorBest;
	*/
	private Color colorWorst = new Color(229/255F, 183/255F, 174/255F, 1);
	private Color colorWorse = new Color(243/255F, 203/255F, 203/255F, 1);
	private Color colorBad = new Color(251/255F, 228/255F, 204/255F, 1);
	private Color colorNeutral = new Color(254/255F, 241/255F, 203/255F, 1);
	private Color colorGood = new Color(216/255F, 233/255F, 210/255F, 1);
	private Color colorBetter = new Color(207/255F, 223/255F, 226/255F, 1);
	private Color colorBest = new Color(200/255F, 217/255F, 247/255F, 1);

	private Color colorWorst2 = new Color(200/255F, 102/255F, 82/255F, 1);
	private Color colorWorse2 = new Color(219/255F, 93/255F, 93/255F, 1);
	private Color colorBad2 = new Color(243/255F, 161/255F, 76/255F, 1);
	private Color colorNeutral2 = new Color(254/255F, 206/255F, 66/255F, 1);
	private Color colorGood2 = new Color(138/255F, 190/255F, 120/255F, 1);
	private Color colorBetter2 = new Color(125/255F, 169/255F, 177/255F, 1);
	private Color colorBest2 = new Color(82/255F, 135/255F, 232/255F, 1);



	public List<Color> primaryColors;
	public List<Color> secondaryColors;
	public List <Texture2D> skillsTextures;
	private List<string> testButton = new List<string>();
	private bool hasUpdatedGui = false;
	public GUISkin skinny;
    //private string roaming = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
	private int i = 0;
	private string test = "";
	public float buttonX=120;
	public float buttonY=48;
    void Start()
    {
    /*    Debug.Log("special folder is "+ roaming);
        string saveFolder = roaming + @"\.h3d\";
        Debug.Log(saveFolder);
        if (!Directory.Exists(saveFolder))
        {
            Directory.CreateDirectory(saveFolder);
        }
        //System.IO.File.WriteAllText(roaming+ "test.txt", roaming.ToString());
        if (!File.Exists(saveFolder + "test.txt"))
        {
            System.IO.File.CreateText(saveFolder + "test.txt");
            
        }
        System.IO.File.WriteAllText(saveFolder + "test.txt", saveFolder.ToString());
*/
    }

	void OnGUI()
	{
		
		if (!hasUpdatedGui) {
			ColoredGUISkin.Instance.UpdateGuiColors(primaryColors[0], secondaryColors[0]);
			hasUpdatedGui = true;
		}


		GUI.skin = ColoredGUISkin.Skin;
		//GUI.skin = skinny;
		//GUI.skin = DemoGuiSkin;
		/*for (i = 0; i <= primaryColors.Count; i++)
		{
			//ColoredGUISkin.Instance.UpdateGuiColors(primaryColors [i % primaryColors.Count],secondaryColors[i % secondaryColors.Count]);
			GUI.backgroundColor = new Color(1, 143/255F, 0, 1);
			if (GUI.Button (new Rect (100, 10+40*i, buttonX, buttonY), "test")) 
			{

			
				Debug.Log ("Just a test");	
			}
		}
		*/
		GUI.contentColor = Color.black;
		GUI.backgroundColor = colorWorst2;
		if (GUI.Button (new Rect (100, 10+40*1, buttonX, buttonY), "test")) 
		{
			
			
			Debug.Log ("Just a test");	
		}
		GUI.backgroundColor = colorWorse2;
		if (GUI.Button (new Rect (100, 10+40*2, buttonX, buttonY), "test")) 
		{
			
			
			Debug.Log ("Just a test");	
		}
		GUI.backgroundColor = colorBad2;
		if (GUI.Button (new Rect (100, 10+40*3, buttonX, buttonY), "test")) 
		{
			
			
			Debug.Log ("Just a test");	
		}
		GUI.backgroundColor = colorNeutral2;
		if (GUI.Button (new Rect (100, 10+40*4, buttonX, buttonY), "test")) 
		{
			
			
			Debug.Log ("Just a test");	
		} 
		GUI.backgroundColor = colorGood2;
		if (GUI.Button (new Rect (100, 10+40*5, buttonX, buttonY), "test")) 
		{
			
			
			Debug.Log ("Just a test");	
		} 
		GUI.backgroundColor = colorBetter2;
		if (GUI.Button (new Rect (100, 10+40*6, buttonX, buttonY), "test")) 
		{
			
			
			Debug.Log ("Just a test");	
		} 
		GUI.backgroundColor = colorBest2;
		if (GUI.Button (new Rect (100, 10+40*7, buttonX, buttonY), "test")) 
		{
			
			
			Debug.Log ("Just a test");	
		} 
        

	}
}
