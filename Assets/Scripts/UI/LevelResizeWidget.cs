using UnityEngine;
using System.Collections;

public class LevelResizeWidget : MonoBehaviour
{
	public void btn_GrowLevelArea(){
		LevelEditor editor = GameObject.FindObjectOfType<LevelEditor> ();
		editor.GrowLevelArea ();
	}
	public void btn_ShrinkLevelArea(){
		LevelEditor editor = GameObject.FindObjectOfType<LevelEditor> ();
		editor.ShrinkLevelArea ();
	}
}

