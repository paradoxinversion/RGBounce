using UnityEngine;
using UnityEngine.UI;
public class PointGoalDisplay : MonoBehaviour{
    public InputField pointGoal;
    
    public void SetGoalText(int goal){
        pointGoal.text = goal.ToString();
    }
}