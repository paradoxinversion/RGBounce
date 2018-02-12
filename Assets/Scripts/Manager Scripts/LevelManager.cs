using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

/// <summary>
/// Handles Levels and Level Objects
/// </summary>
public static class LevelManager
{
	/// <summary>
	/// A list of all level Placeable prefabs...
	/// MIGHT BE DEPRECATED
	/// </summary>
	public static List<GameObject> objectPrefabs;
	/// <summary>
	/// All Placeable objects for use in the game. Gets its data from LoadPlaceables()
	/// </summary>
	private static Placeable[] placeables;
	public static Placeable[] Placeables{
		get {
			if (placeables == null || placeables.Length == 0){
				LoadPlaceables ();
			}
			return placeables;
		}
	}

	/// <summary>
	/// A list of all saved game levels
	/// </summary>
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
	public static LevelData ActiveLevel {
		get{
			if (currentLevelGameObject != null){
				return currentLevelGameObject.levelData;
			} else {
				return null;
			}

		}
	}

	/// <summary>
	/// Loads Placeables from the resources folder and sets them to the 
	/// </summary>
	public static void LoadPlaceables(){
		if (placeables == null || placeables.Length == 0){
			Debug.Log ("Loading game Placeables");
			List<Placeable> placeableGameObjects = new List<Placeable> ();

			foreach (var placeableGO in GameResources.LoadPlaceables()){
				Placeable placeable;
				if (placeableGO.GetComponent<Placeable>() == null){
					placeable = placeableGO.AddComponent<Placeable> ();
					PlaceableData pleaceableData = new PlaceableData (placeableGO.name);
					placeable.SetData (pleaceableData);
				} else {
					placeable = placeableGO.GetComponent<Placeable> ();
				}
				placeableGameObjects.Add (placeable);
			}
			placeables = placeableGameObjects.ToArray();
		}
	}

	public static void SaveLevelData(){
		//save level data out to files?
		DataPersistence.SaveLevelData();
	}

	/// <summary>
	/// Loads serialized level data
	/// </summary>
	public static void LoadLevelData(){
		if (gameLevels == null){
			if (DataPersistence.IsLevelDataFound()){
				DataPersistence.LoadLevelData();

			}
			else {
				gameLevels = new List<LevelData>();
				DataPersistence.SaveLevelData();
			}
		}
	}

	/// <summary>
	/// Changes a level's index in the list. Bumps Everything forward
	/// </summary>
	public static void ChangeLevelIndex(LevelData level, int newIndex){
		if (newIndex > gameLevels.Count - 1){
			Debug.LogError("ChangeLevelIndex::New index is out of range.");
			return;
		}

		int oldLevelIndex = gameLevels.IndexOf(level);
		if (oldLevelIndex == newIndex){
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

	public static GameObject CreateLevelBase(){
		GameObject levelGameObject = new GameObject ("Base Level");
		levelGameObject.tag = "level";
		Level newLevel = levelGameObject.AddComponent<Level> ();

		GameObject placeableContainer = new GameObject ("Placeable Container");
		placeableContainer.tag = "placeable container";
		placeableContainer.transform.SetParent (levelGameObject.transform);

		newLevel.GenerateEdgeColliders ();
		return levelGameObject;
	}

	public static void BuildLevelPlaceables(Level level){
		Transform placeableContainer = GameObject.FindGameObjectWithTag ("placeable container").transform;
		foreach (var placeable in level.levelData.placeables){
			Placeable placeablePrototype = placeables.FirstOrDefault (p => p.name == placeable.typeStr);
			Placeable placeableGO = GameObject.Instantiate(placeablePrototype, placeableContainer);
			placeableGO.transform.position = placeable.ReturnOriginalPosition ();
			placeableGO.GetComponent<Placeable>().SetData(placeable);
			placeableGO.gameObject.AddComponent<AnimatedObject>().SetData(placeableGO.pData);
		}
	}

	public static IEnumerator CreateLevel(LevelData levelData){
		DestroyCurrentLevel ();
		LoadPlaceables ();
		yield return null;
		GameObject levelGameObject = CreateLevelBase ();
		yield return null;
		Level newLevel = levelGameObject.GetComponent<Level>();
		newLevel.InitializeLevelData (levelData);

		yield return null;
		BuildLevelPlaceables(newLevel);
		yield return null;
		AppendChildren (newLevel);
		newLevel.SetCameraSize ();
		SetLevel(newLevel);	
	}
	public static void AppendChildren(Level level){
		foreach (var placeable in level.levelData.placeables){
			Placeable currentPlaceable = GameObject.FindObjectsOfType<Placeable>().FirstOrDefault(p => p.ID == placeable.ID);
			if (placeable.ParentID != -1){
				Placeable placeableParent = GameObject.FindObjectsOfType<Placeable>().FirstOrDefault(p => p.ID == placeable.ParentID);
				currentPlaceable.transform.SetParent (placeableParent.transform);

			}
		}
	}
	public static void AddGameLevel(LevelData levelData){
		if (!gameLevels.Contains(levelData)){
			gameLevels.Add (levelData);
		} else {
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

