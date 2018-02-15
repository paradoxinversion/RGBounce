using UnityEngine;
using System.Collections;
public static class PlaceableEffects{
    public static void Accelerate(Transform parentTranform, Collider2D other){
        other.GetComponent<Rigidbody2D>().AddForce(parentTranform.up * 320);
    }

    public static void Explode(Transform parentTranform, Collider2D other){
        ExplosionForce2D.AddExplosionForce(other.GetComponent<Rigidbody2D>(), 3000, parentTranform.position, 1);
    }

    public static void Pull(Transform parentTranform, Collider2D other){
        ExplosionForce2D.AddExplosionForce(other.GetComponent<Rigidbody2D>(), -640, parentTranform.position, (parentTranform.localScale.x)*1.5f) ;
    }

    public static void GrantPoints(Transform parentTransform, Collider2D other){
        Debug.Log("Gained Points");
    }

    public static IEnumerator SlowTime(){
        Time.timeScale = 0.5f;
        TimeManager.SetTimeState(TimeManager.TimeState.SLOW);
        yield return new WaitForSecondsRealtime(5);
        TimeManager.SetTimeState(TimeManager.TimeState.NORMAL);

        Time.timeScale = 1.0f;
    }
    public static IEnumerator SpeedTime(){
        Time.timeScale = 2.0f;
        TimeManager.SetTimeState(TimeManager.TimeState.FAST);
        yield return new WaitForSecondsRealtime(5);
        TimeManager.SetTimeState(TimeManager.TimeState.NORMAL);
        Time.timeScale = 1.0f;
    }

    public static void GetKey(Transform parentTranform, Collider2D other){
        LevelManager.ActiveLevel.GetKey();
        GameObject.Destroy(parentTranform.gameObject);
    }
}