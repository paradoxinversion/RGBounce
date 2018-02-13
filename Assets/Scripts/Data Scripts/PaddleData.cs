using UnityEngine;
using System.Collections;

[System.Serializable]
public class PaddleData
{

	public Vector3 mousePosition;
	public Vector3 previousPosition;
	public float highPowerForce = 100;
	public float midPowerForce = 65;
	public float lowPowerForce = 20;
	public float horizontalForce;
	public float horizontalForceMultiplier = 1f;
	public float maxTilt = 45f;
	public float tiltTimeMultiplier = 25f;

	/// <summary>
	/// What color mode is the paddle in?
	/// </summary>
	private int colorMode;

	public int ColorMode{
		get{
			return colorMode;
		}

		set{
			colorMode = value;
		}
	}
	/// <summary>
	/// Paddle only responds when true.
	/// </summary>
	public bool paddleActive = true;

	/// <summary>
	/// Handles keys for changing paddle color
	/// </summary>
	public void ModeSelect(){

		if (Input.GetKeyDown(KeyCode.Alpha1)){
			colorMode = 0;
			//			ObjectColorHandler.SetColor(this, 0);
		}
		else if (Input.GetKeyDown(KeyCode.Alpha2)){
			colorMode = 1;
			//			ObjectColorHandler.SetColor(this, 1);
		}
		else if (Input.GetKeyDown(KeyCode.Alpha3)){
			colorMode = 2;
			//			ObjectColorHandler.SetColor(this, 2);
		}
	}

}

