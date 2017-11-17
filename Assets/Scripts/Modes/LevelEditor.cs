using UnityEngine;
using System.Collections.Generic;
using System.Linq;
public class LevelEditor : MonoBehaviour{
	public Level CurrentLevel{
		get {
			return LevelManager.currentLevelGameObject;
		}
	}

	public Placeable[] Placeables{
		get {
			return LevelManager.Placeables;
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

	void Start(){
		DataPersistence.LoadLevelData ();
		GameObject.FindObjectOfType< LevelSelectWidget_Editor> ().PopulateDropdown ();
		if (LevelManager.GameLevels.Count > 0){
			LoadLevel (LevelManager.GameLevels [0]);
		}else{
			CreateNewLevel ();
		}
	}

	private float objectScaleStep = 0.2f;
	public float ObjectScaleStep{
		get {
			return objectScaleStep;
		}
	}

	private float objectRotationStep = 22.5f;
	public float ObjectRotationStep{
		get {
			return objectRotationStep;
		}

	}

	private float cameraSizeStep = 0.5f;
	public float CameraSizeStep{
		get {
			return cameraSizeStep;
		}
	}
	void Update(){
		HandleKeys ();
	}

	public void ClearLevel(){
		Destroy (CurrentLevel);
		LevelManager.SetLevel(null);
	}

	public void CreateNewLevel(){
		if (CurrentLevel != null){
			LevelManager.DestroyCurrentLevel ();
		}
		GameObject levelGameObject = new GameObject ();
		Level newLevel = levelGameObject.AddComponent<Level> ();
		newLevel.InitializeLevelData ();
		levelGameObject.name = levelGameObject.GetComponent<Level> ().levelData.levelName;
		LevelManager.SetLevel(newLevel);
		CurrentLevel.GenerateEdgeColliders ();
		levelGameObject.tag = "level";
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
		CurrentLevel.levelData.AddPlaceableData (newPlaceable.pData);
		newPlaceable.gameObject.transform.SetParent (CurrentLevel.gameObject.transform);
		newPlaceable.gameObject.name = placeable.gameObject.name + " " + newPlaceable.pData.ID;
		AnimatedObject animation = newPlaceable.gameObject.AddComponent<AnimatedObject> ();
		animation.SetData (newPlaceable.pData);

		return newPlaceable;
	}

	void HandleKeys(){
		if (Input.GetKeyDown(KeyCode.A)){
			PlaceablePaintWidget placeableWidget = GameObject.FindObjectOfType<PlaceablePaintWidget> ();
			if (placeableWidget.enabled && placeableWidget.paintEnabled.isOn){
				Placeable newPlaceable = PaintPlaceable (Placeables[placeableWidget.placeableSelect.value]);
				if (newPlaceable != null){
					currentSelection = newPlaceable;
					newPlaceable.pData.SetOriginalPosition (newPlaceable.transform.position);
				}
			}
		} else if (Input.GetKeyDown(KeyCode.D)){
			if (currentSelection != null){
				DeletePlaceable ();
			}
		} else if (Input.GetKeyDown(KeyCode.Comma)){
			if (currentSelection != null){
				if ((Input.GetKey(KeyCode.LeftShift))){
					currentSelection.transform.Rotate (0, 0, ObjectRotationStep);
				}else{
					currentSelection.transform.localScale = new Vector3(
						currentSelection.transform.localScale.x - ObjectScaleStep, 
						currentSelection.transform.localScale.y - ObjectScaleStep,
						1);
				}
			}
		} else if (Input.GetKeyDown(KeyCode.Period)){
			if (currentSelection != null){
				if (Input.GetKey(KeyCode.LeftShift)){
					currentSelection.transform.Rotate (0, 0, -ObjectRotationStep);
				}else{
					currentSelection.transform.localScale = new Vector3(
						currentSelection.transform.localScale.x + ObjectScaleStep, 
						currentSelection.transform.localScale.y + ObjectScaleStep,
						1);
				}
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
		CurrentSelection.DeletePlaceable (CurrentLevel.levelData);
	}

	public void SaveLevelData(){
		LevelManager.AddGameLevel (CurrentLevel.levelData);
		DataPersistence.SaveLevelData ();
	}

	public void LoadLevel(LevelData data){
		LevelManager.BuildLevel (data);
	}

	public void SetObjectScaleStep(float newScaleStep){
		objectScaleStep = newScaleStep;
	}
}