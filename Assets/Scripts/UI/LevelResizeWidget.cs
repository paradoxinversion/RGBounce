using UnityEngine;
using System.Collections;

public class LevelResizeWidget : MonoBehaviour
{
	public void btn_GrowLevelArea(){
		LevelEditor editor = GameObject.FindObjectOfType<LevelEditor> ();
		Camera.main.orthographicSize = Camera.main.orthographicSize + editor.CameraSizeStep;
	}
	public void btn_ShrinkLevelArea(){
		LevelEditor editor = GameObject.FindObjectOfType<LevelEditor> ();
		Camera.main.orthographicSize = Camera.main.orthographicSize - editor.CameraSizeStep;
	}
}

