using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using HTTP;
//using HTTP;


public class Login : MonoBehaviour
{
    //public string bUrl = "http://beta.habitrpg.com/api/v2/user";
	//public string bUrl = "https://www.habitrpg.com/api/v2/user";
	public string bUrl = "http://fragmental.no-ip.org:3000/api/v2
    public static string url = bURL+ "/user";
    private string aUrl = bUrl + "/auth/local";
	private string cUrl = bUrl + "/status";
    //public static string user = "b2f17791-3247-462b-8cfe-86e9f9bca28f";
    public static string uid = "";
    //public static string key = "45482a67-8c71-4595-bfa5-f19ddeca8d95";
    public static string key = "";
    //public HabitDatav1 userData;
    public static string uName = "";
    public static string pass = "";

    private string auth;

    public bool error;

    
    public float screenWidth;
    public float screenHeight;
    public float xShift;
    public float yShift;
    public float xShiftA;


	public List<Color> primaryColors;
	public List<Color> secondaryColors;
	private bool hasUpdatedGui = false;

	bool serverUp;
	//bool choice=false;
	bool uPass=false;

	public float lPBSX=320;
	public float lPBSY=34;
	public float lPBPX=-10;
	public float lPBPY=60;
	public float lABSX=270;
	public float lABSY=34;
	public float lABPX=-10;
	public float lABPY=45;

	public float lAXS;
	public float lPXS;

    string introString1 = "Welcome to the HabitRPG Desktop PreAlpha.";
    string introString2 = "To login with facebook, you must use your API UID and Token.";
    //public GUISkin mySkin;

        // Disabling UI if blockUI is true: 
        //GUI.enabled = !blockUI;

        // Main label:
    void Start()
    {
		//var sockOut =Security.PrefetchSocketPolicy("http://fragmental.no-ip.org", 843);
		//Security.PrefetchSocketPolicy("http://fragmental.no-ip.org", 843);

		//Debug.Log ("Eff this waste " + sockOut);
		 lPBSX=320;
		 lPBSY=34;
		 lPBPX=-10;
		 lPBPY=35;
		 lABSX=270;
		 lABSY=34;
		 lABPX=-10;
		 lABPY=60;
		//StartCoroutine(CheckServer());
        screenWidth = Screen.width;
         screenHeight = Screen.height;
        
        Debug.Log("Screen.width = " + Screen.width);
        Debug.Log("screenWidth = " + screenWidth);

        //xShiftA = screenWidth / 2;
        xShift = Screen.width / 2 - introString1.Length * 3;
		yShift = Screen.height / 2 - Screen.height/4;


        if (PlayerPrefs.HasKey("uid") && PlayerPrefs.HasKey("key"))
        {

            uid = PlayerPrefs.GetString("uid");
            key = PlayerPrefs.GetString("key");
        }

        if (PlayerPrefs.HasKey("uName") && PlayerPrefs.HasKey("pass"))
        {

            uName = PlayerPrefs.GetString("uName");
            pass = PlayerPrefs.GetString("pass");
        }

    }

    public IEnumerator LoginRetrieve()
    {
        auth = "{\"username\":\""+uName +"\",\"password\":\""+pass+"\"}";
        Debug.Log("auth is "+auth);
        var request = new HTTP.Request("POST", aUrl);

        
        request.headers.Set("Content-Type", "application/json");
        request.Text = auth;
        
         


        /*var request = new HTTP.Request("GET", url);
        //set headers
        request.headers.Set("username", uName);
        request.headers.Set("password", pass);
         */
        request.Send();
        while (!request.isDone) yield return new WaitForEndOfFrame();
        

        if (request.exception != null)
        {
            Debug.LogError(request.exception);
            error = true;

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
            //Debug.Log(response.Text);
            //string authResponse = response.Text;
            //Type t = authResponse.GetType();
            Hashtable authResponse = JsonSerializer.Decode(response.Text) as Hashtable;
            //Debug.Log("Type is " + t.FullName);
            //Debug.Log(authObject["id"]);

            if (response.status == 200)
            {
                //possibly not secure
                //PlayerPrefs.SetString("uName", uName);
                //PlayerPrefs.SetString("pass", pass);
                uid = authResponse["id"] as string;
                key = authResponse["token"]as string;
                StartCoroutine(LoginRequest());

                //Application.LoadLevel("Main");
                
            }
            else
            {
                error = true;
            }
            
        }
        
    }
    
	public IEnumerator CheckServer()
	{
		var request = new HTTP.Request("POST", cUrl);
		
		
		//request.headers.Set("Content-Type", "application/json");
		request.Send();
		while (!request.isDone) yield return new WaitForEndOfFrame();
		
		
		if (request.exception != null)
		{
			Debug.LogError(request.exception);
			error = true;

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
			//Debug.Log(response.Text);
			//string authResponse = response.Text;
			//Type t = authResponse.GetType();
//			Hashtable authResponse = JsonSerializer.Decode(response.Text) as Hashtable;
			//Debug.Log("Type is " + t.FullName);
			//Debug.Log(authObject["id"]);
			
			if (response.status == 200)
			{
				serverUp = true;				
			}
			else
			{
				serverUp = false;
			}
		}
	}
    public IEnumerator LoginRequest()
    {
        var request = new HTTP.Request("GET", url);
        //set headers
		request.headers.Set("x-api-key", key);
        request.headers.Set("x-api-user", uid);
        request.Send();
        while (!request.isDone) yield return new WaitForEndOfFrame();


        if (request.exception != null)
        {
            Debug.LogError(request.exception);
            error = true;
            
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
            if(response.status == 200)
            {
                //possibly not secure
                PlayerPrefs.SetString("uid", uid);
                PlayerPrefs.SetString("key", key);
                Application.LoadLevel("Main");
            }
            else
            {
                error = true;
            }
            //userData = new HabitDatav1(response.Text);
            //object ht = userData.ht;
            
        }
    }




