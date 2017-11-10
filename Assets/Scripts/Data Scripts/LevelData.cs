using UnityEngine;
using System.Collections;
using System.Collections.Generic;
[System.Serializable]
public class LevelData 
{
	public string levelName;
	public List<PlaceableData> placeables = new List<PlaceableData>();
	public List<AnimatedObjectData> animations = new List<AnimatedObjectData>();
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
	public LevelData(string name = "New Level"){
		levelName = name;
		placeables = new List<PlaceableData> ();
		animations = new List<AnimatedObjectData> ();
	}
	public LevelData(LevelData data){
		levelName = data.levelName;
		placeables = data.placeables;
		animations = data.animations;
	}
	public void AddPlaceableData(PlaceableData placeableData){
		placeables.Add (placeableData);
		placeableData.ID = totalCreatedObjects;
		totalCreatedObjects++;
	}
	public void RemovePlaceableData(PlaceableData placeableData){
		placeables.Remove (placeableData);
	}
}

