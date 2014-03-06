﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using HTTP;
using System.Reflection;
using System;
using SimpleJSON;
using System.IO;
using System.Text.RegularExpressions;


public class UserStats : MonoBehaviour
{
    //public static string url = "https://beta.habitrpg.com/api/v1/user";
    //public static string url = "http://fragmental.no-ip.org:3000/api/v2/user";
    public static string url = Login.url;
    //string taskurl = "https://beta.habitrpg.com/api/v1/user/tasks";
	public float healthBarLength;
	//public float barDisplay; //current progress
    public Vector2 pos = new Vector2(100,40);
    //public Vector2 size = new Vector2(60,20);
    public float hpLength = 60;
    public float hpHeight = 40;
    public Texture2D emptyTex;
    public Texture2D fullTex;
	public Texture2D xpTex;
 	public Vector2 hudPos = new Vector2(0,0);
	public Vector2 hudSize = new Vector2(60,20);
	public Vector2 xpPos = new Vector2(150,70);
	//public Vector2 xpSize = new Vector2(60,20);
    public float xpLength = 60;
    public float xpHeight = 40;
	public Vector2 lvlPos = new Vector2(60,Screen.height/6-20);
	public Vector2 lvlSize = new Vector2(40,32);

    public Hashtable profile;
	public string name;
	//public string sName;
	public float health;
	public float maxHealth;
	//public float fMaxHealth;
	
	public JSONNode stats;

	public float level;
	public float lvl;
	public float gp;
	public float nextLevel;
	
	public float xp;
	
	public HabitDatav1 userData;
    public ArrayList habits;
    public ArrayList dailies;
    public ArrayList todos;
    public ArrayList rewards;
    
    public List<string> habitList = new List<string>();
    public List<string> habitIDList = new List<string>();
    //public List<string> habitButtPos = new List<string>();
    //public List<string> habitButtNeg = new List<string>();
    public List<bool> habitClick = new List<bool>();
    public List<bool> habitUp = new List<bool>();
    public List<bool> habitDown = new List<bool>();
	public List<float> habitValue = new List<float> ();
	public List<Color> habitColor = new List<Color> ();

    public List<string> dailyList = new List<string>();
    public List<bool> dailyListClicked = new List<bool>();
    public List<string> dailyIDList = new List<string>();
    public List<bool> dailyToggleList = new List<bool>();
    
    public List<string> todoList = new List<string>();
    public List<string> todoIDList = new List<string>();
    public List<bool> todoToggleList = new List<bool>();
    public List<bool> todoCompleted = new List<bool>();
    
    
    public List<string> rewardList = new List<string>();
    public List<string> rewardIDList = new List<string>();
    public List<long> rewardValue = new List<long>();
    

    private int i;
    public float buttSpread;
    public float buttHeight;
    public float buttLength;
    public float buttDistance;
    public float buttAddSpread = 72;
    public float buttSize = 34;
    public float buttSizeY = 3;
	public float buttToggle = 5;
	public float habitButtSpread;
	public float habitBoxSpread;

    public GUISkin newSkin;
    public GUISkin mySkin;
    
    public Vector2 scrollPosition = Vector2.zero;
    public float fullY;
   
  
    

    public string uid; 
    public string key; 

	public string habitAdd = "";
	public string dailyAdd = "";
	public string todoAdd = "";
	public string rewardAdd = "";

    public List<Color> primaryColors;
    public List<Color> secondaryColors;
    private bool hasUpdatedGui = false;

    private object ht;
    public string something;

	private string addType;
	private string addText;

	public float togPosX;
	public float togPosY;

	private bool updateColors = false;

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
	public GUIStyle hudStyle;

