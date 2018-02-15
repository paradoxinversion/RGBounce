using UnityEngine;
using System.Collections;
using System;
public class GameManager : MonoBehaviour
{
	private LevelEditor levelEditor;
	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	public void GetLevelEditor(){
		levelEditor = GameObject.FindObjectOfType<LevelEditor>();
		levelEditor.LevelEditorLoading += OnLevelEditorLoading;
	}

	public void OnLevelEditorLoading(object source, EventArgs e){
		
	}
}

