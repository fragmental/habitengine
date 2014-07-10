﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;
using System;

public class Avatar : MonoBehaviour {
	private HabitDatav1 userData;


	//public Texture2D skinTest;
	//public Sprite spriteSkin;
	public SpriteRenderer avatarHair;
	public SpriteRenderer avatarSkin;

	public SpriteRenderer avatarArmor;
	public SpriteRenderer avatarHead;
	public SpriteRenderer avatarWeapon;
	public SpriteRenderer avatarShield;

	public SpriteRenderer avatarPet;
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
		if (hBangs == 0) 
		{
			hBangs = 3;
		}
		avatarHair.sprite = Resources.LoadAssetAtPath<Sprite> ("Assets/Textures/spritesmith/customize/hair/hair_bangs_"+hBangs+"_"+hColor+".png");
		Debug.Log ("Assets/Textures/spritesmith/customize/hair/hair_bangs_" + hBangs + "_" + hColor + ".png");


		avatarSkin.sprite = Resources.LoadAssetAtPath<Sprite> ("Assets/Textures/spritesmith/customize/skin/skin_"+skin+".png");

		//weapon
		//string sWeapon = cWeapon;
		if (cWeapon == "shield_base_0") 
		{
			avatarWeapon.enabled = false;
		}

		else
		{
			avatarWeapon.sprite = Resources.LoadAssetAtPath<Sprite> ("Assets/Textures/spritesmith/gear/weapon/"+cWeapon+".png");
		}
		/* doesn't work
		 * else if(sWeapon.Contains ("special"))
		{
			avatarWeapon.sprite = Resources.LoadAssetAtPath<Sprite> ("Assets/Textures/spritesmith/gear/event/"+cWeapon+".png");
		}*/
		//head
		if (cHead == "head_base_0") 
		{
			cHead = "head_0";
		}
		avatarHead.sprite = Resources.LoadAssetAtPath<Sprite> ("Assets/Textures/spritesmith/gear/head/"+cHead+".png");

		//armor
		if (cArmor == "armor_base_0") 
		{
			avatarArmor.sprite = Resources.LoadAssetAtPath<Sprite> ("Assets/Textures/spritesmith/customize/shirt/" + size + "_shirt_" + shirt + ".png");
		}
		else
		{
			avatarArmor.sprite = Resources.LoadAssetAtPath<Sprite> ("Assets/Textures/spritesmith/gear/armor/" + size + "_" + cArmor + ".png");
		}

		//shield
		if (cShield == "shield_base_0")
		{
			avatarShield.enabled = false;
		}
		else
		{
			avatarShield.sprite = Resources.LoadAssetAtPath<Sprite> ("Assets/Textures/spritesmith/gear/shield/"+cShield+".png");
		}

		//pet
		avatarPet.sprite = Resources.LoadAssetAtPath<Sprite> ("Assets/Textures/spritesmith/stable/pets/Pet-"+pet+".png");
		Debug.Log ("Assets/Textures/spritesmith/stable/pets/Pet-" + pet + ".png");
		//avatarMount.sprite = Resources.LoadAssetAtPath<Sprite> ("Assets/Textures/spritesmith/skin/skin_"+skin+".png");


	 
		
	}
	

}
