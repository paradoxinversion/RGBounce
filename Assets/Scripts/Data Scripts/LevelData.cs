using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
[System.Serializable]
public class LevelData 
{
	public EventHandler LevelUpdated;
	public string levelName;
    private List<PlaceableData> placeables = new List<PlaceableData>();
	public List<PlaceableData> Placeables
    {
        get
        {
            return placeables;
        }

        set
        {
            placeables = value;
			OnLevelUpdated();
        }
    }

    private int totalCreatedObjects;
	public int TotalCreatedObjects{
		get {
			return totalCreatedObjects;
		}
		set {
			if (value > totalCreatedObjects){
				totalCreatedObjects = value;
			}else{
				Debug.Log ("Value was not lower than current index");
			}
		}
	}
	[SerializeField]
	private float cameraSize = 5.0f;
	public float CameraSize{
		get {
			return cameraSize;
		}
	}

	private float keysRemaining;
	public float KeysRemaining{
		get {
			return keysRemaining;
		}
	}
    private VictoryConditionData victoryCondition;

    public VictoryConditionData VictoryCondition {
        get {
            return victoryCondition;
        }
    }

   
    public LevelData(string name = "New Level"){
		levelName = name;
		Placeables = new List<PlaceableData> ();
		victoryCondition = new VictoryConditionData(VictoryConditionData.Condition.KEYS, GatherKeys());
	}

	// XXX: If this is never called, why does it exist and what is serving its purpose?
	public LevelData(LevelData data){
		levelName = data.levelName;
		Placeables = data.Placeables;
		victoryCondition = new VictoryConditionData(VictoryConditionData.Condition.KEYS, GatherKeys());
	}
	public void AddPlaceableData(PlaceableData placeableData){
		placeableData.ID = totalCreatedObjects;

		Placeables.Add (placeableData);
		totalCreatedObjects++;
	}
	public void RemovePlaceableData(PlaceableData placeableData){
		Placeables.Remove (placeableData);
	}

	public void RenameLevel(string newName){
		LevelSelectWidget_Editor levelSelect = GameObject.FindObjectOfType<LevelSelectWidget_Editor> ();
		levelName = newName;
		levelSelect.PopulateDropdown ();
	}

	public void SetCameraSize(float newSize){
		cameraSize = newSize;
	}

	/// <summary>
	/// Checks that the level is prepared. Returns false if there is no spawner.
	/// </summary>
	public bool LevelReady(){
		if (Placeables.FirstOrDefault (placeable => placeable.ObstacleName == "Ball Spawner") != null)
			return true;
		return false;
	}

	public PlaceableData GetSpawnPoint(){
		return Placeables.FirstOrDefault(placeable => placeable.typeStr == "Ball Spawner");
	}

	public void GetKey(){
		keysRemaining--;
		Debug.Log(keysRemaining);
	}
	private bool isKey(PlaceableData pData){
		if (pData.typeStr == "Key"){
			return true;
		}
		return false;
	}
	/// <summary>
	/// Counts the amount of keys in Placeables and sets the value as keysRemaining
	/// </summary>
	public int GatherKeys(){
		int keys = Placeables.FindAll(isKey).Count;
		keysRemaining = keys;
		return keys;
	}

	protected virtual void OnLevelUpdated(){
		if (LevelUpdated != null)
			LevelUpdated(this, EventArgs.Empty);
	}
}

