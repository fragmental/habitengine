/*using System;

//namespace AssemblyCSharp
//{
public class hdataconvertfromjava :MonoBehavior
{
	private const string HABIT_SAV = "habit.sav";

	public static string getTextFromTask(JSONObject o)
	{
		try
		{
			return o.getString("text");
		}
		catch (JSONException e)
		{
			Console.WriteLine(e.ToString());
			Console.Write(e.StackTrace);
			return null;
		}
	}
	
	internal JSONObject root;

	//	JSONArray data_dailies;
	//	JSONArray data_habits;
	//	JSONArray data_rewards;
	//	JSONArray data_todos;
	//	JSONArray data_todos_done;

	public virtual bool applyServerResultToData(JSONObject o, string taskID, bool upOrCompleted)
	{
		lock (this)
		{
			try
			{
    
				root.getJSONObject("stats").put("gp", o.getDouble("gp"));
				root.getJSONObject("stats").put("hp", o.getDouble("hp"));
				root.getJSONObject("stats").put("lvl", o.getDouble("lvl"));
				root.getJSONObject("stats").put("exp", o.getDouble("exp"));
    
				JSONObject task = getTask(taskID);
				JSONObject tasks = root.getJSONObject("tasks");
				task.put("value", task.getDouble("value") + o.getDouble("delta"));
    
				string taskType = task.getString("type");
    
				if (taskType.Equals("habit"))
				{
					// nothing to do here
				}
				if (taskType.Equals("reward"))
				{
					// nothing to do here
				}
				if (taskType.Equals("daily"))
				{
					task.put("completed", upOrCompleted);
				}
				if (taskType.Equals("todo"))
				{
					task.put("completed", upOrCompleted);
				}
    
				tasks.put(task.getString("id"), task);
				root.put("tasks", tasks);
    
				// unused: o.getDouble("delta");
				return true;
			}
			catch (JSONException e)
			{
				Console.WriteLine(e.ToString());
				Console.Write(e.StackTrace);
			}
    
			return false;
		}
	}
	
	public virtual string ArmorSet
	{
		get
		{
				try
				{
					return root.getJSONObject("preferences").getString("armorSet");
	    
				}
				catch (JSONException e)
				{
					Console.WriteLine(e.ToString());
					Console.Write(e.StackTrace);
				}
	    
				return "v1";
		}
	}

	public virtual JSONArray Dailies
	{
		get
		{
			return getTasksByIdField("dailyIds");
		}
	}

	public virtual JSONArray DailyIds
	{
		get
		{
			try
			{
				return root.getJSONArray("dailyIds");
			}
			catch (JSONException e)
			{
				Console.WriteLine(e.ToString());
				Console.Write(e.StackTrace);
				return null;
			}
		}
	}

	public virtual double XP
	{
		get
		{
			try
			{
				return root.getJSONObject("stats").getDouble("exp");
			}
			catch (JSONException e)
			{
				Console.WriteLine(e.ToString());
				Console.Write(e.StackTrace);
				return 0;
			}
		}
	}
	public virtual double GP
{
	get
	{
			try
			{
				return root.getJSONObject("stats").getDouble("gp");
			}
			catch (JSONException e)
			{
				Console.WriteLine(e.ToString());
				Console.Write(e.StackTrace);
				return 0;
			}
	}
}

	public virtual JSONArray HabitIds
	{
		get
		{
			try
			{
				return root.getJSONArray("habitIds");
			}
			catch (JSONException e)
			{
				Console.WriteLine(e.ToString());
				Console.Write(e.StackTrace);
				return null;
			}
		}
	}

	public virtual JSONArray Habits
	{
		get
		{
			return getTasksByIdField("habitIds");
		}
	}

	public virtual JSONArray getTasksByIdField(string idField)
	{

		try
		{
			JSONArray data_habits = new JSONArray();
			JSONObject tasks = root.getJSONObject("tasks");
			JSONArray ids = root.getJSONArray(idField);
			for (int i = 0; i < ids.length(); i++)
			{
				data_habits.put(tasks.getJSONObject(ids.getString(i)));
			}
			return data_habits;
		}
		catch (JSONException e)
		{
			Console.WriteLine(e.ToString());
			Console.Write(e.StackTrace);
		}

		return null;
	}

	public virtual string Hair
	{
		get
		{
			try
			{
				return root.getJSONObject("preferences").getString("hair");
			}
			catch (JSONException e)
			{
				Console.WriteLine(e.ToString());
				Console.Write(e.StackTrace);
			}
    
			return "blond";
		}
	}

	public virtual int Head
	{
		get
		{
			try
			{
				if (root != null)
				{
					return root.getJSONObject("items").getInt("head");
				}
			}
			catch (JSONException e)
			{
				Console.WriteLine(e.ToString());
				Console.Write(e.StackTrace);
			}
			return 0;
    
		}
	}

	public virtual double HP
	{
		get
		{
			try
			{
				return root.getJSONObject("stats").getDouble("hp");
			}
			catch (JSONException e)
			{
				Console.WriteLine(e.ToString());
				Console.Write(e.StackTrace);
				return 0;
			}
		}
	}

	public virtual double Level
	{
		get
		{
			try
			{
				return root.getJSONObject("stats").getDouble("lvl");
			}
			catch (JSONException e)
			{
				Console.WriteLine(e.ToString());
				Console.Write(e.StackTrace);
				return 0;
			}
		}
	}

	public virtual double MaxHealth
	{
		get
		{
			try
			{
				return root.getJSONObject("stats").getDouble("maxHealth");
			}
			catch (JSONException e)
			{
				Console.WriteLine(e.ToString());
				Console.Write(e.StackTrace);
				return 0;
			}
		}
	}

	public virtual string PartyID
	{
		get
		{
			try
			{
				return root.getJSONObject("party").getString("current");
			}
			catch (JSONException e)
			{
				Console.WriteLine(e.ToString());
				Console.Write(e.StackTrace);
				return null;
			}
		}
	}

	public virtual string PartyInvitation
	{
		get
		{
			try
			{
				return root.getJSONObject("party").getString("invitation");
			}
			catch (JSONException e)
			{
				Console.WriteLine(e.ToString());
				Console.Write(e.StackTrace);
				return null;
			}
		}
	}
	
	
	public virtual JSONArray Rewards
	{
		get
		{
				return getTasksByIdField("rewardIds");
		}
	}

	public virtual int Shield
	{
		get
		{
			try
			{
				if (root != null)
				{
					return root.getJSONObject("items").getInt("shield");
				}
			}
			catch (JSONException e)
			{
				Console.WriteLine(e.ToString());
				Console.Write(e.StackTrace);
			}
			return 0;
    
		}
	}

	public virtual string Skin
	{
		get
		{
			try
			{
				root.getJSONObject("preferences").getString("skin");
			}
			catch (JSONException e)
			{
				Console.WriteLine(e.ToString());
				Console.Write(e.StackTrace);
				return "white";
			}
    
			return "white";
		}
	}

	public virtual JSONObject getTask(string taskID)
	{
		try
		{
			JSONObject tasks = root.getJSONObject("tasks");
			return tasks.getJSONObject(taskID);
		}
		catch (JSONException e)
		{
			Console.WriteLine(e.ToString());
			Console.Write(e.StackTrace);
		}
		return null;
	}

	public virtual JSONArray TodoIds
	{
		get
		{
			try
			{
				return root.getJSONArray("todoIds");
			}
			catch (JSONException e)
			{
				Console.WriteLine(e.ToString());
				Console.Write(e.StackTrace);
				return null;
			}
		}
	}

	public virtual JSONArray Todos
	{
		get
		{
			return getTasksByIdField("todoIds");
		}
	}
	
	public virtual double ToNextLevel
	{
		get
		{
				try
				{
					return root.getJSONObject("stats").getDouble("toNextLevel");
				}
				catch (JSONException e)
				{
					Console.WriteLine(e.ToString());
					Console.Write(e.StackTrace);
					return 0;
				}
		}
	}

	public virtual string UserMail
	{
		get
		{
			try
			{
				return root.getJSONObject("auth").getJSONObject("local").getString("email");
			}
			catch (JSONException e)
			{
				Console.WriteLine(e.ToString());
				Console.Write(e.StackTrace);
				return null;
			}
		}
	}

	public virtual string UserName
	{
		get
		{
			try
			{
				return root.getJSONObject("auth").getJSONObject("local").getString("username");
			}
			catch (JSONException e)
			{
				Console.WriteLine(e.ToString());
				Console.Write(e.StackTrace);
				return null;
			}
		}
	}

	public virtual int Weapon
	{
		get
		{
			try
			{
				if (root != null)
				{
					return root.getJSONObject("items").getInt("weapon");
				}
			}
			catch (JSONException e)
			{
				Console.WriteLine(e.ToString());
				Console.Write(e.StackTrace);
			}
			return 0;
    
		}
	}

	public virtual bool hasPartyInvitation()
	{
		return PartyInvitation != null;
	}

	public virtual bool Male
	{
		get
		{
			try
			{
				return root.getJSONObject("preferences").get("gender").Equals("m");
			}
			catch (JSONException e)
			{
				Console.WriteLine(e.ToString());
				Console.Write(e.StackTrace);
			}
    
			return true;
		}
	}
	
	public virtual bool PartyLeader
	{
		get
		{
				try
				{
					return root.getJSONObject("party").getBoolean("leader");
				}
				catch (JSONException e)
				{
					Console.WriteLine(e.ToString());
					Console.Write(e.StackTrace);
					return false;
				}
		}
	}

	public virtual bool loadLocalData(Context ctx)
	{
		JSONObject data = null;
		try
		{
			FileInputStream fin = ctx.openFileInput(HABIT_SAV);
			string dataJSON = HabitConnectionV1.getStringFromInputStream(fin);
			if (dataJSON == null)
			{
				return false;
			}
			data = new JSONObject(dataJSON);
			return setupData(data);
		}
		catch (FileNotFoundException e)
		{
			Console.WriteLine(e.ToString());
			Console.Write(e.StackTrace);
		}
		catch (JSONException e)
		{
			Console.WriteLine(e.ToString());
			Console.Write(e.StackTrace);
		}
		return false;
	}
	
/// <summary>
/// This function is used to extract the different task types from the
/// fetchData chunk.
/// </summary>
/// <param name="data"> </param>
/// <returns> true if successful </returns>
	public virtual bool setupData(JSONObject data)
	{

		try
		{
			root = data;

			JSONObject tasks = data.getJSONObject("tasks");

			return true;
		}
		catch (JSONException e)
		{
			Console.WriteLine(e.ToString());
			Console.Write(e.StackTrace);
		}
		return false;
	}

	public virtual bool showHelm()
	{
		try
		{
			return root.getJSONObject("preferences").getBoolean("showHelm");
		}
		catch (JSONException e)
		{
			Console.WriteLine(e.ToString());
			Console.Write(e.StackTrace);
			return true;
		}
	}

	public virtual bool storeLocalData(Context ctx)
	{
		if (root == null)
		{
			return false;
		}

		string dataJSON = root.ToString();
		try
		{
			FileOutputStream fout = ctx.openFileOutput(HABIT_SAV, Context.MODE_PRIVATE);
			fout.write(dataJSON.GetBytes());
		}
		catch (FileNotFoundException e)
		{
			Console.WriteLine(e.ToString());
			Console.Write(e.StackTrace);
		}
		catch (IOException e)
		{
			Console.WriteLine(e.ToString());
			Console.Write(e.StackTrace);
		}

		return false;
	}
	
	public virtual bool clobberTasks(JSONArray source)
	{
			JSONObject tasks;
			try
			{
				tasks = root.getJSONObject("tasks");
				fillJSONOject(tasks, source);
				root.put("tasks", tasks);
				return true;
			}
			catch (JSONException e)
			{
				Console.WriteLine(e.ToString());
				Console.Write(e.StackTrace);
			}
			return false;
	}

	public virtual void fillJSONOject(JSONObject target, JSONArray source)
	{
		for (int i = 0; i < source.length(); i++)
		{
			JSONObject item;
			try
			{
				item = source.getJSONObject(i);
				target.put(item.getString("id"), item);
			}
			catch (JSONException e)
			{
				Console.WriteLine(e.ToString());
				Console.Write(e.StackTrace);
				continue;
			}

		}

	}

	public virtual bool rootIsNull()
	{
		return root == null;
	}

	public virtual long CronHour
	{
		get
		{
			if (root == null)
			{
				return 0;
			}
    
			try
			{
				return root.getJSONObject("preferences").getInt("dayStart");
			}
			catch (JSONException e)
			{
				Console.WriteLine(e.ToString());
				Console.Write(e.StackTrace);
			}
    
			return 0;
		}
	}
	
	
/*	
//-------------------------------------------------------------------------------------------
//	Copyright © 2007 - 2013 Tangible Software Solutions Inc.
//	This class can be used by anyone provided that the copyright notice remains intact.
//
//	This class is used to convert some aspects of the Java String class.
//-------------------------------------------------------------------------------------------
internal static class StringHelperClass
{
	//------------------------------------------------------------------------------------
	//	This method is used to replace calls to the 2-arg Java String.startsWith method.
	//------------------------------------------------------------------------------------
	internal static bool StartsWith(this string self, string prefix, int toffset)
	{
		return self.IndexOf(prefix, toffset) == toffset;
	}

	//------------------------------------------------------------------------------
	//	This method is used to replace most calls to the Java String.split method.
	//------------------------------------------------------------------------------
	internal static string[] Split(this string self, string regexDelimiter, bool trimTrailingEmptyStrings)
	{
		string[] splitArray = System.Text.RegularExpressions.Regex.Split(self, regexDelimiter);

		if (trimTrailingEmptyStrings)
		{
			if (splitArray.Length > 1)
			{
				for (int i = splitArray.Length; i > 0; i--)
				{
					if (splitArray[i - 1].Length > 0)
					{
						if (i < splitArray.Length)
							System.Array.Resize(ref splitArray, i);

						break;
					}
				}
			}
		}

		return splitArray;
	}

	//-----------------------------------------------------------------------------
	//	These methods are used to replace calls to some Java String constructors.
	//-----------------------------------------------------------------------------
	internal static string NewString(sbyte[] bytes)
	{
		return NewString(bytes, 0, bytes.Length);
	}
	internal static string NewString(sbyte[] bytes, int index, int count)
	{
		return System.Text.Encoding.UTF8.GetString((byte[])(object)bytes, index, count);
	}
	internal static string NewString(sbyte[] bytes, string encoding)
	{
		return NewString(bytes, 0, bytes.Length, encoding);
	}
	internal static string NewString(sbyte[] bytes, int index, int count, string encoding)
	{
		return System.Text.Encoding.GetEncoding(encoding).GetString((byte[])(object)bytes, index, count);
	}

	//--------------------------------------------------------------------------------
	//	These methods are used to replace calls to the Java String.getBytes methods.
	//--------------------------------------------------------------------------------
	internal static sbyte[] GetBytes(this string self)
	{
		return GetSBytesForEncoding(System.Text.Encoding.UTF8, self);
	}
	internal static sbyte[] GetBytes(this string self, string encoding)
	{
		return GetSBytesForEncoding(System.Text.Encoding.GetEncoding(encoding), self);
	}
	private static sbyte[] GetSBytesForEncoding(System.Text.Encoding encoding, string s)
	{
		sbyte[] sbytes = new sbyte[encoding.GetByteCount(s)];
		encoding.GetBytes(s, 0, s.Length, (byte[])(object)sbytes, 0);
		return sbytes;
	}
}
*/
//}
//}

