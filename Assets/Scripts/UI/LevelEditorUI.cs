using UnityEngine;
using System.Collections;

public class LevelEditorUI : MonoBehaviour
{
	public GameObject animationCommands;
	public GameObject levelCommands;
	public GameObject placeableCommands;

	public void ShowAnimationCommands(){
		if (animationCommands.activeInHierarchy){
			animationCommands.SetActive (false);
		}else{
			levelCommands.SetActive (false);
			placeableCommands.SetActive (false);
			animationCommands.SetActive (true);
		}

	}

	public void ShowLevelCommands(){
		if (levelCommands.activeInHierarchy) {
			levelCommands.SetActive (false);
		} else {
			levelCommands.SetActive (true);
			placeableCommands.SetActive (false);
			animationCommands.SetActive (false);
		}

	}
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

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
}