    void OnGUI()
    {

		if (!hasUpdatedGui) {
			ColoredGUISkin.Instance.UpdateGuiColors(primaryColors[0], secondaryColors[0]);
			hasUpdatedGui = true;
		}

		GUI.skin = ColoredGUISkin.Skin;

		//if (serverUp == true)
		//{


			//GUI.skin = FantasyColorable;
			GUI.Label(new Rect(Screen.width / 2 - introString1.Length * 3, Screen.height / 2 - Screen.height/4 , Screen.width, 30), introString1);
	       
	        
	        GUI.Label(new Rect(Screen.width / 2 - introString1.Length * 3, Screen.height / 2 - Screen.height/4 + 20, Screen.width, 30), introString2);
	        //GUI.Label(new Rect(Screen.width / 2 - introString1.Length * 3, Screen.height / 2 - Screen.height/4 + 40, Screen.width, 30), "To test without uid and api key, simply hit the 'testercules' button.  ");
	        //GUI.Label(Rect(0, Screen.height / 2 - Screen.height/4 + 40, Screen.width, 30), "For testercules use username 'testercules' and password 'tester'", header1Style);

			if (GUI.Button(new Rect(Screen.width / 2 - introString1.Length * 3 + lPBPX, Screen.height / 2 - Screen.height/4 + lPBPY, lPBSX, lPBSY), "Login with Username and Password"))
			{
				uPass=true;
				//choice=true;
			}
			if (GUI.Button(new Rect(Screen.width / 2 - introString1.Length * 3 + lABPX, Screen.height / 2 - Screen.height/4 + lABPY, lABSX, lABSY), "Login with API UID and Token"))
			{
				uPass=false;
				//choice=true;
			}

	        // Message label:
	        if (error)
	        {
	            GUI.Label(new Rect(Screen.width / 2 - introString1.Length * 3, Screen.height / 2 - Screen.height/4 + 90, Screen.width, 30), "There was an error in the login.  Please try again");
	        }
	        else
	        {
	            GUI.Label(new Rect(Screen.width / 2 - introString1.Length * 3, Screen.height / 2 - Screen.height/4 + 90, Screen.width, 30), "Enter your HabitRPG User ID and API Token");
	        }

			if (uPass)
			{
	        // uid label and uid text field:
		        GUI.Label(new Rect(Screen.width / 2 - introString1.Length * 3, Screen.height / 2 - Screen.height/4 + 130, 100, 40), "Enter User Name");
		        uName = GUI.TextField(new Rect(Screen.width / 2 - introString1.Length * 3 + 110, Screen.height / 2 - Screen.height/4 + 120, 350, 50), uName, 36);

		        // key label and key text field:
		        GUI.Label(new Rect(Screen.width / 2 - introString1.Length * 3, Screen.height / 2 - Screen.height/4 + 180, 100, 40), "Enter Password");
		        pass = GUI.PasswordField(new Rect(Screen.width / 2 - introString1.Length * 3 + 110, Screen.height / 2 - Screen.height/4 + 170, 350, 50), pass, "*"[0],36);
		
		        // Login button:
		        if (GUI.Button(new Rect(Screen.width / 2 - introString1.Length * 3 - 10, Screen.height / 2 - Screen.height/4 + 220, 320 + lPXS, 35), "Login with Username and Password") || 
			       (Event.current.isKey && Event.current.keyCode == KeyCode.Return))
				{
					//StartCoroutine(LoginRequest());
					StartCoroutine(LoginRetrieve());
					
				}
			}
			else if (!uPass)
			{
				// uid label and uid text field:
				GUI.Label(new Rect(Screen.width / 2 - introString1.Length * 3, Screen.height / 2 - Screen.height/4 + 130, 100, 40), "Enter UID");
				uid = GUI.TextField(new Rect(Screen.width / 2 - introString1.Length * 3 + 110, Screen.height / 2 - Screen.height/4 + 120, 350, 50), uid, 36);
				
				// key label and key text field:
				GUI.Label(new Rect(Screen.width / 2 - introString1.Length * 3, Screen.height / 2 - Screen.height/4 + 180, 100, 40), "Enter Token");
				key = GUI.TextField(new Rect(Screen.width / 2 - introString1.Length * 3 + 110, Screen.height / 2 - Screen.height/4 + 170, 350, 50), key,36);
				
				// Login button:
				if (GUI.Button(new Rect(Screen.width / 2 - introString1.Length * 3 - 10, Screen.height / 2 - Screen.height/4 + 220, 320 + lAXS, 35), "Login with UID and Token") || 
				    (Event.current.isKey && Event.current.keyCode == KeyCode.Return))
				{
					StartCoroutine(LoginRequest());
					//StartCoroutine(LoginRetrieve());
					
				}
			}
		
		
		//Login with Testercules
		/*if (GUI.Button( new Rect(Screen.width / 2 - introString1.Length * 3, Screen.height / 2 - Screen.height/4 + 220, 120, 30), "testercules!"))
	        {
	            uid = "f78c4fdf-f404-4922-8d83-cbcf941119c4";
	            key = "345a363b-6092-422f-9dfb-9caf3373c4e0";
	            //uid = "b2f17791-3247-462b-8cfe-86e9f9bca28f";
	            //key = "45482a67-8c71-4595-bfa5-f19ddeca8d95";
	            StartCoroutine(LoginRequest());
	            //   openRegistrationHandler();

	        }
	*/
			
	        
	        // Enabling UI: 

	        //GUI.enabled = true;
		}
	//}

}