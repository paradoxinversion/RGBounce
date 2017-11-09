using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public interface ILevelEditorMoveable
{
    
    GameObject gameObject { get; }
    string ObjectName { get; }
    PlaceableData pData { get; }
    Vector2 OriginalPosition { get; }
    Quaternion OriginalRotation { get; }
    Vector3 OriginalScale { get; }
    void RestoreDefaults();
    void SetDefaultPosition(Vector2 defaultPosition);
    void SetDefaultRotation(Quaternion defaultRotation);
    void SetDefaultScale(Vector3 defaultScale);
    void SetData(PlaceableData placeableData);
}
