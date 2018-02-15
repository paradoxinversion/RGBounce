using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System;
public class PlaceablePaintedEventArgs : EventArgs {
	public Placeable NewPlaceable {get; set;}
}
public class LevelEditor : MonoBehaviour{
	public EventHandler LevelEditorLoading;
	public EventHandler<PlaceablePaintedEventArgs> PlaceablePainted;
	/// <summary>
	/// Returns a reference to the Level Manager's current level.
	/// </summary>
	/// <value>The current level.</value>
	public Level CurrentLevel{
		get {
			return LevelManager.currentLevelGameObject;
		}
	}

	/// <summary>
	/// Returns a reference to all Placeable prototypes loaded in the Level Manager.
	/// </summary>
	/// <value>The placeables.</value>
	public Placeable[] Placeables{
		get {
			return LevelManager.Placeables;
		}
	}

	private Placeable currentSelection;
	// Returns a reference to the currently selected Placeable instance in the Level Editor.
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
		OnLevelEditorLoading();
		LevelManager.LevelLoaded += OnLevelLoaded;
		PlaceablePainted += LevelManager.OnPlaceablePainted;
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

	private Paddle paddle = null;
	public Paddle CurrentPaddle{
		get {
			return paddle;
		}
	}
	void Update(){
		HandleKeys ();
	}

//	public void ClearLevel(){
//		Destroy (CurrentLevel);
//		LevelManager.SetLevel(null);
//	}

	/// <summary>
	/// Creates a new level GameObject with default level data.
	/// </summary>
	public void CreateNewLevel(){
		if (CurrentLevel != null){
			LevelManager.DestroyCurrentLevel ();
		}
		LevelManager.CreateNewLevel();
	}

	public void GrowLevelArea(){
		float currentSize = CurrentLevel.levelData.CameraSize;
		Camera.main.orthographicSize = currentSize + CameraSizeStep;
		CurrentLevel.levelData.SetCameraSize (Camera.main.orthographicSize);
		CurrentLevel.GenerateEdgeColliders();
	}
	public void ShrinkLevelArea(){
		float currentSize = CurrentLevel.levelData.CameraSize;
		Camera.main.orthographicSize = currentSize - CameraSizeStep;
		CurrentLevel.levelData.SetCameraSize (Camera.main.orthographicSize);
		CurrentLevel.GenerateEdgeColliders();

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
		newPlaceable.gameObject.transform.SetParent (GameObject.FindGameObjectWithTag("placeable container").transform);
		newPlaceable.gameObject.name = placeable.gameObject.name + " " + newPlaceable.pData.ID;
		AnimatedObject animation = newPlaceable.gameObject.AddComponent<AnimatedObject> ();
		animation.SetData (newPlaceable.pData);
		OnPlaceablePainted(newPlaceable);
		return newPlaceable;
	}

	void HandleKeys(){
		if (Input.GetKeyDown(KeyCode.A)){
			PlaceablePaintWidget placeableWidget = GameObject.FindObjectOfType<PlaceablePaintWidget> ();
			if (placeableWidget != null){
				if (placeableWidget.gameObject.activeInHierarchy && placeableWidget.paintEnabled.isOn){
					Placeable newPlaceable = PaintPlaceable (Placeables[placeableWidget.placeableSelect.value]);
					if (newPlaceable != null){
						currentSelection = newPlaceable;
						newPlaceable.pData.SetOriginalPosition (newPlaceable.transform.position);
					}
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
		StartCoroutine(LevelManager.CreateLevel(data));
		
	}

	public void SetObjectScaleStep(float newScaleStep){
		objectScaleStep = newScaleStep;
	}

	public void TestLevel(){
		/// We need to...
		/// Clear the current UI
		/// switch to a widget with a stop test button
		GameObject commandPalette = GameObject.Find("Command Palette");
		LevelEditorUI levelEditorUI = GameObject.FindObjectOfType<LevelEditorUI>();
		if(CurrentLevel.levelData.LevelReady()){
			CurrentLevel.LoadPaddle ();
			CurrentLevel.LoadSpawner ();
			CurrentLevel.StartSpawner ();
			CurrentLevel.levelData.GatherKeys();
			commandPalette.SetActive(false);
			levelEditorUI.HideAllCommands();
			CurrentLevel.StartPlaceableAnimations();
		} else {
			Debug.Log("No spawner in level");
		}
	}

	public void StopTest(){
		LevelEditorUI levelEditorUI = GameObject.FindObjectOfType<LevelEditorUI>();
		CurrentLevel.StopSpawner();
		CurrentLevel.DestroyPaddle();
		CurrentLevel.testMode = false;
		levelEditorUI.commandPalette.SetActive(true);
		levelEditorUI.ShowCommandPalette();
		CurrentLevel.StopPlaceableAnimations();
	}

	protected virtual void OnLevelEditorLoading(){
		if (LevelEditorLoading != null)
			LevelEditorLoading(this, EventArgs.Empty);
	}

	protected virtual void OnLevelLoaded(object source, EventArgs e){
		Debug.Log("Level Was Loaded");
	}

	protected virtual void OnPlaceablePainted(Placeable placeable){
		if (PlaceablePainted != null)
			PlaceablePainted(this, new PlaceablePaintedEventArgs(){NewPlaceable = placeable});
	}
}