using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public static class PlayerProfileManager 
{

	public static List<PlayerProfile> playerProfiles = new List<PlayerProfile>();
	public static PlayerProfile currentProfile = null;
	public static void CreateNewProfile(string profileName)
	{
		PlayerProfile newProfile = new PlayerProfile(profileName);
		playerProfiles.Add(newProfile);
		DataPersistence.SaveGameData();
	}

	public static void DeleteProfile(PlayerProfile profileToDelete)
	{
		playerProfiles.Remove(profileToDelete);
		DataPersistence.SaveGameData();
	}

}

