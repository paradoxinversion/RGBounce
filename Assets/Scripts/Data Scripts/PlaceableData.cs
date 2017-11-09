using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
/// <summary>
/// Data representation of a solid obstacle in the game.
/// </summary>
[Serializable]
public class PlaceableData
{
    public string ObstacleName;
	public string typeStr;
    public int ColorMode;
    /// <summary>
    /// ID of the Placeable this one is the child of
    /// </summary>
    private int parentID = -1;
    public int ParentID
    {
        get
        {
            return parentID;
        }
    }
    private int id = -1;
    public int ID
    {
        get { return id; }
        set { id = value; }
    }
    public int objectType = -1;
    [SerializeField] private float originalPositionX = 0;
    [SerializeField] private float originalPositionY = 0;

    [SerializeField] private float originalRotationW = 0;
    [SerializeField] private float originalRotationX = 0;
    [SerializeField] private float originalRotationY = 0;
    [SerializeField] private float originalRotationZ = 0;

    [SerializeField] private float originalScaleX = 1;
    [SerializeField] private float originalScaleY = 1;

	[SerializeField] private float currentPositionX = 0;
	[SerializeField] private float currentPositionY = 0;

	[SerializeField] private float currentRotationW = 0;
	[SerializeField] private float currentRotationX = 0;
	[SerializeField] private float currentRotationY = 0;
	[SerializeField] private float currentRotationZ = 0;

	[SerializeField] private float currentScaleX = 1;
	[SerializeField] private float currentScaleY = 1;
	public PlaceableData(string name)
    {
		typeStr = name;
		ObstacleName = name;
        id = -1;
        parentID = -1;
    }
    public void SetParentID(int newParentID)
    {
        parentID = newParentID;
    }

    public void ClearParent()
    {
        parentID = -1;
    }
    public void SetOriginalPosition(Vector2 position)
    {
        originalPositionX = position.x;
        originalPositionY = position.y;
    }
	public Vector2 ReturnOriginalPosition(){
		return new Vector2 (originalPositionX, originalPositionY);
	}

	public void SetOriginalScale(Vector2 scale){
		originalScaleX = scale.x;
		originalScaleY = scale.y;
	}
    public Vector2 ReturnOriginalScale()
    {
        return new Vector2(originalScaleX, originalScaleY);
    }

    public void SetOriginalRotation(Quaternion rotation)
    {
        originalRotationX = rotation.x;
        originalRotationY = rotation.y;
        originalRotationZ = rotation.z;
        originalRotationW = rotation.w;
    }
    public Quaternion ReturnOriginalRotation()
    {
        return new Quaternion(originalRotationX, originalRotationY, originalRotationZ, originalRotationW);
    }

	/// <summary>
	/// Sets all current Pos/Rot/Scale as defaults
	/// </summary>
	public void FreezeDefaults(){
		SetOriginalPosition (new Vector2(currentPositionX, currentPositionY));
		SetOriginalScale (new Vector2 (currentScaleX, currentScaleY));
		SetOriginalRotation (new Quaternion (currentRotationX, currentRotationY, currentRotationZ, currentRotationW));
	}
}

