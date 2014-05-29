using UnityEngine;
using System.Collections;
using HTTP;
using SimpleJSON;
using System.Collections.Generic;
using System.Reflection;
using System;
//using System.Exception;

public class HabitDatav1 
{
	public JSONNode root;  
	public object ht;
	public JSONNode tasks;
	//JsonSerializer.Decode (response.Text);
	
	
	public HabitDatav1(string jsonText)
	{
	ht = JsonSerializer.Decode (jsonText);
	//root = JSONNode.Parse (jsonText);
    setupData(JSONNode.Parse(jsonText));
	}
	
    //This is a Property 
	public string ProfileName
	{
		get {return root["profile"]["name"];}
        //set { }
	}

    public JSONNode Stats 
    {
        get { return root["stats"]; }
        
    }

    public JSONNode Habits
    {
        get { return root["habits"]; }
    }

	public JSONNode Dailies
	{
		get { return root["dailys"]; }
	}

	public JSONNode Todos
	{
		get { return root["todos"]; }
	}

	public JSONNode Rewards
	{
		get { return root["rewards"]; }
	}
	/*public bool setupData(JSONNode data) 
	{
	try 
	{
		
			root = data;

			JSONNode tasks = data.ToString("tasks");
			
			return true;
		} catch (Exception e) {
			Debug.Log (e.StackTrace);
		}
		return false;
	}*/
    public virtual bool setupData(JSONNode data)
    {

        try
        {
            root = data;

			//copying code from java?
           // JSONNode tasks = data.ToString("tasks");
			string taskString = data.ToString();
			Debug.Log("taskString is "+ taskString);
            return true;

        }
        catch (Exception e)
        {
            Debug.Log (e.ToString());
            Debug.Log (e.StackTrace);
        }
        return false;
     
    }
    
    
}

