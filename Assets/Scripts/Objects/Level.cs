using UnityEngine;
using System.Collections;
using System.Linq;
public enum LevelAnimationStatus{
	STOPPED_OR_NONE,
	RUNNING,
	PAUSED
}
public class Level : MonoBehaviour
{
	public Paddle paddle;
	public SpawnController spawnController;
	public const float PaddleYPosition = 0.05f;
	public LevelData levelData;
	public LevelAnimationStatus animationStatus = LevelAnimationStatus.STOPPED_OR_NONE;
	/// <summary>
	/// Is the level in a testing mode? 
	/// </summary>	
	public bool testMode;
	/// <summary>
	/// Have level boundaries been created yet?
	/// This mostly matters if testMode is true
	/// </summary>
	
	void Start(){
		if (testMode){
			GenerateEdgeColliders();
			LoadPaddle();
		}
			
	}

	public void SetCameraSize(){
		if (levelData != null){
			Camera.main.orthographicSize = levelData.CameraSize;
		}
	}

	public void LoadPaddle(){
		float adjustedY = Camera.main.ViewportToWorldPoint ( new Vector3(transform.position.x, PaddleYPosition, transform.position.z)).y;
		paddle = Instantiate(GameResources.GetPaddle (), new Vector3(0, adjustedY, transform.position.z), Quaternion.identity) as Paddle;
	}

	public void DestroyPaddle(){
		Destroy(paddle.gameObject);
		StopPlaceableAnimations();
	}

	public void LoadSpawner(){
		if (levelData.LevelReady()){
			// Load the Spawn Controller from Resources and Instantiate
			spawnController = Instantiate(GameResources.LoadSpawnController()) as SpawnController;
		}
	}

	public void StartSpawner(){
		spawnController.SpawnLevelBalls ();
	}

	public void StopSpawner(){
		spawnController.StopSpawner();
		spawnController.DestroyAllBalls();
	}
	/// <summary>
	/// Creates a new LevelData instance with default values.
	/// </summary>
	/// <returns>The level data.</returns>
	public LevelData InitializeLevelData(){
		levelData = new LevelData ();
		return levelData;
	}

	/// <summary>
	/// Sets LevelData equal to the data provided.
	/// </summary>
	/// <returns>The level data.</returns>
	/// <param name="levelData">Level data.</param>
	public LevelData InitializeLevelData(LevelData levelData){
		this.levelData = levelData;
		return levelData;
	}
	/// <summary>
	/// Creates wall and ceiling colliders, as well as a killzone floor collider
	/// </summary>
	public void GenerateEdgeColliders(){
		GameObject boundaries = GameObject.Find("Boundaries");
		if (boundaries != null){
			Destroy(boundaries);
		}
		GameObject boundaryContainer = new GameObject("Boundaries");
		Vector2 topLeft = Camera.main.ViewportToWorldPoint (new Vector3 (0, 1, 0));
		Vector2 topRight = Camera.main.ViewportToWorldPoint (new Vector3 (1, 1, 0));
		Vector2 bottomRight = Camera.main.ViewportToWorldPoint (new Vector3 (1, 0, 0));
		Vector2 bottomLeft = Camera.main.ViewportToWorldPoint (new Vector3 (0, 0, 0));
		Vector2[] wallColliderPoints = new Vector2[] {
			bottomLeft,
			topLeft,
			topRight,
			bottomRight
		};

		Vector2[] killZoneColliderPoints = new Vector2[] {
			bottomLeft,
			bottomRight
		};

		GameObject edgeColliderObject = new GameObject ("Edge Collider");
		EdgeCollider2D wallCollider = edgeColliderObject.AddComponent<EdgeCollider2D> ();
		wallCollider.points = wallColliderPoints;

		GameObject killZone = new GameObject ("Kill Zone");
		EdgeCollider2D killZoneCollider = killZone.AddComponent<EdgeCollider2D> ();
		killZoneCollider.points = killZoneColliderPoints;
		killZoneCollider.tag = "killzone";

		edgeColliderObject.transform.SetParent (boundaryContainer.transform);
		killZone.transform.SetParent (boundaryContainer.transform);
		boundaryContainer.transform.SetParent(transform);
	}
	public void StartPlaceableAnimations(){
		GameObject[] placeables = GameObject.FindGameObjectsWithTag ("placeable");
		foreach(var placeable in placeables){
			AnimatedObject animatedObject = placeable.GetComponent<AnimatedObject> ();
//			if (animatedObject == null){
//				animatedObject = placeable.AddComponent<AnimatedObject> ();
//				animatedObject.SetData (placeable.GetComponent<Placeable> ().pData.AnimationData);
//			}
			animatedObject.StartAnimation ();
		}
		animationStatus = LevelAnimationStatus.RUNNING;
	}

	public void StopPlaceableAnimations(){
		GameObject[] placeables = GameObject.FindGameObjectsWithTag ("placeable");
		foreach(var placeable in placeables){
			AnimatedObject animatedObject = placeable.GetComponent<AnimatedObject> ();
			animatedObject.StopAnimationAndReset ();
		}
		animationStatus = LevelAnimationStatus.STOPPED_OR_NONE;
	}
	public void PausePlaceableAnimations(){
		GameObject[] placeables = GameObject.FindGameObjectsWithTag ("placeable");
		foreach(var placeable in placeables){
			AnimatedObject animatedObject = placeable.GetComponent<AnimatedObject> ();
			animatedObject.PauseAnimation ();
		}
		animationStatus = LevelAnimationStatus.PAUSED;
	}

	public void ResumePlaceableAnimations(){
		GameObject[] placeables = GameObject.FindGameObjectsWithTag ("placeable");
		foreach(var placeable in placeables){
			AnimatedObject animatedObject = placeable.GetComponent<AnimatedObject> ();
			animatedObject.ResumeAnimation ();
		}
		animationStatus = LevelAnimationStatus.RUNNING;
	}

	public Placeable GetPleaceableByID(int id)
	{
		return GameObject.FindObjectsOfType<Placeable>().FirstOrDefault(e => e.ID == e.ID);
	}


	public Placeable GetSpawnPoint(){
		return GameObject.FindObjectsOfType<Placeable>().FirstOrDefault(placeable => placeable.pData.typeStr == "Ball Spawner");
	}
}

