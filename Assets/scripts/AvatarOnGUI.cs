using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;
using System;

public class AvatarOnGUI : MonoBehaviour {
	private HabitDatav1 userData;


	//public Texture2D skinTest;
	//public Sprite spriteSkin;
	public Texture2D avatarHair;
	public Texture2D avatarSkin;

	public Texture2D avatarArmor;
	public Texture2D avatarHead;
	public Texture2D avatarWeapon;
	public Texture2D avatarShield;

	public Texture2D avatarPet;
	public SpriteRenderer avatarMount;
	/*preferences:
	 * timezoneOffset int
	 * toolbarCollapsed bool
	 * advancedCollapsed bool
	 * tagsCollapsed bool
	 * newTaskEdit bool
	 * disableClasses bool
	 * stickyHeader bool
	 * sleep bool
	 * allocationMode string
	 * shirt string
	 * skin string
	 * hideHeader bool
	 * hair obj
	 * 	mustache int
	 * 	beard int
	 * 	bangs int
	 * 	base int
	 * 	color string
	 * size string
	 * daystart int
	 */
	public float left = 10;
	public float top =9;
	public float width =256;
	public float height = 256;

	void Start () {

		userData = new HabitDatav1(PlayerPrefs.GetString("jsonSave"));



		//preferences assignation
		var preferences = userData.Preferences;

		var size = preferences["size"];
		Debug.Log ("size is "+size);
		var shirt = preferences["shirt"];
		Debug.Log ("shirt is "+shirt);
		var skin = preferences["skin"];
		Debug.Log ("skin is "+skin);
		//hair
		var hMustache = preferences["hair"]["mustache"];
		Debug.Log ("hMustache is "+hMustache);
		var hColor = preferences["hair"]["color"];
		Debug.Log ("hColor is "+hColor);
		var hBangs = preferences["hair"]["bangs"].AsInt;
		Debug.Log ("hBangs is "+hBangs);
		
				
		//gear assignation
		var items = userData.Items;
		var cShield = items["gear"]["costume"]["shield"];
		Debug.Log ("cShield is "+cShield);
		var cHead =	items["gear"]["costume"]["head"];
		Debug.Log ("cHead is "+cHead);
		var cArmor = items["gear"]["costume"]["armor"];
		Debug.Log ("cArmor is "+cArmor);
		var cWeapon = items["gear"]["costume"]["weapon"];
		Debug.Log ("cWeapon is "+cWeapon);
		var eShield = items["gear"]["equipped"]["shield"];
		Debug.Log ("eShield is "+eShield);
		var eHead =	items["gear"]["equipped"]["head"];
		Debug.Log ("eHead is "+eHead);
		var eArmor = items["gear"]["equipped"]["armor"];
		Debug.Log ("eArmor is "+eArmor);
		var eWeapon = items["gear"]["equipped"]["weapon"];
		Debug.Log ("eWeapon is "+eWeapon);
		var owned = items["gear"]["owned"]["weapon_warrior_0"];
		Debug.Log ("owned is "+owned);

		//Pet Assignation
		var pet = items["currentPet"];
		Debug.Log ("currentPet = "+pet); 

		/* ignore this
		 * string fuck = "fuckassshit";
		if(fuck.Contains("fuck"))
		{
			print("damn it");
		}*/


		//hair
		/*if (hBangs == 0) 
		{
			hBangs = 3;
		}*/
		avatarHair = Resources.LoadAssetAtPath<Texture2D> ("Assets/Textures/spritesmith/customize/hair/hair_bangs_"+hBangs+"_"+hColor+".png");
		Debug.Log ("Assets/Textures/spritesmith/customize/hair/hair_bangs_" + hBangs + "_" + hColor + ".png");


		avatarSkin = Resources.LoadAssetAtPath<Texture2D> ("Assets/Textures/spritesmith/customize/skin/skin_"+skin+".png");

		//weapon
		//string sWeapon = cWeapon;
		if (cWeapon == "shield_base_0") 
		{
			//avatarWeapon.enabled = false;
		}

		else
		{
			avatarWeapon = Resources.LoadAssetAtPath<Texture2D> ("Assets/Textures/spritesmith/gear/weapon/"+cWeapon+".png");
		}
		/* doesn't work
		 * else if(sWeapon.Contains ("special"))
		{
			avatarWeapon = Resources.LoadAssetAtPath<Texture2D> ("Assets/Textures/spritesmith/gear/event/"+cWeapon+".png");
		}*/
		//head
		if (cHead == "head_base_0") 
		{
			cHead = "head_0";
		}
		avatarHead = Resources.LoadAssetAtPath<Texture2D> ("Assets/Textures/spritesmith/gear/head/"+cHead+".png");

		//armor
		if (cArmor == "armor_base_0") 
		{
			avatarArmor = Resources.LoadAssetAtPath<Texture2D> ("Assets/Textures/spritesmith/customize/shirt/" + size + "_shirt_" + shirt + ".png");
		}
		else
		{
			avatarArmor = Resources.LoadAssetAtPath<Texture2D> ("Assets/Textures/spritesmith/gear/armor/" + size + "_" + cArmor + ".png");
		}

		//shield
		if (cShield == "shield_base_0")
		{
			//avatarShield.enabled = false;
		}
		else
		{
			avatarShield = Resources.LoadAssetAtPath<Texture2D> ("Assets/Textures/spritesmith/gear/shield/"+cShield+".png");
		}

		//pet
		avatarPet = Resources.LoadAssetAtPath<Texture2D> ("Assets/Textures/spritesmith/stable/pets/Pet-"+pet+".png");
		Debug.Log ("Assets/Textures/spritesmith/stable/pets/Pet-" + pet + ".png");
		//avatarMount = Resources.LoadAssetAtPath<Texture2D> ("Assets/Textures/spritesmith/skin/skin_"+skin+".png");


	 
		
	}

	void OnGUI()
	{
		//GUITexture
		GUI.DrawTexture (new Rect (left, top, width, height), avatarSkin, ScaleMode.ScaleToFit);
		GUI.DrawTexture (new Rect (left, top, width, height), avatarHair, ScaleMode.ScaleToFit);
		GUI.DrawTexture (new Rect (left, top, width, height), avatarArmor, ScaleMode.ScaleToFit);
		GUI.DrawTexture (new Rect (left, top, width, height), avatarWeapon, ScaleMode.ScaleToFit);
		GUI.DrawTexture (new Rect (left, top, width, height), avatarHead, ScaleMode.ScaleToFit);
		GUI.DrawTexture (new Rect (left, top, width, height), avatarShield, ScaleMode.ScaleToFit);
	}
	

}
