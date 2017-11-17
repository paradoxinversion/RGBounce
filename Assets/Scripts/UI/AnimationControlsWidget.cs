using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;
public class AnimationControlsWidget : MonoBehaviour
{
	public InputField xTarget;
	public InputField yTarget;
	public InputField movementTime;
	public InputField movementDelay;
	public Dropdown movementEaseTypeDropdown;
	public Dropdown movementLoopTypeDropdown;
	public Toggle xToggle;
	public Toggle yToggle;
	public Toggle clockwiseRotationToggle;
	public InputField rotationTime;
	public InputField rotationDelay;
	public InputField rotationDegrees;
	public Dropdown rotationEaseTypeDropdown;
	public Dropdown rotationLoopTypeDropdown;
	public Toggle rotationMethodToggle; //FIXME: What is this?
//	public UIWindowBase movementAnimationWindow;
//	public UIWindowBase rotationAnimationWindow;
	private void OnEnable()
	{
		SetInputs();
		SetAnimationDropdowns ();
	}
//	public void btn_ToggleMovementAnimation()
//	{
//		if (movementAnimationWindow.isActiveAndEnabled)
//		{
//			movementAnimationWindow.gameObject.SetActive(false);
//
//		}
//		else
//		{
//			movementAnimationWindow.gameObject.SetActive(true);
//
//		}
//	}
//	public void btn_ToggleRotationAnimation()
//	{
//		if (rotationAnimationWindow.isActiveAndEnabled)
//		{
//			rotationAnimationWindow.gameObject.SetActive(false);
//		}
//		else
//		{
//			rotationAnimationWindow.gameObject.SetActive(true);
//		}
//
//	}


	public void RotationMethodToggled()
	{
		LevelEditor editor = GameObject.FindObjectOfType<LevelEditor> ();
		if (editor.CurrentSelection != null)
		{
			AnimatedObject ao = editor.CurrentSelection.GetComponent<AnimatedObject>();
			if (ao != null)
			{
				if (rotationMethodToggle.isOn)
				{
					ao.rotationType = AnimatedObject.RotationTypes.ROTATE_BY;
				}
				else
				{
					ao.rotationType = AnimatedObject.RotationTypes.ROTATE_TO;
				}
			}
		}
	}
	public void XMovementToggled()
	{
		LevelEditor editor = GameObject.FindObjectOfType<LevelEditor> ();

		if (editor.CurrentSelection != null)
		{
			AnimatedObject ao = editor.CurrentSelection.GetComponent<AnimatedObject>();
			if (ao != null)
			{
				ao.useX = xToggle.isOn;
			}
		}
	}
	public void YMovementToggled()
	{
		LevelEditor editor = GameObject.FindObjectOfType<LevelEditor> ();
		if (editor.CurrentSelection != null)
		{
			AnimatedObject ao = editor.CurrentSelection.GetComponent<AnimatedObject>();
			if (ao != null)
			{
				ao.useY = yToggle.isOn;
			}
		}
	}
	/// <summary>
	/// Updates animation inputs to have the same values as the curremtly selected object, should be called when an object is selected.
	/// </summary>
	public void SetInputs()
	{
		LevelEditor editor = GameObject.FindObjectOfType<LevelEditor> ();
		if (editor.CurrentSelection)
		{
			AnimatedObject ao = editor.CurrentSelection.GetComponent<AnimatedObject>();
			if (ao != null)
			{
				// Set inputs to current movemnt values
				xTarget.text = ao.xDestination.ToString();
				yTarget.text = ao.yDestinastion.ToString();
				movementTime.text = ao.movementTime.ToString();
				movementDelay.text = ao.movementDelay.ToString();
				movementEaseTypeDropdown.value = (int)ao.movementEaseType;
				movementLoopTypeDropdown.value = (int)ao.movementLoopType;
				xToggle.isOn = ao.useX;
				yToggle.isOn = ao.useY;


				//Rotation
				clockwiseRotationToggle.isOn = ao.clockwise;
				rotationTime.text = ao.rotationTime.ToString();
				rotationDelay.text = ao.rotationDelay.ToString();
				rotationEaseTypeDropdown.value = (int)ao.rotationEaseType ;
				rotationLoopTypeDropdown.value = (int)ao.rotationLoopType ;
				if (ao.rotationType == AnimatedObject.RotationTypes.ROTATE_BY)
				{
					rotationMethodToggle.isOn = true;
				}
				else
				{
					rotationMethodToggle.isOn = false;
				}
				rotationDegrees.text = ao.rotationDegrees.ToString();
			}
			else
			{
				Debug.LogWarning("AnimationControlsWidget:SetInputs:: No animated object is selected. Animated controls widget should likely be closed.");
			}
		}

	}

