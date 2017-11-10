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
	public Text defaultLocation;
	public Text currentLocation;

	public void UpdateWidget(){
		if (SelectedObject != null){
			RenderPlacable ();
		} else{
			RenderNoPlaceable ();
		}
	}

	public void RenderPlacable(){
		Vector2 originalPosition = SelectedObject.pData.ReturnOriginalPosition ();
		objectName.text = SelectedObject.gameObject.name;
		defaultLocation.text = string.Format ("Default: ({0}, {1})", originalPosition.x, originalPosition.y);
		currentLocation.text = string.Format ("Current: ({0}, {1})", SelectedObject.transform.position.x, SelectedObject.transform.position.y);
	}

	public void RenderNoPlaceable(){
		objectName.text = "No Placeable Selected";
	}

	void Update(){
		UpdateWidget ();
	}

	void Start(){
		
	}
}

