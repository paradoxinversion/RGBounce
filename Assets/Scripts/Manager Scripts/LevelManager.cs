using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public static class LevelManager
{
	public static List<GameObject> objectPrefabs;
	private static Placeable[] placeables;
	public static Placeable[] Placeables{
		get {
			if (placeables == null || placeables.Length == 0){
				LoadPlaceables ();
			}
			return placeables;
		}
	}
	private static List<LevelData> gameLevels = null;
	public static List<LevelData> GameLevels {
		get {
			if (gameLevels == null){
				gameLevels = DataPersistence.LoadLevelData ();
			}
			return gameLevels;
		}
	}
	public static Level currentLevelGameObject;
	public static LevelData ActiveLevel
	{
		get
		{
			return currentLevelGameObject.levelData;
		}
	}
	public static void LoadPlaceables(){
		List<Placeable> placeableGameObjects = new List<Placeable> ();

		foreach (var placeableGO in GameResources.LoadPlaceables()){
			Placeable placeable;
			if (placeableGO.GetComponent<Placeable>() == null){
				placeable = placeableGO.AddComponent<Placeable> ();
				PlaceableData pleaceableData = new PlaceableData (placeableGO.name);
				placeable.SetData (pleaceableData);
			}else{
				placeable = placeableGO.GetComponent<Placeable> ();
			}

			placeableGameObjects.Add (placeable);
		}
		placeables = placeableGameObjects.ToArray();
	}

	public static void SaveLevelData()
	{
		//save level data out to files?
		DataPersistence.SaveLevelData();
	}

	/// <summary>
	/// Loads serialized level data
	/// </summary>
	public static void LoadLevelData()
	{

		if (gameLevels == null)
		{
			if (DataPersistence.IsLevelDataFound())
			{
				DataPersistence.LoadLevelData();

			}
			else
			{
				gameLevels = new List<LevelData>();
				DataPersistence.SaveLevelData();
			}

		}
	}

	

	/// <summary>
	/// Changes a level's index in the list. Bumps Everything forward
	/// </summary>
	public static void ChangeLevelIndex(LevelData level, int newIndex)
	{
		if (newIndex > gameLevels.Count - 1)
		{
			Debug.LogError("ChangeLevelIndex::New index is out of range.");
			return;
		}

		int oldLevelIndex = gameLevels.IndexOf(level);
		if (oldLevelIndex == newIndex)
		{
			Debug.LogWarning("ChangeLevelIndex::New index for level is the same as its current index.");
			return;
		}

		gameLevels.RemoveAt(oldLevelIndex);
		gameLevels.Insert(newIndex, level);

		DataPersistence.SaveLevelData();
	}
	public static void SetLevel(Level level){
		currentLevelGameObject = level;
	}
	public static GameObject GetPleaceableByID(int id)
	{
		//
		//
		//
		//
		//
		//
		//
		//

		//This needs to be changed, indexes are no longer used in favor of typeStr
		foreach (var placeable in GameObject.FindObjectsOfType<Placeable>().ToList())
		{
			if (placeable.ID == id)
			{
				return placeable.gameObject;
			}
		}
		return null;
	}

	public static void BuildLevel(LevelData levelData){
		DestroyCurrentLevel ();
		GameObject levelGameObject = new GameObject ();
		Level newLevel = levelGameObject.AddComponent<Level> ();

		newLevel.InitializeLevelData (levelData);
		LevelManager.SetLevel(newLevel);
		newLevel.GenerateEdgeColliders ();
		levelGameObject.tag = "level";
		currentLevelGameObject = newLevel;

		foreach (var placeable in ActiveLevel.placeables)
		{
			Placeable placeablePrototype = placeables.FirstOrDefault (p => p.name == placeable.typeStr);
			Placeable placeableGO = GameObject.Instantiate(placeablePrototype, currentLevelGameObject.transform);
			placeableGO.transform.position = placeable.ReturnOriginalPosition ();
//			placeableGOS.Add(placeableGO);
			placeableGO.GetComponent<Placeable>().SetData(placeable);
//			placeableGO.GetComponent<AnimatedObject>().SetData(ActiveLevel.animations[ActiveLevel.placeables.IndexOf(placeableGO.GetComponent<Placeable>().pData)]);
//			if (placeableGO.GetComponent<PassableObstacle>() != null)
//			{
//				placeableGO.GetComponent<PassableObstacle>().SetData(ActiveLevel.passableObjectDataLink[placeable]);
//			}
		}
	}
	public static void AddGameLevel(LevelData levelData){
		if (!gameLevels.Contains(levelData)){
			gameLevels.Add (levelData);
		}else{
			gameLevels[gameLevels.IndexOf (levelData)] = levelData;
		}
	}

	public static void SetGameLevels(List<LevelData> levels){
		gameLevels = levels;
	}

	public static void DestroyCurrentLevel(){
		if (currentLevelGameObject != null){
			GameObject.Destroy(currentLevelGameObject.gameObject);	
		}
	}
}

