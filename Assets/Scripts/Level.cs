using UnityEngine;
using System.Collections;

public class Level : MonoBehaviour
{
	public Paddle paddle;
	public const float PaddleYPosition = 0.05f;
	public LevelData levelData;
	public void LoadPaddle(){
		float adjustedY = Camera.main.ViewportToWorldPoint ( new Vector3(transform.position.x, PaddleYPosition, transform.position.z)).y;

		paddle = Instantiate(GameResources.GetPaddle (), new Vector3(0, adjustedY, transform.position.z), Quaternion.identity) as Paddle;
	}

	public LevelData InitializeLevelData(){
		return levelData = new LevelData ();
	}

	/// <summary>
	/// Creates wall and ceiling colliders, as well as a killzone floor collider
	/// </summary>
	public void GenerateEdgeColliders(){
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
	}

	void Update ()
	{
	}
}

