using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class Preloader : MonoBehaviour
{

	void Start ()
	{
		DataPersistence.LoadGameData ();
		SceneManager.LoadScene ("Start Menu");
	}
	

}