	public void UpdateSelectedObstacleAnimationValues()
	{
		LevelEditor editor = GameObject.FindObjectOfType<LevelEditor> ();
		if (editor.CurrentSelection != null)
		{
			AnimatedObject ao = editor.CurrentSelection.GetComponent<AnimatedObject>();
			if (editor.CurrentSelection.GetComponent<AnimatedObject>() != null)
			{

				//Movement
				if (xToggle.isOn)
				{
					ao.useX = true;
					ao.xDestination = System.Convert.ToInt32(xTarget.text);
				}
				else
				{
					ao.useX = false;
				}

				if (yToggle.isOn)
				{
					ao.useY = true;
					ao.yDestinastion = System.Convert.ToInt32(yTarget.text);
				}
				else
				{
					ao.useY = false;
				}

				if (string.IsNullOrEmpty(movementTime.text) == false)
				{
					ao.movementTime = System.Convert.ToInt32(movementTime.text);
				}
				else
				{
					ao.movementTime = 0;
				}
				if (string.IsNullOrEmpty(xTarget.text) == false)
				{
					ao.movementDelay = System.Convert.ToInt32(movementDelay.text);
				}
				else
				{
					ao.movementDelay = 0;
				}
				ao.movementEaseType = (iTween.EaseType)movementEaseTypeDropdown.value;
				ao.movementLoopType = (iTween.LoopType)movementLoopTypeDropdown.value;



				//Rotation
				ao.clockwise = clockwiseRotationToggle.isOn;
				if (string.IsNullOrEmpty(rotationTime.text) == false)
				{
					ao.rotationTime = System.Convert.ToInt32(rotationTime.text);
				}
				else
				{
					ao.rotationTime = 0;
				}
				if (string.IsNullOrEmpty(rotationDelay.text) == false)
				{
					ao.rotationDelay = System.Convert.ToInt32(rotationDelay.text);

				}
				else
				{
					ao.rotationDelay = 0;
				}
				ao.rotationEaseType = (iTween.EaseType)rotationEaseTypeDropdown.value;
				ao.rotationLoopType = (iTween.LoopType)rotationLoopTypeDropdown.value;
				if (rotationMethodToggle.isOn)
				{
					ao.rotationType = AnimatedObject.RotationTypes.ROTATE_BY;
				}
				else
				{
					ao.rotationType = AnimatedObject.RotationTypes.ROTATE_TO;
				}
				if (string.IsNullOrEmpty(rotationDegrees.text) == false)
				{
					ao.rotationDegrees = Convert.ToInt32(rotationDegrees.text);
				}
				else
				{
					ao.rotationDegrees = 0;
				}

				editor.CurrentSelection.GetComponent<Placeable>().RestoreDefaults();
				ao.SetAnimationInfo();
//				ao.StartAnimation();
			}
		}
	}

	public void SetAnimationDropdowns()
	{
		movementEaseTypeDropdown.ClearOptions();
		rotationEaseTypeDropdown.ClearOptions();

		movementLoopTypeDropdown.ClearOptions();
		rotationLoopTypeDropdown.ClearOptions();
		List<string> easeTypes = new List<string>();
		List<string> loopTypes = new List<string>();
		for (int easeIndex = 0; easeIndex < System.Enum.GetNames(typeof(iTween.EaseType)).Length; easeIndex++)
		{
			easeTypes.Add(System.Enum.GetNames(typeof(iTween.EaseType))[easeIndex].ToString());
		}
		for (int loopIndex = 0; loopIndex < System.Enum.GetNames(typeof(iTween.LoopType)).Length; loopIndex++)
		{
			loopTypes.Add(System.Enum.GetNames(typeof(iTween.LoopType))[loopIndex].ToString());
		}

		movementEaseTypeDropdown.AddOptions(easeTypes);
		rotationEaseTypeDropdown.AddOptions(easeTypes);

		movementLoopTypeDropdown.AddOptions(loopTypes);
		rotationLoopTypeDropdown.AddOptions(loopTypes);

	}

	public void btn_PauseAnimation(){
		Level currentLevel = GameObject.FindObjectOfType<Level> ();
		if (currentLevel.animationStatus == LevelAnimationStatus.RUNNING){
			currentLevel.PausePlaceableAnimations ();
		}else if (currentLevel.animationStatus == LevelAnimationStatus.PAUSED){
			currentLevel.ResumePlaceableAnimations ();
		}

	}
	public void btn_PlayAnimation(){
		Level currentLevel = GameObject.FindObjectOfType<Level> ();
		currentLevel.StartPlaceableAnimations ();
	}
	public void btn_StopAnimation(){
		Level currentLevel = GameObject.FindObjectOfType<Level> ();
		currentLevel.StopPlaceableAnimations ();
	}
}