    void Start()
	{
		habitButtSpread = 30;
		habitBoxSpread = 50;
		togPosX = 8;
		togPosY = 3;
        uid = Login.uid;
        key = Login.key;
        //GUI.skin = ColoredGUISkin.Instance.UpdateGuiColors(primaryColors[0], secondaryColors[0]);
        float scrW = Screen.width;
		buttSize = 35;
		buttSizeY = (float)3.5;
	    healthBarLength = Screen.width / 2;
        Debug.Log("healthBarLength = " + healthBarLength);
	    hpLength =   healthBarLength;
	    xpLength = healthBarLength;
	    pos.y = Screen.height/30;
	    xpPos.y = pos.y + 30;
	    xpPos.x = 150;
	    pos.x = xpPos.x;
	    lvlPos.x = Screen.width/25;
	    lvlPos.y = Screen.height/6-40;
	    lvlSize.x = 80;
	    lvlSize.y = 40;
        buttHeight = 42;
        buttLength = Screen.width * 24 / 100;      
		Debug.Log("buttLength = " + buttLength);
        buttSpread = Screen.width / 4;
        Debug.Log("buttSpread = " + buttSpread);
        buttDistance = 28;
        hpHeight = 40;
        xpHeight = 40;

	    StartCoroutine(HrpgJson());		
    
	}
    
/*    public IEnumerator StatsUpdate()
    {
        //I was going to use this to update the xp, hp, and gp only, but...
    }
    */

    public IEnumerator HabitUpdate()
    {
        string hUrl = "no";
                
        if (habitClick[i] == true)//Positive
        {
            hUrl = url + "/tasks/" + habitIDList[i] +"/up";
            Debug.Log(hUrl);
        }
        else if(habitClick[i] == false)//Negative
        {
            
            hUrl = url + "/tasks/" + habitIDList[i] +"/down";
            Debug.Log(hUrl);
        }

        
                
        var request = new HTTP.Request("POST", hUrl);
                
        request.headers.Set("x-api-key", key);
        request.headers.Set("x-api-user", uid);
        request.headers.Set("Content-Type", "application/json");
        request.Send();
        while (!request.isDone) yield return new WaitForEndOfFrame();

        if (request.exception != null)
        {
            Debug.LogError(request.exception);
        }
        else
        {
            
            var response = request.response;
            //inspect response code
            Debug.Log(response.status);
            //inspect headers
            Debug.Log(response.headers.Get("Content-Type"));
            //Get the body as a byte array
            //Debug.Log(response.bytes);
            //Or as a string
            Debug.Log(response.Text);

            JSONNode hU = JSONNode.Parse(response.Text);
            float lvlUp = hU["lvl"].AsFloat;
            
            if (lvlUp == lvl)
            {
                lvl = lvlUp;
                health = hU["hp"].AsFloat;
                gp = hU["gp"].AsFloat;
                xp = hU["exp"].AsFloat;
            }
            else
            {
                StartCoroutine(HrpgJson());
            }
                
                
        }
        
        //dummy Text for Testing
        //Debug.Log(dUrl is dUrl)
        
    }

    public IEnumerator DailyUpdate()
    {
        string dUrl = " ";

        if (dailyToggleList[i] == true)//completed
        {
            dUrl = url + "/tasks/" + dailyIDList[i] + "/up";
            //request.Text = "{\"completed\": true}";
            Debug.Log("Should be checked");
        }
        else if (dailyToggleList[i] == false)//uncompleted
        {
            dUrl = url + "/tasks/" + dailyIDList[i] + "/down";
            Debug.Log("Should be unchecked");
            //request.Text = "{\"completed\" : false}";
        }



        var request = new HTTP.Request("POST", dUrl);

        request.headers.Set("x-api-key", key);
        request.headers.Set("x-api-user", uid);
        request.headers.Set("Content-Type", "application/json");
        request.Send();
        while (!request.isDone) yield return new WaitForEndOfFrame();

        if (request.exception != null)
        {
            Debug.LogError(request.exception);
        }
        else
        {

            var response = request.response;
            //inspect response code
            Debug.Log(response.status);
            //inspect headers
            Debug.Log(response.headers.Get("Content-Type"));
            //Get the body as a byte array
            //Debug.Log(response.bytes);
            //Or as a string
            Debug.Log(response.Text);

            JSONNode dU = JSONNode.Parse(response.Text);
            float lvlUp = dU["lvl"].AsFloat;

            if (lvlUp == lvl)
            {
                lvl = lvlUp;
                health = dU["hp"].AsFloat;
                gp = dU["gp"].AsFloat;
                xp = dU["exp"].AsFloat;
            }
            else
            {
                StartCoroutine(HrpgJson());
            }


        }
    }

