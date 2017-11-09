using UnityEngine;

public class Paddle : MonoBehaviour, IColorChanger
{
	public PaddleData paddle;
	public SpriteRenderer sr { get; private set; }
	public Vector3 test;
	public float yPosition;
	private void Start()
	{
		paddle = new PaddleData();

		sr = GetComponent<SpriteRenderer>();
		paddle.ColorMode = 0;
	}
	public void SetYPosition(float yPos){
		yPosition = yPos;
	}
	// <summary>
	// Handles keys for changing paddle color
	// </summary>
	public void ModeSelect()
	{

	}
	void RepelBall(Collision2D ballCollision)
	{
		if (paddle.ColorMode == 2)
		{
			ballCollision.rigidbody.AddForce(transform.up * paddle.lowPowerForce);
		}
		else if (paddle.ColorMode == 0)
		{
			ballCollision.rigidbody.AddForce(transform.up * paddle.highPowerForce);
		}
		else if (paddle.ColorMode == 1)
		{
			ballCollision.rigidbody.AddForce(transform.up * paddle.midPowerForce);
		}
	}
	public void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "Ball")
		{
			RepelBall(collision);
			collision.rigidbody.AddForce(new Vector2(paddle.horizontalForce * paddle.horizontalForceMultiplier, 1f));
		}
	}

	void MovePaddleNoTilt()
	{
		//Handle left-and-right movement
		paddle.mousePosition = Input.mousePosition;
		Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(paddle.mousePosition);
		float positionDifference = (paddle.mousePosition.x - paddle.previousPosition.x);

		paddle.horizontalForce = positionDifference;
		float adjustedY = Camera.main.ViewportToWorldPoint ( new Vector3(transform.position.x, yPosition, transform.position.z)).y;
		this.transform.position = new Vector3(mouseWorldPosition.x, adjustedY, this.transform.position.z);
		paddle.previousPosition = paddle.mousePosition;


//		Debug.Log (Camera.main.ScreenToWorldPoint (Input.mousePosition));
	}

	void Update()
	{

		paddle.ModeSelect();
		if (paddle.paddleActive)
		{
			MovePaddleNoTilt();
		}
//		this.transform.position = new Vector3(transform.position.x, Camera.main.ViewportToWorldPoint(test).y, transform.position.z);
//		SetYPosition(test.y);
	}
}

