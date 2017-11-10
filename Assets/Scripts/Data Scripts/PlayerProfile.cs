using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

[Serializable]
public class PlayerProfile
{
	public string profileName;
	public int levelsComplete = 1;
	/// <summary>
	/// How many times has the player of this profile failed to win within time limits?
	/// </summary>
	public int timeUps;
	public PlayerProfile(string profileName)
	{
		this.profileName = profileName;
	}
}