    /*public IEnumerator DailyUpdate()
    {
        var dUrl = url + "/task/" + dailyIDList[i];

        var request = new HTTP.Request("PUT", dUrl);

        request.headers.Set("x-api-key", key);
        request.headers.Set("x-api-user", uid);
        request.headers.Set("Content-Type", "application/json");

        if (dailyToggleList[i] == true)
        {
            request.Text = "{\"completed\": true}";
            Debug.Log("Should be checked");
        }
        else if (dailyToggleList[i] == false)
        {
            Debug.Log("Should be unchecked");
            request.Text = "{\"completed\" : false}";
        }
        request.Send();
        while (!request.isDone) yield return new WaitForEndOfFrame();

        if (request.exception != null)
        {
            Debug.LogError(request.exception);
        }
        else
        {
            StartCoroutine(HrpgJson());
            var response = request.response;
            //inspect response code
            Debug.Log(response.status);
            //inspect headers
            Debug.Log(response.headers.Get("Content-Type"));
            //Get the body as a byte array
            //Debug.Log(response.bytes);
            //Or as a string
            Debug.Log(response.Text);
            var dailyResponse = response.Text;
            Type t = dailyResponse.GetType();
            Debug.Log("dailyResponse type is " + t.FullName);
            /*if (dailyResponse is Hashtable)
            {

            }
            
        }

    }
    */
    public IEnumerator TodoUpdate()
    {
        string tUrl = " ";

        tUrl = url + "/tasks/" + todoIDList[i] + "/up";

        var request = new HTTP.Request("POST", tUrl);

        request.headers.Set("x-api-key", key);
        request.headers.Set("x-api-user", uid);
        request.headers.Set("Content-Type", "application/json");
        request.Send();
        while (!request.isDone) yield return new WaitForEndOfFrame();

        if (request.exception != null)
        {
            Debug.LogError(request.exception);
        }
        else
        {

            var response = request.response;
            //inspect response code
            Debug.Log(response.status);
            //inspect headers
            Debug.Log(response.headers.Get("Content-Type"));
            //Get the body as a byte array
            //Debug.Log(response.bytes);
            //Or as a string
            Debug.Log(response.Text);

            JSONNode tU = JSONNode.Parse(response.Text);
            float lvlUp = tU["lvl"].AsFloat;

            if (lvlUp == lvl)
            {
                lvl = lvlUp;
                health = tU["hp"].AsFloat;
                gp = tU["gp"].AsFloat;
                xp = tU["exp"].AsFloat;
            }
            else
            {
                StartCoroutine(HrpgJson());
            }


        }
    }

    
 
    public IEnumerator RewardUpdate()
    {
        string rUrl = " ";
                
        
        rUrl = url + "/tasks/" + rewardIDList[i] +"/up";
        Debug.Log(rUrl);
                        
        var request = new HTTP.Request("POST", rUrl);
                
        request.headers.Set("x-api-key", key);
        request.headers.Set("x-api-user", uid);
        request.headers.Set("Content-Type", "application/json");
        request.Send();
        while (!request.isDone) yield return new WaitForEndOfFrame();

        if (request.exception != null)
        {
            Debug.LogError(request.exception);
        }
        else
        {

            var response = request.response;
            //inspect response code
            Debug.Log(response.status);
            //inspect headers
            Debug.Log(response.headers.Get("Content-Type"));
            //Get the body as a byte array
            //Debug.Log(response.bytes);
            //Or as a string
            Debug.Log(response.Text);

            JSONNode rU = JSONNode.Parse(response.Text);
            gp = rU["gp"].AsFloat;
            

        }            
    }

	public IEnumerator TaskAdd()
    {
        var tAUrl = url + "/tasks/";
//{"text":"from the api!","type":"todo"}
        var request = new HTTP.Request("POST", tAUrl);

        request.headers.Set("x-api-key", key);
        request.headers.Set("x-api-user", uid);
        request.headers.Set("Content-Type", "application/json");
		string taskAddJson = "{\"text\": \""+addText+"\",\"type\":\""+addType+"\"}";
		Debug.Log("taskAddJson is "+taskAddJson);
		request.Text = taskAddJson;
		
		
        request.Send();
        while (!request.isDone) yield return new WaitForEndOfFrame();

        if (request.exception != null)
        {
            Debug.LogError(request.exception);
        }
        else
        {
            StartCoroutine(HrpgJson());
            var response = request.response;
            //inspect response code
            Debug.Log(response.status);
            //inspect headers
            Debug.Log(response.headers.Get("Content-Type"));
            //Get the body as a byte array
            //Debug.Log(response.bytes);
            //Or as a string
            Debug.Log(response.Text);
            //var dailyResponse = response.Text;
            //Type t = dailyResponse.GetType();
            //Debug.Log("dailyResponse type is " + t.FullName);
            //if (dailyResponse is Hashtable)
            {

            }
            
        }

    }
    


