using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;

public class SpriteTest : MonoBehaviour {
	private HabitDatav1 userData;
	public JSONArray dailies;
	public List<Sprite> avatarSkin;
	public List<Sprite> avatarHair;
	public List<Sprite> avatarSize;
	public List<Sprite> avatarShirt;
	public List<Sprite> avatarShield;
	public List<Sprite> avatarHead;
	public List<Sprite> avatarArmor;
	public List<Sprite> avatarWeapon;
	//use type Sprite or Texture2d?  Methinks Sprite
	public Texture2D skinTest;
	public Sprite spriteSkin;
	public SpriteRenderer artwork;




	void Start () {
		artwork.sprite = spriteSkin;
		//artwork = GetComponent<blah>();
		userData = new HabitDatav1(PlayerPrefs.GetString("jsonSave"));
		JSONArray dailies = userData.Dailies.AsArray;

		//Debug.Log ("first completed object is = " + dailies ["tasks"] ["dailys"] ["checklist"] [0] ["completed"].AsBool);
		//preferences assignation
		var preferences = userData.Preferences;
		var hair = preferences["hair"];
		var size = preferences["size"];
		var shirt = preferences["shirt"];
		var skin = preferences["skin"];
		
		var mustache = hair["mustache"];
		var color = hair["color"];
		Debug.Log ("size is "+size);
		Debug.Log ("shirt is "+shirt);
		Debug.Log ("skin is "+skin);
		Debug.Log ("mustache is "+mustache);
		Debug.Log ("nested color is"+preferences["hair"]["color"]);

		//Debug.Log ();
		
		//gear assignation
		var items = userData.Items;
		var cShield = items["gear"]["costume"]["shield"];
		Debug.Log (cShield);
		var cHead =	items["gear"]["costume"]["head"];
		Debug.Log (cHead);
		var cArmor = items["gear"]["costume"]["armor"];
		Debug.Log (cArmor);
		var cWeapon = items["gear"]["costume"]["weapon"];
		Debug.Log (cWeapon);
		var eShield = items["gear"]["equipped"]["shield"];
		Debug.Log (eShield);
		var eHead =	items["gear"]["equipped"]["head"];
		Debug.Log (eHead);
		var eArmor = items["gear"]["equipped"]["armor"];
		Debug.Log (eArmor);
		var eWeapon = items["gear"]["equipped"]["weapon"];
		Debug.Log (eWeapon);
		var owned = items["gear"]["owned"]["weapon_warrior_0"];
		Debug.Log (owned);
		
	}

	void OnGUI()
	{
		//GUITexture
		GUI.DrawTexture (new Rect(10, 9, 256, 256), skinTest, ScaleMode.ScaleToFit, true);
		//SpriteRenderer ();
		//GUI.DrawTexture (new Rect(10, 9, 90, 90), spriteSkin, ScaleMode.StretchToFill, true, 2.5F);
		//Sprite spriteSkin;
	}
	
	
}
