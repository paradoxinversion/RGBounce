using UnityEngine;
using System.Collections;

public class LevelEditorUI : MonoBehaviour
{
	public GameObject commandPalette;
	public GameObject animationCommands;
	public GameObject levelCommands;
	public GameObject placeableCommands;

	/// <summary>
	/// Start is called on the frame when a script is enabled just before
	/// any of the Update methods is called the first time.
	/// </summary>
	void Start()
	{
		if (commandPalette == null)
			commandPalette = GameObject.Find("Command Palette");
		if (animationCommands == null)
			animationCommands = GameObject.FindObjectOfType<AnimationControlsWidget>().gameObject;
		
	}
	/// <summary>
	/// Shows animation commends-- Used in the Unity Editor
	/// </summary>
	public void ShowAnimationCommands(){
		if (animationCommands.activeInHierarchy){
			animationCommands.SetActive (false);
		}else{
			levelCommands.SetActive (false);
			placeableCommands.SetActive (false);
			animationCommands.SetActive (true);
		}

	}
	/// <summary>
	/// Shows level commands-- Used in the Unity Editor
	/// </summary>
	public void ShowLevelCommands(){
		if (levelCommands.activeInHierarchy) {
			levelCommands.SetActive (false);
		} else {
			levelCommands.SetActive (true);
			placeableCommands.SetActive (false);
			animationCommands.SetActive (false);
		}

	}
	/// <summary>
	/// Shows placeable commands-- Used in the Unity Editor
	/// </summary>
	public void ShowPlaceableCommands(){
		if (placeableCommands.activeInHierarchy) {
			placeableCommands.SetActive (false);
		} else {
			levelCommands.SetActive (false);
			placeableCommands.SetActive (true);
			animationCommands.SetActive (false);
		}

	}

	public void HideAllCommands(){
		levelCommands.SetActive (false);
		placeableCommands.SetActive (false);
		animationCommands.SetActive (false);
	}
	public void ShowCommandPalette(){
		levelCommands.SetActive (true);
		placeableCommands.SetActive (true);
		animationCommands.SetActive (true);
	}
}

