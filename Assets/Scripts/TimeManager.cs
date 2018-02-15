using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour {
	public enum TimeState
	{
		NORMAL,
		FAST,
		SLOW
	}
	static TimeState timeState;

	public static void SetTimeState(TimeState state){
		timeState = state;
	}
	public static void SlowTime(TimeManager tm){
		if (timeState != TimeState.SLOW){
			tm.StartCoroutine(PlaceableEffects.SlowTime());
		}
	}
	
	public static void SpeedTime(TimeManager tm){
		if (timeState != TimeState.FAST){
			tm.StartCoroutine(PlaceableEffects.SpeedTime());
		}
	}
}