	public IEnumerator HrpgJson ()
	{
        //if (!PlayerPrefs.HasKey("jsonSave"))
        //{
            var request = new HTTP.Request("GET", url);
	        //set headers
            request.headers.Set("x-api-key", key);
            request.headers.Set("x-api-user", uid);
	        request.Send();
	        while(!request.isDone) yield return new WaitForEndOfFrame();



            if (request.exception != null)
            {
                Debug.LogError(request.exception);
            }
            else
            {
                var response = request.response;
                //inspect response code
                Debug.Log(response.status);
                //inspect headers
                Debug.Log(response.headers.Get("Content-Type"));
                //Get the body as a byte array
                //Debug.Log(response.bytes);
                //Or as a string
                Debug.Log(response.Text);
                /*if (!Directory.Exists(Environment.SpecialFolder.ApplicationData + @"\.h3d\"))
                {
                    Directory.CreateDirectory(Environment.SpecialFolder.ApplicationData + @"\.h3d\");
                }
                //System.IO.File.WriteAllText(Environment.SpecialFolder.ApplicationData + @"\.h3d\" + "Jsonsave.txt", response.Text.ToString());			
		        */

                userData = new HabitDatav1(response.Text);
                ht = userData.ht;
                //object setupData = new HabitDatav1.setupData(task);
                PlayerPrefs.SetString("jsonSave", response.Text);
                Debug.Log("Player prefs after if is " + PlayerPrefs.GetString("jsonSave"));
                something = "yes";
				Debug.Log ("string 'something' equals" + something);
            }
        /*}
        else
        {
            Debug.Log("Player prefs in else is " + PlayerPrefs.GetString("jsonSave"));
            userData = new HabitDatav1(PlayerPrefs.GetString("jsonSave"));
            ht = userData.ht;
            something = "yes";
            Debug.Log("ht in else is" + ht);
        }
        */
    		//to parse api/user
		if (something == "yes") 
		{
             
			Hashtable ht2 = ht as Hashtable;
            Debug.Log("ht is not null and ht2 is" + ht2);
			//stats = ht2["stats"] as Hashtable;
            stats = userData.Stats;
			Type t = userData.Habits.GetType();
			Debug.Log("xp type is " + t.FullName);
			profile = ht2["profile"] as Hashtable;
            habits = ht2["habits"] as ArrayList;
            //habits = userData.Habits;
            dailies = ht2["dailys"] as ArrayList;
            todos = ht2["todos"] as ArrayList;
            rewards = ht2["rewards"] as ArrayList;
								
			health = stats["hp"].AsFloat;
				
			maxHealth = stats["maxHealth"].AsFloat;
			name = userData.ProfileName;
			lvl = stats["lvl"].AsFloat;
            gp = stats["gp"].AsFloat;
            nextLevel = stats["toNextLevel"].AsFloat;
            xp = stats["exp"].AsFloat;
			//var value = habits["value";
			//Type t = xp.GetType();
            //Type t2 = nextLevel.GetType();
						
			//Debug.Log("xp type is " + t.FullName);
            //Debug.Log("level = " + nextLevel);
			//barDisplay =  health;
						

            //clears list so they can be repopulated on refresh, and populates the display lists(habits, todos, dailies, rewards)
			int i =0;
			habitList.Clear();
            habitUp.Clear();
            habitDown.Clear();
            habitIDList.Clear();
            habitClick.Clear();
            foreach (Hashtable habit in habits)
            {


                habitList.Add((string)habit["text"]);
                habitUp.Add((bool)habit["up"]);
                habitDown.Add((bool)habit["down"]);
                habitIDList.Add((string)habit["id"]);
                habitClick.Add(true);
				habitValue.Add((float) habit["value"]);
				//var valueType = (float)habit["value"];
				//Type t = valueType.GetType();

				//Debug.Log("value type is " + t.FullName);
				//Debug.Log((string)habit["text"]);

				if (habitValue[i] < -20)
				{
				habitColor.Add(colorWorst);
				}
				else if (habitValue[i]< -10)
				{
				habitColor.Add(colorWorse);
				}
				else if (habitValue[i]< -1)
				{
				habitColor.Add(colorBad);
				}
				else if (habitValue[i]< 1)
				{
				habitColor.Add(colorNeutral);
				}
				else if (habitValue[i]< 5)
				{
				habitColor.Add(colorGood);
				}
				else if (habitValue[i]< 10)
				{
				habitColor.Add(colorBetter);
				}
				else
				{
				habitColor.Add(colorBest);
				}
				i++;
            }
                
                
            dailyList.Clear();
            dailyToggleList.Clear();
            dailyListClicked.Clear();
            dailyIDList.Clear();
            foreach (Hashtable daily in dailies)
            {
                dailyList.Add((string)daily["text"]);
                dailyToggleList.Add((bool)daily["completed"]);
                dailyListClicked.Add((bool)daily["completed"]);
                dailyIDList.Add((string)daily["id"]);
            }

            todoList.Clear();
            todoToggleList.Clear();
            todoIDList.Clear();
            todoCompleted.Clear();
            foreach (Hashtable todo in todos)
            {
                todoList.Add((string)todo["text"]);
                todoToggleList.Add((bool)todo["completed"]);
                todoCompleted.Add((bool)todo["completed"]);
                todoIDList.Add((string)todo["id"]);
            }

            rewardList.Clear();
            rewardValue.Clear();
            rewardIDList.Clear();
            foreach (Hashtable reward in rewards)
            {
                rewardList.Add((string)reward["text"]);
                rewardValue.Add((long)reward["value"]);
                rewardIDList.Add((string)reward["id"]);
            }
            StartCoroutine(ScrollCalc());
  		}	
		
	}

