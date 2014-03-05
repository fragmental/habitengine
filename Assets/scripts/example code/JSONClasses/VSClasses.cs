using UnityEngine;
using System.Collections;
using System;

public class VSClasses {

	/*// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
      */
    
public class Rootobject
{
public int __v { get; set; }
public string _id { get; set; }
public int balance { get; set; }
public Reward[] rewards { get; set; }
public Todo1[] todos { get; set; }
public Daily[] dailys { get; set; }
public Habit[] habits { get; set; }
public string[] challenges { get; set; }
public Tag[] tags { get; set; }
public Stats stats { get; set; }
public Profile profile { get; set; }
public Preferences preferences { get; set; }
public Party party { get; set; }
public DateTime lastCron { get; set; }
public Items items { get; set; }
public Invitations invitations { get; set; }
public History history { get; set; }
public Flags flags { get; set; }
public Purchased purchased { get; set; }
public Filters filters { get; set; }
public Contributor contributor { get; set; }
public Backer backer { get; set; }
public Auth auth { get; set; }
public Achievements achievements { get; set; }
public int _v { get; set; }
public string id { get; set; }
}

public class Stats
{
public int exp { get; set; }
public float gp { get; set; }
public float hp { get; set; }
public int lvl { get; set; }
public int toNextLevel { get; set; }
public int maxHealth { get; set; }
}

public class Profile
{
public string name { get; set; }
}

public class Preferences
{
public string armorSet { get; set; }
public string language { get; set; }
public int timezoneOffset { get; set; }
public string skin { get; set; }
public bool showArmor { get; set; }
public bool showShield { get; set; }
public bool showWeapon { get; set; }
public bool showHelm { get; set; }
public bool hideHeader { get; set; }
public string hair { get; set; }
public string gender { get; set; }
public int dayStart { get; set; }
}

public class Party
{
}

public class Items
{
public int armor { get; set; }
public string currentMount { get; set; }
public string currentPet { get; set; }
public int head { get; set; }
public int shield { get; set; }
public int weapon { get; set; }
public Lastdrop lastDrop { get; set; }
public Mounts mounts { get; set; }
public Food food { get; set; }
public Hatchingpotions hatchingPotions { get; set; }
public Eggs eggs { get; set; }
public Pets pets { get; set; }
}

public class Lastdrop
{
public int count { get; set; }
public DateTime date { get; set; }
}

public class Mounts
{
}

public class Food
{
}

public class Hatchingpotions
{
}

public class Eggs
{
public int Wolf { get; set; }
}

public class Pets
{
public int WolfVeteran { get; set; }
}

public class Invitations
{
public object party { get; set; }
public object[] guilds { get; set; }
}

public class History
{
public Todo[] todos { get; set; }
public Exp[] exp { get; set; }
}

public class Todo
{
public float value { get; set; }
public object date { get; set; }
public string _id { get; set; }
}

public class Exp
{
public float value { get; set; }
public object date { get; set; }
}

public class Flags
{
public bool partyEnabled { get; set; }
public bool rest { get; set; }
public bool rewrite { get; set; }
public bool newStuff { get; set; }
public bool itemsEnabled { get; set; }
public bool dropsEnabled { get; set; }
public bool showTour { get; set; }
public bool customizationsNotification { get; set; }
}

public class Purchased
{
public Hair hair { get; set; }
public Skin skin { get; set; }
public bool ads { get; set; }
}

public class Hair
{
}

public class Skin
{
}

public class Filters
{
}

public class Contributor
{
}

public class Backer
{
}

public class Auth
{
public Timestamps timestamps { get; set; }
public Local local { get; set; }
}

public class Timestamps
{
public DateTime loggedin { get; set; }
public DateTime created { get; set; }
}

public class Local
{
public string username { get; set; }
public string email { get; set; }
public string salt { get; set; }
public string hashed_password { get; set; }
}

public class Achievements
{
public bool beastMaster { get; set; }
public bool veteran { get; set; }
public object[] challenges { get; set; }
}

public class Reward
{
public string _id { get; set; }
public string text { get; set; }
public Challenge challenge { get; set; }
public int streak { get; set; }
public Repeat repeat { get; set; }
public string priority { get; set; }
public bool completed { get; set; }
public int value { get; set; }
public bool down { get; set; }
public bool up { get; set; }
public string type { get; set; }
public Tags tags { get; set; }
public string notes { get; set; }
public object[] history { get; set; }
public string id { get; set; }
}

public class Challenge
{
}

public class Repeat
{
public int su { get; set; }
public int s { get; set; }
public int f { get; set; }
public int th { get; set; }
public int w { get; set; }
public int t { get; set; }
public int m { get; set; }
}

public class Tags
{
}

public class Todo1
{
public string _id { get; set; }
public string text { get; set; }
public Challenge1 challenge { get; set; }
public int streak { get; set; }
public Repeat1 repeat { get; set; }
public string priority { get; set; }
public bool completed { get; set; }
public float value { get; set; }
public bool down { get; set; }
public bool up { get; set; }
public string type { get; set; }
public Tags1 tags { get; set; }
public string notes { get; set; }
public object[] history { get; set; }
public string id { get; set; }
}

public class Challenge1
{
}

public class Repeat1
{
public int su { get; set; }
public int s { get; set; }
public int f { get; set; }
public int th { get; set; }
public int w { get; set; }
public int t { get; set; }
public int m { get; set; }
}

public class Tags1
{
}

public class Daily
{
public string _id { get; set; }
public string text { get; set; }
public Challenge2 challenge { get; set; }
public int streak { get; set; }
public Repeat2 repeat { get; set; }
public string priority { get; set; }
public bool completed { get; set; }
public float value { get; set; }
public bool down { get; set; }
public bool up { get; set; }
public string type { get; set; }
public Tags2 tags { get; set; }
public string notes { get; set; }
public History1[] history { get; set; }
public string id { get; set; }
}

public class Challenge2
{
public string id { get; set; }
}

public class Repeat2
{
public bool m { get; set; }
public bool t { get; set; }
public bool w { get; set; }
public bool th { get; set; }
public bool f { get; set; }
public bool s { get; set; }
public bool su { get; set; }
}

public class Tags2
{
public bool f7f397b0279c45eaa5efd715d94dbd53 { get; set; }
}

public class History1
{
public float value { get; set; }
public object date { get; set; }
}

public class Habit
{
public string _id { get; set; }
public string text { get; set; }
public Challenge3 challenge { get; set; }
public int streak { get; set; }
public Repeat3 repeat { get; set; }
public string priority { get; set; }
public bool completed { get; set; }
public float value { get; set; }
public bool down { get; set; }
public bool up { get; set; }
public string type { get; set; }
public Tags3 tags { get; set; }
public string notes { get; set; }
public History2[] history { get; set; }
public string id { get; set; }
}

public class Challenge3
{
}

public class Repeat3
{
public int su { get; set; }
public int s { get; set; }
public int f { get; set; }
public int th { get; set; }
public int w { get; set; }
public int t { get; set; }
public int m { get; set; }
}

public class Tags3
{
}

public class History2
{
public float value { get; set; }
public object date { get; set; }
}

public class Tag
{
public string name { get; set; }
public string id { get; set; }
public string challenge { get; set; }
public string _id { get; set; }
}

}
    