#undef UseUniweb
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using HTTP;
using SimpleJSON;

using System.Net; // Dns.GetHostEntry

public class Login : MonoBehaviour
{
//	private string bUrl = "http://healpha.heroku.com/";
    //private string bUrl = "https://beta.habitrpg.com/api/v2";
	//private string bUrl = "https://www.habitrpg.com/api/v2";
	//public string bUrl = "http://fragmental.no-ip.org:3000/api/v2";
	// Temporary testing URL; should be switched back to default URL before 
	// merging this branch!
	private string bUrl = "http://192.168.151.162:3000/api/v2";
	public static string url ;
	private string aUrl ;
	private string cUrl;
    //public static string user = "";
    public static string uid = "";
    //public static string key = "";
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

	// Configuration settings for Socket Policy Server; these take effect only
	// when the build platform is set to Web Player.
	//
	// Temporary testing host name; this remote host name should be changed to 
	// the default base host name of bUrl before merging this branch
	private string policy_server_remote_host_name = "virgo.local";
	
	// The port number **must** not require root-privileges, in order to be 
	// used by Heroku deployments of HabitRPG; anything not in use that is above
	// 1024 should be OK.
	private int policy_server_port = 8433;
	private int policy_server_timeout = 3000;

    void Start()
    {
		// Build platform must be Web Player for the socket policy server 
		// configuration to be used
		#if UNITY_WEBPLAYER
			bool policy_server_connected = false;
			IPHostEntry remote_addr;

			// Try resolving a given host name to an IP address; this must be
			// done because:
			//
			// a) Security.PrefetchSocketPolicy method requires an IP address,
			// so the burden of resolving a host name to an IP address is
			// squarely on us.
			//
			// b) a web host does not necessarily reside at a static location
			// (IP address), such as is the case with Heroku deployed apps
			// (i.e.: www.habitrpg.com), nor may the location exist solely at
			// one address (one host name can resolve to several IP addresses).
			try {
				remote_addr = Dns.GetHostEntry(policy_server_remote_host_name);

				IPAddress[] addr = remote_addr.AddressList;

				// Try connecting to the Socket Policy Server at each of the
				// resolved addresses until we find a successful connection
				for(int i = 0; i < addr.Length; ++i) {
					
					/* Additional debug info
					Debug.Log(	"Host name " +
								policy_server_remote_host_name +
								"resolves to " +
								addr[i].ToString() );
					*/

					policy_server_connected =
						Security.PrefetchSocketPolicy(	addr[i].ToString(),
														policy_server_port);

					// Not a successful connection
					if( policy_server_connected == false ) {
						Debug.Log(	"Failed to connect to Policy Server at " +
									addr[i].ToString() +
									":" + policy_server_port);
					}
					else {
						Debug.Log(	"Connected to Policy Server at " +
									addr[i].ToString() +
									":" +
									policy_server_port);
						// Mission success; onwards, progress, or so it seems!
						break;
					}
				} // end for addr loop
			}
			catch {
				// TODO: Handle err gracefully -- abort program execution?
				Debug.Log(	"Failed to resolve DNS host name: " +
							policy_server_remote_host_name);
			}
		#endif // UNITY_WEBPLAYER defined

		//moved down here, because unity started to inexplicably whine.  Might have been my fault for for mistyping a variable name :/ -sjm
		 url = bUrl+ "/user";
		 aUrl = bUrl + "/user/auth/local";
		 cUrl = bUrl + "/status";
		///moved
		Debug.Log ("aUrl is = " + aUrl);

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

#if UseUniweb
        request.headers.Set("Content-Type", "application/json");
#else
		//unityhttp
		request.SetHeader("Content-Type", "application/json");
#endif
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

#if UseUniweb
			Debug.Log(response.headers.Get("Content-Type"));
#else
			//unityhttp
			Debug.Log (response.GetHeaders("Content-Type"));
#endif

            //Get the body as a byte array
            //Debug.Log(response.bytes);
            //Or as a string
            //Debug.Log(response.Text);
            //string authResponse = response.Text;
            //Type t = authResponse.GetType();

#if UseUniweb
			Hashtable authResponse = JsonSerializer.Decode(response.Text) as Hashtable;
#else
			//unityhttp
			Hashtable authResponse = JSON.JsonDecode(response.Text) as Hashtable;
#endif

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
		var request = new HTTP.Request("GET", cUrl);
		
		
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

#if UseUniweb
			Debug.Log(response.headers.Get("Content-Type"));
#else
			//unityhttp
			Debug.Log (response.GetHeaders("Content-Type"));
#endif
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

		#if UseUniweb
		request.headers.Set("x-api-key", key);
       	request.headers.Set("x-api-user", uid);
		#else
		request.SetHeader("x-api-key", key);
		request.SetHeader("x-api-user", uid);
		#endif

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


#if UseUniweb
            Debug.Log(response.headers.Get("Content-Type"));
#else
			//unityhttp
			Debug.Log(response.GetHeaders("Content-Type"));
#endif

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