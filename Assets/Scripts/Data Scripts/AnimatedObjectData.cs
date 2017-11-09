using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

[Serializable]
public class AnimatedObjectData
{
	public float xDestination;
	public bool useX;
	public float yDestinastion;
	public bool useY;
	public double movementTime;
	public float movementDelay;
	public iTween.EaseType movementEaseType;
	public iTween.LoopType movementLoopType;

	public bool clockwise;
	public float roationDegrees;
	public double rotationTime;
	public AnimatedObject.RotationTypes rotationType;
	public float rotationDelay;
	public iTween.EaseType rotationEaseType;
	public iTween.LoopType rotationLoopType;
}

