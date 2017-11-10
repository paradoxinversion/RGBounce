using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class StartScreen : MonoBehaviour
{

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	public void btn_GoToLevelEditor(){
		SceneManager.LoadScene ("Level Editor");
	}
}

