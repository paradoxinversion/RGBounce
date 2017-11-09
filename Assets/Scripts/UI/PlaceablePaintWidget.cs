using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlaceablePaintWidget : MonoBehaviour {
	public Dropdown placeableSelect;
	public Toggle paintEnabled;
	public void LoadPlaceables(){
		LevelEditor editor = GameObject.FindObjectOfType<LevelEditor>();
		List<Dropdown.OptionData> options = new List<Dropdown.OptionData> ();
		foreach (Placeable placeable in editor.Placeables){
			options.Add (new Dropdown.OptionData (placeable.gameObject.name));
		}
		placeableSelect.AddOptions (options);
	}


	// Use this for initialization
	void OnEnable () {
		
		LoadPlaceables ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
