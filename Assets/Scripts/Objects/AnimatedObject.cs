using UnityEngine;
using System.Collections;


public class AnimatedObject : MonoBehaviour
{
	[SerializeField]
	private AnimatedObjectData _animationData;
	public AnimatedObjectData animationData
	{
		get
		{
			return _animationData;
		}
		set
		{
			_animationData = value;
		}
	}
	public enum RotationTypes
	{
		NONE,
		ROTATE_TO,
		ROTATE_BY
	}
	/// <summary>
	/// The end of the movement in X direction--
	/// Object will bounce back from here if ping ponging
	/// </summary>
	public float xDestination
	{
		get
		{
			return animationData.xDestination;
		}

		set
		{
			animationData.xDestination = value;
		}
	}
	public bool useX
	{
		get
		{
			return animationData.useX;
		}
		set
		{
			animationData.useX = value;
		}
	}

	public float yDestinastion
	{
		get
		{
			return animationData.yDestinastion;
		}
		set
		{
			animationData.yDestinastion = value;
		}
	}
	public bool useY
	{
		get
		{
			return animationData.useY;
		}
		set
		{
			animationData.useY = value;
		}
	}
	public double movementTime
	{
		get { return animationData.movementTime;
		}
		set
		{
			animationData.movementTime = value;
		}
	}

	public float movementDelay
	{
		get
		{
			return animationData.movementDelay;
		}

		set
		{
			animationData.movementDelay = value;
		}
	}
	public iTween.EaseType movementEaseType 
	{
		get 
		{
			return animationData.movementEaseType;
		}
		set 
		{
			animationData.movementEaseType = value;
		}
	}
	public iTween.LoopType movementLoopType
	{
		get
		{
			return animationData.movementLoopType;
		}
		set{ 
			animationData.movementLoopType = value;
		}
	}
	Hashtable mht;

	public bool clockwise
	{
		get{
			return animationData.clockwise;
		}

		set{
			animationData.clockwise = value;
		}

	}
	public double rotationTime {
		get {
			return animationData.rotationTime;
		}
		set {
			animationData.rotationTime = value;
		}
	}
	public RotationTypes rotationType {
		get {
			return animationData.rotationType;
		}
		set {
			animationData.rotationType = value;
		}
	}
	public float rotationDelay {
		get {
			return animationData.rotationDelay;
		}
		set {
			animationData.rotationDelay = value;
		}
	}
	public float rotationDegrees
	{
		get
		{
			return animationData.roationDegrees;
		}
		set
		{
			animationData.roationDegrees = value;
		}
	}
	public iTween.EaseType rotationEaseType{
		get{
			return animationData.rotationEaseType;
		}
		set {
			animationData.rotationEaseType = value;
		}
	}
	public iTween.LoopType rotationLoopType
	{
		get { return animationData.rotationLoopType; }
		set { animationData.rotationLoopType = value;
		}
	}
	Hashtable rht;


	/// <summary>
	/// Prepares object for iTween animation.
	/// </summary>
	public void SetAnimationInfo()
	{
		mht = new Hashtable();
		rht = new Hashtable();

		//Movement
		if (useX)
			mht.Add("x", xDestination);
		if (useY)
			mht.Add("y", yDestinastion);
		mht.Add("delay", movementDelay);
		mht.Add("time", movementTime);
		mht.Add("looptype", movementLoopType);
		mht.Add("easetype", movementEaseType);

		//Rotation
		if (rotationType == RotationTypes.ROTATE_BY)
		{
			if (clockwise)
			{
				rht.Add("amount", new Vector3(0, 0, -1));
			}
			else
			{
				rht.Add("amount", new Vector3(0, 0, 1));
			}
		}
		else
		{
			Vector3 newAngle = new Vector3(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, rotationDegrees);
			rht.Add("rotation", newAngle);
		}

		rht.Add("delay", rotationDelay);
		rht.Add("time", rotationTime);
		rht.Add("looptype", rotationLoopType);
		rht.Add("easetype", rotationEaseType);

	}

	public void StartAnimation()
	{
		SetAnimationInfo();
		iTween.MoveTo(gameObject, mht);
		//iTween.RotateBy(gameObject, rht);
		if (rotationType == RotationTypes.ROTATE_BY)
		{
			iTween.RotateBy(gameObject, rht);
		}
		else if (rotationType == RotationTypes.ROTATE_TO)
		{
			iTween.RotateTo(gameObject, rht);
		}
	}
	/// <summary>
	/// Stops animation on this particular object. Removes the itween. Object should be reset before being restarted or strange behavior may occur.
	/// </summary>
	public void StopAnimation()
	{
		iTween.Stop(gameObject);
	}

	/// <summary>
	/// Stops animation on this particular object. Removes the itween.
	/// </summary>
	public void StopAnimationAndReset()
	{
		iTween.Stop(gameObject);
		GetComponent<Placeable>().RestoreDefaults();
	}
	// Pauses animation on this particular object. Does not stop or remove the itween.
	public void PauseAnimation()
	{
		iTween.Pause(gameObject);
	}

	/// <summary>
	/// Resumes animation on the object. Should only be used if the object has no been reset.
	/// </summary>
	public void ResumeAnimation()
	{
		iTween.Resume(gameObject);
	}
	public void SetData(AnimatedObjectData animationData)
	{
		this.animationData = animationData;
	}

}
