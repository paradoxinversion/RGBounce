using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class ParentingWidget : MonoBehaviour
{
	public Placeable SelectedObject{
		get {
			LevelEditor editor = GameResources.GetLevelEditor ();
			if (editor != null){
				return editor.CurrentSelection;
			}else{
				Debug.Log ("Tried to get Level Editor but it was null");
				return null;
			}
		}
	}

	public Text statusText;
	public Button parentPlaceableButton;
	public Button cancelParentingButton;
	public Button unparentPlaceableButton;

	public void UpdateWidget(){
		if (SelectedObject != null){
			//If an Object is selectd
			if (SelectedObject.pData.ParentID != -1){
				//if the Object has a parent
				statusText.text = "Parent: " + SelectedObject.pData.ParentID.ToString();
				unparentPlaceableButton.gameObject.SetActive (true);
				cancelParentingButton.gameObject.SetActive (false);
				parentPlaceableButton.gameObject.SetActive (false);
			}else{
				//If the object has no parent
				LevelEditor editor = GameResources.GetLevelEditor();
				if (editor.ParentModeEnabled){
					//If we have enabled parent mode...
					statusText.text = "Select a new Parent";
					parentPlaceableButton.gameObject.SetActive(false);
					cancelParentingButton.gameObject.SetActive (true);
					unparentPlaceableButton.gameObject.SetActive (false);
				} else{
					//If parent mode is not enabled...
					statusText.text = "No Parent";
					parentPlaceableButton.gameObject.SetActive(true);
					cancelParentingButton.gameObject.SetActive (false);
					unparentPlaceableButton.gameObject.SetActive (false);

				}

			}
		}else{
			//If no Object is selectd
			statusText.text = "No Placeable Selected";
			parentPlaceableButton.gameObject.SetActive(false);
			unparentPlaceableButton.gameObject.SetActive (false);
			cancelParentingButton.gameObject.SetActive (false);
		}

	}

	public void btn_EnableParentMode(){
		LevelEditor editor = GameResources.GetLevelEditor ();
		editor.SetParentMode (true);
	}
	public void btn_CancelParentMode(){
		LevelEditor editor = GameResources.GetLevelEditor ();
		editor.SetParentMode (false);
	}

	public void btn_Unparent(){
		LevelEditor editor = GameResources.GetLevelEditor ();
		editor.UnsetPlaceableParent (editor.CurrentSelection);
	}
	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		UpdateWidget ();
	}
}

