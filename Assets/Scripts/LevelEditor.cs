using UnityEngine;
using System.Collections.Generic;
using System.Linq;
public class LevelEditor : MonoBehaviour{
	public Level currentLevel;
	private Placeable[] placeables;
	public Placeable[] Placeables{
		get {
			if (placeables == null || placeables.Length == 0){
				LoadPlaceables ();
			}
			return placeables;
		}

	}
	private Placeable currentSelection;
	public Placeable CurrentSelection{
		get {
			return currentSelection;
		}
	}
	private bool parentModeEnabled;
	public bool ParentModeEnabled {
		get {
			return parentModeEnabled;
		}
	}
	public void ClearLevel(){
		Destroy (currentLevel);
		currentLevel = null;
	}

	public void CreateNewLevel(){
		GameObject levelGameObject = new GameObject ();
		Level newLevel = levelGameObject.AddComponent<Level> ();
		newLevel.InitializeLevelData ();
		levelGameObject.name = levelGameObject.GetComponent<Level> ().levelData.levelName;
		currentLevel = newLevel;
		currentLevel.GenerateEdgeColliders ();
		levelGameObject.tag = "level";
	}

	public void LoadPlaceables(){
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

	/// <summary>
	/// Paints the placeable.
	/// </summary>
	/// <param name="placeable">Placeable.</param>
	public Placeable PaintPlaceable(Placeable placeable){
		float x = Mathf.Round(Camera.main.ScreenToWorldPoint(Input.mousePosition).x);
		float y = Mathf.Round(Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
		Placeable newPlaceable = Instantiate (placeable, new Vector2(x, y), Quaternion.identity);
		newPlaceable.SetData(new PlaceableData (placeable.name));
		currentLevel.levelData.AddPlaceableData (newPlaceable.pData);
		newPlaceable.gameObject.transform.SetParent (currentLevel.gameObject.transform);
		newPlaceable.gameObject.name = placeable.gameObject.name + " " + newPlaceable.pData.ID;
		return newPlaceable;
	}
	void Start(){
		CreateNewLevel ();
	}
	void HandleKeys(){
		if (Input.GetKeyDown(KeyCode.A)){
			PlaceablePaintWidget placeableWidget = GameObject.FindObjectOfType<PlaceablePaintWidget> ();
			if (placeableWidget.enabled && placeableWidget.paintEnabled.isOn){
				Placeable newPlaceable = PaintPlaceable (placeables[placeableWidget.placeableSelect.value]);
				if (newPlaceable != null){
					currentSelection = newPlaceable;
				}
			}
		} else if (Input.GetKeyDown(KeyCode.Backspace)){
			if (currentSelection != null){
				DeletePlaceable ();
			}
		}


	}
	public void SetSelection(Placeable placeable){
		currentSelection = placeable;
	}

	public bool SetParentMode(bool modeEnabled){
		parentModeEnabled = modeEnabled;
		return modeEnabled;
	}

	public void SetPlaceableParent(Placeable placeable, Placeable newParent){
		placeable.pData.SetParentID (newParent.ID);
		placeable.transform.SetParent (newParent.transform);
	}

	public void UnsetPlaceableParent(Placeable placeable){
		CurrentSelection.transform.SetParent (CurrentSelection.transform.parent.parent);

		placeable.pData.ClearParent ();
	}
	public void DeletePlaceable(){
		CurrentSelection.DeletePlaceable (currentLevel.levelData);
	}
	void Update(){
		HandleKeys ();
	}
}