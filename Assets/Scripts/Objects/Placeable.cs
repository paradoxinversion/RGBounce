﻿using UnityEngine;
using System.Collections;

public class Placeable : MonoBehaviour
{
	/// <summary>
	/// If true, placeable's collider is inactive during play
	/// </summary>
	public bool passable;
	[SerializeField]
	PlaceableData _pData;
	public PlaceableData pData{
		get{
			if (_pData == null)
				_pData = new PlaceableData("Placeable");
			return _pData;
		}
		private set { _pData = value; }
	}

	public string ObjectName{
		get {
			return pData.ObstacleName;
		}
	}

	public int ID{
		get{
			return pData.ID;
		}
	}

	public Vector2 OriginalPosition{
		get{
			return pData.ReturnOriginalPosition();
		}
	}

	public Quaternion OriginalRotation{
		get{
			return pData.ReturnOriginalRotation();
		}
	}

	public Vector3 OriginalScale{
		get{
			return pData.ReturnOriginalScale();
		}
	}

	public void SetDefaultPosition(Vector2 defaultPosition){
		pData.SetOriginalPosition(defaultPosition);
	}

	public void SetDefaultRotation(Quaternion defaultRotation){
		pData.SetOriginalRotation(defaultRotation);
	}

	public void SetDefaultScale(Vector3 defaultScale)
	{
		pData.SetOriginalScale(defaultScale);
	}

	public void SetData(PlaceableData keyData){
		this.pData = keyData;
	}

	/// <summary>
	/// Restores the placeable's original position, rotation, and scale.
	/// </summary>
	public void RestoreDefaults(){
		transform.position = OriginalPosition;
		transform.rotation = OriginalRotation;
		transform.localScale = OriginalScale;
	}

	/// <summary>
	/// 'Freezes' current values setting them as default
	/// </summary>
	public void FreezeDefaults(){
		
	}


	public Vector2 LevelEditorOffset { get; private set; }

	private void OnMouseDown(){
		LevelEditor editor = GameObject.FindObjectOfType<LevelEditor> ();
		AnimationControlsWidget animationControls = GameObject.FindObjectOfType<AnimationControlsWidget> ();
		if (editor != null){
			
			if (editor.ParentModeEnabled && editor.CurrentSelection.ID != ID){
				// If parenting is enabled...
				editor.SetPlaceableParent(editor.CurrentSelection, this);

				editor.SetParentMode (false);
			}else{
				editor.SetSelection (this);
			}

			if (animationControls != null){
				animationControls.SetInputs ();
			}

		}
	}
	private void OnMouseDrag(){
		MovePlaceable();
	}
	public void MovePlaceable(){
		float posX = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
		float posY = Camera.main.ScreenToWorldPoint(Input.mousePosition).y;
		Vector2 pos = new Vector2(Mathf.Round(posX) * LevelEditConfig.gridScaleSize, Mathf.Round(posY) * LevelEditConfig.gridScaleSize) + LevelEditorOffset;
		transform.position = pos;
		SetDefaultPosition(transform.position);
	}

	public void DeletePlaceable(LevelData level){
		int children = gameObject.transform.childCount;
		//Delete all children first, if it has any
		if (children > 0){
			for (int x = children-1; x > -1; x--){
				gameObject.transform.GetChild (x).GetComponent<Placeable> ().DeletePlaceable (level);
			}
		}
		level.RemovePlaceableData (pData);
		Destroy (gameObject);
	}

	/// <summary>
	/// Sent when another object enters a trigger collider attached to this
	/// object (2D physics only).
	/// </summary>
	/// <param name="other">The other Collider2D involved in this collision.</param>
	void OnTriggerEnter2D(Collider2D other)
	{
		pData.ExecuteEffect(transform, other);
	}
}

