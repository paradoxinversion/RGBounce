using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour
{
	public int mode = 0;
	public int ballID;
	public bool metal;
	public float lifetime;

	void FixedUpdate()
	{
		///Apply opposite force to the ball if it is going too fast
		if (GetComponent<Rigidbody2D>().velocity.sqrMagnitude > 150)
		{
			GetComponent<Rigidbody2D>().AddForce(new Vector2((-GetComponent<Rigidbody2D>().velocity.x) * 2, (-GetComponent<Rigidbody2D>().velocity.y) * 2));
		}
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		//Play bounce sound if impacting a paddle
		if (collision.gameObject.tag == "Paddle")
		{
			gameObject.GetComponent<AudioSource>().Play();
		}
		else if (collision.gameObject.tag == "Ball")
		{
			//check if balls are the same color and combine them if they are

		}
	}


	void Update()
	{
		lifetime += Time.deltaTime;
	}
}
