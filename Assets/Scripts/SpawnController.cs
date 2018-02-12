using UnityEngine;
using System.Collections;
using System;
public class SpawnController : MonoBehaviour {
	private static SpawnController _instance = null;
	public static SpawnController Get()
	{
		if (_instance == null)
		{
			_instance = (SpawnController)FindObjectOfType(typeof(SpawnController));
		}

		return _instance;
	}

//	public GameObject ballPrefab;
	private int ballsCreated;
	private int ballsDestroyed;
	private int ballsCombined;
	public float ballScale = 1;
	public float warmUp;

	public void OnBallDestroyed(Ball ball)
	{
		Debug.Log("Ball " + ball.ballID + " destroyed.");
		Destroy(ball.gameObject);
		ballsDestroyed++;
//		SessionManager.Get().result.levelBallsDestroyed++;
		if (ballsCombined + ballsDestroyed >= ballsCreated)
		{
			SpawnBall();
		}
	}
	public void OnBallCombined(Ball ball)
	{
		Vector2 ballPosition = ball.transform.position;
		Debug.Log("Ball " + ball.ballID + " combined.");
		Destroy(ball.gameObject);
		ballsCombined++;
//		SessionManager.Get().result.ballsCombined++;
		if (ballsCombined + ballsDestroyed >= ballsCreated)
		{
			SpawnBall(ballPosition);
		}
	}

	/// <summary>
	/// Called when balls are removed from play, but not dropped or combined
	/// </summary>
	/// <param name="ball"></param>
	public void OnBallRemoved(Ball ball)
	{
		Debug.Log("Ball " + ball.ballID + " removed.");
		Destroy(ball.gameObject);

	}
	public void OnBallCreated(GameObject ballGameObject)
	{
		ballGameObject.GetComponent<Ball>().mode = UnityEngine.Random.Range(1, 4);

		ballGameObject.GetComponent<Ball>().ballID = ballsCreated;
		ballsCreated++;
//		SessionManager.Get().result.levelBallsCreated++;

	}

	public void DestroyAllBalls()
	{

		Ball[] balls = GameObject.FindObjectsOfType<Ball>();
		for (int x = balls.Length-1; x > -1; x--)
		{
			OnBallRemoved(balls[x].gameObject.GetComponent<Ball>());
		}
	}
	// Use this for initialization
	void Start () {
		Debug.Log("Spawn Controller Started");
		//StartCoroutine("SpawnTestBalls");
	}


	public void SpawnBall()
	{
//		GameObject ballPrefab = Resources.Load<Ball> ("Ball").gameObject as GameObject;
//		GameObject newBall = Instantiate(ballPrefab, transform.position, Quaternion.identity) as GameObject;
		GameObject newBall = Instantiate(GameResources.LoadBall()) as GameObject;
		OnBallCreated(newBall);
	}

	public void SpawnBall(Vector2 location)
	{
		GameObject ballPrefab = Resources.Load<Ball> ("Ball").gameObject as GameObject;

		GameObject newBall = Instantiate(ballPrefab, location, Quaternion.identity) as GameObject;
		OnBallCreated(newBall);
	}

	public void SpawnBall(Vector2 location, float scale)
	{
		GameObject ballPrefab = Resources.Load<Ball> ("Ball").gameObject as GameObject;

		GameObject newBall = Instantiate(ballPrefab, location, Quaternion.identity) as GameObject;
		newBall.transform.localScale = new Vector2(scale, scale);
		OnBallCreated(newBall);
	}

	public void SpawnLevelBalls()
	{
		StartCoroutine("COSpawnLevelBalls");
	}

	public void Reset()
	{
		ballsCreated = 0;
		ballsDestroyed = 0;
		ballsCombined = 0;
	}

	public IEnumerator SpawnTestBalls()
	{
		while (true)
		{
			// Spawn the initial ball
			if (ballsCreated == 0)
			{
				SpawnBall(Vector2.zero, ballScale);
			}

			// if there are no balls, spawn one
			if (ballsDestroyed == ballsCreated)
			{
				SpawnBall(Vector2.zero, ballScale);
			}
			yield return null;
		}

	}
	public IEnumerator COSpawnLevelBalls()
	{
		Debug.Log ("Starting spawn sequence");
		transform.position = LevelManager.currentLevelGameObject.GetSpawnPoint().transform.position;
		yield return new WaitForSeconds(warmUp);
		while (true)
		{
			// Spawn the initial ball
			if (ballsCreated == 0)
			{
				Debug.Log ("Spawning ball");
				SpawnBall();
			}

			// if there are no balls, spawn one
			if (ballsDestroyed == ballsCreated)
			{
				SpawnBall();
			}
			yield return null;
		}

	}
}
