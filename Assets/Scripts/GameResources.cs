using UnityEngine;
using System.Collections;

public static class GameResources
{
	public static Paddle GetPaddle(){
		Debug.Log ("Getting paddle");
		return Resources.Load<Paddle>("Paddle") as Paddle;
	}

	public static GameObject[] LoadPlaceables(){
		return Resources.LoadAll<GameObject> ("Placeables") as GameObject[];
	}

	public static LevelEditor GetLevelEditor(){
		return GameObject.FindObjectOfType<LevelEditor> ();
	}
}

