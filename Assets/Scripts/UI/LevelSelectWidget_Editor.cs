using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
public class LevelSelectWidget_Editor : MonoBehaviour
{
	public Dropdown levelDropdown;
	// Use this for initialization
	void OnEnable ()
	{
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	public void PopulateDropdown(){
		if (levelDropdown.options.Count > 0)
			levelDropdown.options.Clear ();
		List<Dropdown.OptionData> options = new List<Dropdown.OptionData> ();
		if (LevelManager.GameLevels != null){
			foreach (LevelData level in LevelManager.GameLevels){
				options.Add (new Dropdown.OptionData (level.levelName));
			}
		}

		levelDropdown.AddOptions (options);
	}

	public void LoadSelection(){
		LevelEditor editor = GameObject.FindObjectOfType<LevelEditor> ();
		editor.LoadLevel (LevelManager.GameLevels [levelDropdown.value]);
//		LevelManager.BuildLevel (LevelManager.GameLevels [levelDropdown.value]);
	}
}

