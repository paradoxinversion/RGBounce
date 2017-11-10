using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LevelDetailsWidget : MonoBehaviour {
	public InputField levelName;
	public Button renameButton;
	public Button confirmRenameButton;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void btn_SaveLevel(){
		LevelEditor editor = GameObject.FindObjectOfType<LevelEditor> ();
		editor.SaveLevelData ();

	}

	public void btn_RenameLevel(){
		
	}
}
