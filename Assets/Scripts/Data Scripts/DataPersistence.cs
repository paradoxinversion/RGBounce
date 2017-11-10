using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
public static class DataPersistence 
{

	static SaveData saveData;
	static LevelSaveData baseLevels;
	static LevelSaveData customLevels;

	public static bool savedDataFound;
	const string SAVESTRING = "/SaveGame.txt";
	private static string baseLevelSaveString = "/Base.rgblevels";
	private static string userLevelSaveString = "/Custom.rgblevels";
	static void GatherSaveData()
	{
		if (saveData == null)
		{
			saveData = new SaveData();
		}


		GatherLevelData();
	}

	static void GatherLevelData()
	{
		if (baseLevels == null)
		{
			baseLevels = new LevelSaveData();
		}
		baseLevels.levels =  LevelManager.GameLevels;
	}
	static void RestoreSavedData()
	{
		if (saveData != null)
		{
			PlayerProfileManager.playerProfiles = saveData.playerProfiles;
		}
	}

	static void RestoreLevelData()
	{
		LevelManager.SetGameLevels(baseLevels.levels);

	}
	public static void SaveGameData()
	{
		
		GatherSaveData();
		IFormatter formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
		System.IO.Stream stream = new System.IO.FileStream(Application.dataPath + SAVESTRING, System.IO.FileMode.Create, System.IO.FileAccess.Write, System.IO.FileShare.None);
		formatter.Serialize(stream, saveData);
		stream.Close();
	}

	public static void LoadGameData()
	{
		if (IsSaveDataFound()){
			saveData = new SaveData();
			IFormatter formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
			System.IO.Stream stream = new System.IO.FileStream(Application.dataPath + SAVESTRING, System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.Read);
			saveData = (SaveData)formatter.Deserialize(stream);
			stream.Close();
			RestoreSavedData();
		}

	}
	public static void SaveLevelData()
	{
		GatherLevelData();
		IFormatter formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
		System.IO.Stream stream = new System.IO.FileStream(Application.dataPath + baseLevelSaveString, System.IO.FileMode.Create, System.IO.FileAccess.Write, System.IO.FileShare.None);
		formatter.Serialize(stream, baseLevels);
		stream.Close();
	}

	public static List<LevelData> LoadLevelData()
	{
		if (IsLevelDataFound()){
			IFormatter formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
			System.IO.Stream stream = new System.IO.FileStream(Application.dataPath + baseLevelSaveString, System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.Read);
			baseLevels = (LevelSaveData)formatter.Deserialize(stream);
			stream.Close();

			return baseLevels.levels;
		}
		return new List<LevelData> ();

	}
	public static void DeleteSaveDataFile()
	{
		if (System.IO.File.Exists(Application.dataPath + SAVESTRING))
		{
			System.IO.File.Delete(Application.dataPath + SAVESTRING);
			savedDataFound = false;
		}
	}

	public static bool IsSaveDataFound()
	{
		if (System.IO.File.Exists(Application.dataPath + SAVESTRING))
			return true;
		return false;
	}
	public static bool IsLevelDataFound()
	{
		if (System.IO.File.Exists(Application.dataPath + baseLevelSaveString))
			return true;
		return false;
	}
}

[System.Serializable]
public class SaveData
{
	public List<PlayerProfile> playerProfiles;
}

[System.Serializable]
public class LevelSaveData
{
	public List<LevelData> levels;

}
