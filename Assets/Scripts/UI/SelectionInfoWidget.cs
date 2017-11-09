using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class SelectionInfoWidget : MonoBehaviour
{
	public Placeable SelectedObject{
		get {
			LevelEditor editor = GameObject.FindObjectOfType<LevelEditor> ();
			if (editor != null){
				return editor.CurrentSelection;
			}else{
				Debug.Log ("Tried to get Level Editor but it was null");
				return null;
			}
		}
	}

	public Text objectName;
	public Text objectParent;

	public void UpdateWidget(){
		if (SelectedObject != null){
			RenderPlacable ();
		} else{
			RenderNoPlaceable ();
		}
	}

	public void RenderPlacable(){
		objectName.text = SelectedObject.gameObject.name;
		if (SelectedObject.pData.ParentID != -1){
			//TODO Change this to the name of the gameobject that is the parent
			objectParent.text = SelectedObject.pData.ParentID.ToString();
		}else{
			objectParent.text = "No Parent";
		}
	}

	public void RenderNoPlaceable(){
		objectName.text = "No Placeable Selected";
		objectParent.text = "";
	}

	void Update(){
		UpdateWidget ();
	}

	void Start(){
		
	}
}

