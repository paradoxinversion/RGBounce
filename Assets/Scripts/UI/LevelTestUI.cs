using UnityEngine;
using System.Collections;

public class LevelTestUI : MonoBehaviour
{
	private LevelEditorUI levelEditorUI;
	public void btn_StopTest(){
		levelEditorUI.ShowCommandPalette();
	}
	// Use this for initialization
	void Start ()
	{
		levelEditorUI = GameObject.FindObjectOfType<LevelEditorUI>();

	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
}

