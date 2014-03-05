using UnityEngine;
using System.Collections;

public class helloWorld : MonoBehaviour 
{
    public string[] items;
    public Rect Box;
    public string slectedItem = "None";

    private bool editing = false;

    private void OnGUI()
    {
        if (GUI.Button(Box, slectedItem))
        {
            editing = true;
        }

        if (editing)
        {
            for (int x = 0; x < items.Length; x++)
            {
                if (GUI.Button(new Rect(Box.x, (Box.height * x) + Box.y + Box.height, Box.width, Box.height), items[x]))
                {
                    slectedItem = items[x];
                    editing = false;
                }
            }
        }

    }
}