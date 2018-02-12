using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
[System.Serializable]
public class LevelData 
{
	public string levelName;
	public List<PlaceableData> placeables = new List<PlaceableData>();
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
	public LevelData(string name = "New Level"){
		levelName = name;
		placeables = new List<PlaceableData> ();
	}
	public LevelData(LevelData data){
		levelName = data.levelName;
		placeables = data.placeables;
	}
	public void AddPlaceableData(PlaceableData placeableData){
		placeableData.ID = totalCreatedObjects;

		placeables.Add (placeableData);
		totalCreatedObjects++;
	}
	public void RemovePlaceableData(PlaceableData placeableData){
		placeables.Remove (placeableData);
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
		if (placeables.FirstOrDefault (placeable => placeable.ObstacleName == "Ball Spawner") != null)
			return true;
		return false;
	}
}

