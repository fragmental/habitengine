using UnityEngine;
using System.Collections;

public class ToggleExample : MonoBehaviour
{
    /*public Texture aTexture;
    private bool toggleTxt = false;
    private bool toggleImg = false;

    void OnGUI()
    {
        if (!aTexture)
        {
            Debug.LogError("Please assign a texture in the inspector.");
            return;
        }
        Debug.Log("OnGUI is super lame.");
        toggleTxt = GUI.Toggle(new Rect(10, 10, 100, 30), toggleTxt, "A Toggle text");
        toggleImg = GUI.Toggle(new Rect(10, 50, 50, 50), toggleImg, aTexture);
    }
     
    bool toggleBool = true;
    //bool toggleBoolNew;


    void OnGUI()
    {
        new toggleBoolNew = GUI.Toggle(new Rect(25, 25, 100, 30), toggleBool, "Toggle");

        // Check if the toggle was toggled
        if (toggleBoolNew != toggleBool)
        {
            if (toggleBoolNew == true)
                Debug.Log("Toggle was enabled");
            else
                Debug.Log("Toggle was disabled");

            toggleBool = toggleBoolNew;
        }
    }

    private bool toggleBoolNew()
    {
        throw new System.NotImplementedException();
    }
     */
    public bool checkBoxClicked;
    public bool checkBox = false;
    //public bool checkBoxUpdate =;

    void OnGUI()
    {
        //Debug.Log("CheckBox Before = " + checkBox);
        checkBoxClicked = GUI.Toggle(new Rect(25, 25, 100, 30), checkBox, "checkBox");
        //Debug.Log("CheckBox After = " + checkBox);
        //Debug.Log("Outside if");
         if (checkBoxClicked != checkBox)
         {
             //Debug.Log("Inside if");
             //Debug.Log("CheckBox Before = " + checkBox);
             //checkBox = GUI.Toggle(new Rect(25, 25, 100, 30), checkBox, "checkBox");
             //checkBoxClicked = false;
             //Debug.Log("CheckBox After = " + checkBox);
             /*if (checkBoxClicked == true)
                 Debug.Log("Toggle was enabled");
             else
                 Debug.Log("Toggle was disabled");
             */
             checkBox = checkBoxClicked;
             StartCoroutine(Checking());
             
         }

         
    }
    public IEnumerator Checking()
    {
        if (checkBox == true)
            Debug.Log("Box is Checked");
        else if (checkBox == false)
            Debug.Log("Box is not checked");
        yield return new WaitForSeconds(0.0f);
    }



}
