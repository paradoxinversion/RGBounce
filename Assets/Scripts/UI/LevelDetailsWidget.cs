using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LevelDetailsWidget : MonoBehaviour {
	
	public Text levelLabel;
	public InputField levelName;
	public Button renameButton;
	public Button confirmRenameButton;
	public Button newLevelButton;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		UpdateWidget ();
	}
	void UpdateWidget(){
		levelLabel.text = LevelManager.ActiveLevel.levelName;
	}
	public void btn_SaveLevel(){
		LevelEditor editor = GameObject.FindObjectOfType<LevelEditor> ();
		editor.SaveLevelData ();

	}

	public void btn_RenameLevel(){
		levelLabel.gameObject.SetActive (false);
		renameButton.gameObject.SetActive (false);
		confirmRenameButton.gameObject.SetActive (true);
		levelName.gameObject.SetActive (true);
	}

	public void btn_ConfirmRenameLevel(){
		LevelManager.ActiveLevel.RenameLevel (levelName.text);
		levelLabel.gameObject.SetActive (true);
		confirmRenameButton.gameObject.SetActive (false);
		levelName.gameObject.SetActive (false);
		renameButton.gameObject.SetActive (true);
	}

	public void btn_NewLevel(){
		LevelEditor editor = GameObject.FindObjectOfType<LevelEditor> ();
		editor.CreateNewLevel ();
	}
}