    public IEnumerator ScrollCalc()
    {

        fullY = Math.Max(habitList.Count, Math.Max(todoList.Count, Math.Max(dailyList.Count, rewardList.Count)));
        yield return new WaitForSeconds(0.0f);
    }

						
	void OnGUI() 
    {
        if (!hasUpdatedGui)
        {
            ColoredGUISkin.Instance.UpdateGuiColors(primaryColors[0], secondaryColors[0]);
            hasUpdatedGui = true;
        }

        GUI.skin = ColoredGUISkin.Skin;
        // GUI.skin
        //GUI.skin = ColoredGUISkin.Instance.UpdateGuiColors(primaryColors[0], secondaryColors[0]);
        //GUI.skin = newSkin;
        //GUI.skin = ColoredGUISkin.Skin;
		GUI.BeginGroup(new Rect(0, 0, Screen.width, Screen.height/6));
			GUI.Box(new Rect(0,0, Screen.width, Screen.height/6), emptyTex);
			GUI.Box(new Rect(lvlPos.x, lvlPos.y, lvlSize.x, lvlSize.y), "Lvl " + lvl);
			GUI.Box(new Rect(0,0, 150, 40 ), name);
            if (GUI.Button(new Rect(Screen.width - 110, 15, 90, 35), "Quit"))
            {

                Application.Quit();

            }
            if (GUI.Button(new Rect(Screen.width - 110, Screen.height / 6 - Screen.height/20 , 90, 35), "Refresh"))
            {
                StartCoroutine(HrpgJson());
            }
			//draw the hp background
			GUI.BeginGroup(new Rect(pos.x, pos.y,Screen.width/2, hpHeight));
			    GUI.Box(new Rect(0,0,Screen.width/2, hpHeight), emptyTex);
				GUI.DrawTexture(new Rect(10, 9, health/maxHealth*Screen.width/2-19, hpHeight-19), fullTex, ScaleMode.StretchToFill, true, 2.5F);	
				GUI.Box(new Rect(0,0,Screen.width/2, hpHeight), health + "/" + maxHealth, hudStyle);
			 
			    //draw the hp filled-in part:
			    //GUI.BeginGroup(new Rect(0,0, health/maxHealth*Screen.width/2, hpHeight));
						
			        //GUI.Box(new Rect(0,0,Screen.width/2, hpHeight), fullTex);
					//GUI.Box (new Rect(10, 10, 100, 20), barDisplay + "/" + MaxHealth);
			    //GUI.EndGroup();
			GUI.EndGroup();
			
			GUI.BeginGroup(new Rect(xpPos.x, xpPos.y, Screen.width/2, xpHeight));
			    GUI.Box(new Rect(0,0, Screen.width/2, xpHeight), emptyTex);
				GUI.DrawTexture(new Rect(10, 9, xp/nextLevel*Screen.width/2-19, xpHeight-19), xpTex, ScaleMode.StretchToFill, true, 2.5F);
				GUI.Box(new Rect(0,0, Screen.width/2, xpHeight), xp + "/" + nextLevel,hudStyle);
				//GUI.BeginGroup(new Rect(0,0, xp/nextLevel*Screen.width/2, xpHeight));
			    //    GUI.Box(new Rect(0,0, Screen.width/2, xpHeight), fullTex);
							
			    //GUI.EndGroup();
			GUI.EndGroup();
		GUI.EndGroup();

        scrollPosition = GUI.BeginScrollView(new Rect(0, Screen.height / 6, Screen.width, Screen.height-(Screen.height/6)), scrollPosition, new Rect(0, Screen.height / 6, Screen.width, fullY*buttDistance+buttAddSpread));


        //List Habits
        GUI.BeginGroup(new Rect(5, Screen.height / 6, Screen.width, habitList.Count * buttDistance + buttAddSpread));
        GUI.Box(new Rect(0, 0, Screen.width * 24 / 100, buttHeight), "Habits");
		habitAdd = GUI.TextField(new Rect(0, 30, Screen.width * 24 / 100-20, buttHeight), habitAdd, 100);
		if (GUI.Button(new Rect(Screen.width * 24 / 100-35, 30+buttSizeY, buttSize, buttSize), "+"))
		{
			if (habitAdd != "")
			{
				addType = "habit";
				addText = habitAdd;
				habitAdd="";
				StartCoroutine(TaskAdd()); 
				Debug.Log("you pressed button + on " + habitList[i]);
			}
		}
        GUI.BeginGroup(new Rect(0, 60, Screen.width, habitList.Count * buttDistance + buttAddSpread));


            for (i = 0; i < habitList.Count; i++)
            {
			GUI.backgroundColor = habitColor[i];
                GUI.Box(new Rect(habitBoxSpread, i * buttDistance, Screen.width * 24 / 100-42, buttHeight), habitList[i]);
                if(habitUp[i])
                {
                    if (GUI.Button(new Rect(5, i * buttDistance + buttSizeY, buttSize, buttSize), "+"))
                    {
					//ColoredGUISkin.Instance.UpdateGuiColors(primaryColors[i], secondaryColors[i]);
                        habitClick[i] = true;
                        StartCoroutine(HabitUpdate()); 
                        Debug.Log("you pressed button + on " + habitList[i]);
                    }
                }

                if (habitDown[i])
                {
                    if (GUI.Button(new Rect(habitButtSpread, i * buttDistance + buttSizeY, buttSize, buttSize), "-"))
                    {
                        habitClick[i] = false;
                        StartCoroutine(HabitUpdate()); 
                        Debug.Log("you pressed button - on " + habitList[i]);
                    }
                }
                
            }
            GUI.EndGroup();
        GUI.EndGroup();
            
        //List Dailies
        GUI.BeginGroup(new Rect(Screen.width/4 + 5, Screen.height / 6, Screen.width, dailyList.Count * buttDistance + buttAddSpread));
        GUI.Box(new Rect(0, 0, Screen.width * 24 / 100, buttHeight), "Dailies");
		//int ferk = (int)Screen.width * 24 / 100 - 4;
		//Debug.Log ("textLength is " + ferk);
		dailyAdd = GUI.TextField(new Rect(0, 30, Screen.width * 24 / 100-20, buttHeight), dailyAdd, 100);
		//uid = GUI.TextField(new Rect(xShift + 110, yShift + 120, 300, 30), uid, 36);
		if (GUI.Button(new Rect(Screen.width * 24 / 100-35, 30+buttSizeY, buttSize, buttSize), "+"))
		{
			if (dailyAdd != "")
			{
				addType = "daily";
				addText = dailyAdd;
				dailyAdd="";
				StartCoroutine(TaskAdd()); 
			}

		}
        GUI.BeginGroup(new Rect(0, 60, Screen.width, dailyList.Count * buttDistance + buttAddSpread));
                
                
            for (i = 0; i < dailyList.Count; i++)
            {

                GUI.Box(new Rect(20, i * buttDistance, Screen.width * 24 / 100-20, buttHeight), dailyList[i]);
				GUI.Box(new Rect(0, i * buttDistance, 40 , buttHeight), emptyTex);
				
                dailyListClicked[i] = GUI.Toggle(new Rect(togPosX , i * buttDistance+togPosY, Screen.width * 24 / 100 , buttDistance ), dailyToggleList[i], dailyList[i]);
                        
                if(dailyListClicked[i] != dailyToggleList[i])
                {
                    dailyToggleList[i] = dailyListClicked[i];
                    StartCoroutine(DailyUpdate());
                }
            }
            GUI.EndGroup();
        GUI.EndGroup();

        //List Todos
        GUI.BeginGroup(new Rect(Screen.width/4 * 2 + 5, Screen.height / 6, Screen.width, todoList.Count * buttDistance + buttAddSpread));
        GUI.Box(new Rect(0, 0, Screen.width * 24 / 100, buttHeight), "Todos");
		todoAdd = GUI.TextField(new Rect(0, 30, Screen.width * 24 / 100-20, buttHeight), todoAdd, 100);
		if (GUI.Button(new Rect(Screen.width * 24 / 100-35, 30+buttSizeY, buttSize, buttSize), "+"))
		{
			if (todoAdd != "")
			{
				addType = "todo";
				addText = todoAdd;
				todoAdd="";
				StartCoroutine(TaskAdd()); 
			}
			
		}
        GUI.BeginGroup(new Rect(0, 60, Screen.width, todoList.Count * buttDistance + buttAddSpread));

            for (i = 0; i < todoList.Count; i++)
            {
                if (todoCompleted[i] == false)
                {
                    GUI.Box(new Rect(20, i * buttDistance, Screen.width * 24 / 100-20, buttHeight), todoList[i]);
					GUI.Box(new Rect(0, i * buttDistance, 40 , buttHeight), emptyTex);


				if (todoToggleList[i] = GUI.Toggle(new Rect(0, i * buttDistance, Screen.width * 24 / 100, buttDistance), todoToggleList[i], todoList[i]))
                    {
                        StartCoroutine(TodoUpdate());
                        todoCompleted[i] = true;
                        todoToggleList[i] = true;
                        Debug.Log("you pressed button " + todoList[i]);

                    }
                }

            }


            GUI.EndGroup();

        GUI.EndGroup();

        //List Rewards
        GUI.BeginGroup(new Rect(Screen.width/4 * 3 + 5, Screen.height / 6, Screen.width, rewardList.Count * buttDistance + buttAddSpread));
        GUI.Box(new Rect(0, 0, Screen.width * 24 / 100, buttHeight), "Rewards  "+ gp+" GP");
		rewardAdd = GUI.TextField(new Rect(0, 30, Screen.width * 24 / 100-20, buttHeight), rewardAdd, 100);
		if (GUI.Button(new Rect(Screen.width * 24 / 100-35, 30+buttSizeY, buttSize, buttSize), "+"))
		{
			if (rewardAdd != "")
			{
				addType = "reward";
				addText = rewardAdd;
				rewardAdd="";
				StartCoroutine(TaskAdd()); 
			}
			
		}
        GUI.BeginGroup(new Rect(0, 60, Screen.width, rewardList.Count * buttDistance + buttAddSpread));

            for (i = 0; i < rewardList.Count; i++)
            {


                if (GUI.Button(new Rect(0, i * buttDistance, Screen.width * 24 / 100, buttHeight), "Buy (" + rewardValue[i]+") "+rewardList[i]))
                {

                    Debug.Log("you pressed button " + rewardList[i]);
                    StartCoroutine(RewardUpdate());
                }

            }


            GUI.EndGroup();

        GUI.EndGroup();

        GUI.EndScrollView();

    }
		    
}	
			
				
				

		
			
	
	
	
	








