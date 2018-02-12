using UnityEngine;

public class Paddle : MonoBehaviour, IColorChanger {
	public PaddleData paddle;
	public SpriteRenderer sr { get; private set; }
	public float yPosition;
	private void Start(){
		paddle = new PaddleData();

		sr = GetComponent<SpriteRenderer>();
		paddle.ColorMode = 0;
		Debug.Log("paddle initialized");
	}
	/// <summary>
	/// Directly sets the Y position for the Paddle
	/// </summary>
	/// <param name="yPos">The Y position to place the Paddle</param>
	public void SetYPosition(float yPos){
		yPosition = yPos;
	}
	// <summary>
	// Handles keys for changing paddle color
	// </summary>
	public void ModeSelect(){

	}
	void RepelBall(Collision2D ballCollision)
	{
		if (paddle.ColorMode == 2){
			ballCollision.rigidbody.AddForce(transform.up * paddle.lowPowerForce);
		}
		else if (paddle.ColorMode == 0){
			ballCollision.rigidbody.AddForce(transform.up * paddle.highPowerForce);
		}
		else if (paddle.ColorMode == 1){
			ballCollision.rigidbody.AddForce(transform.up * paddle.midPowerForce);
		}
	}
	public void OnCollisionEnter2D(Collision2D collision){
		if (collision.gameObject.tag == "ball"){
			
			collision.rigidbody.AddForce(new Vector2(paddle.horizontalForce * paddle.horizontalForceMultiplier, 1f));
			RepelBall(collision);
		}
	}

	void MovePaddle(){
		paddle.mousePosition = Input.mousePosition;
		Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(paddle.mousePosition);
		float positionDifference = (paddle.mousePosition.x - paddle.previousPosition.x);
		handlePaddleRotation(positionDifference); 
		paddle.horizontalForce = positionDifference;
		float adjustedY = Camera.main.ViewportToWorldPoint ( new Vector3(transform.position.x, yPosition, transform.position.z)).y;
		this.transform.position = new Vector3(mouseWorldPosition.x, adjustedY, this.transform.position.z);
		paddle.previousPosition = paddle.mousePosition;

	}

	/// <summary>
	/// Applies rotation to the paddle based on the difference in mouse position between frames.
	/// </summary>
	/// <param name="mousePositionDifference">The difference in X position between the current and previous frame</param>
	void handlePaddleRotation(float mousePositionDifference){
		float rotationAngle = -Mathf.Clamp(mousePositionDifference, -paddle.maxTilt, paddle.maxTilt);
		Quaternion targetRotation = Quaternion.Euler(0, 0, rotationAngle);
		transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 25.0f);
	}
	void Update(){
		paddle.ModeSelect();
		if (paddle.paddleActive){
			MovePaddle();
		}
	}
}

