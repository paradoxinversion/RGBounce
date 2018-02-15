using UnityEngine;
using UnityEngine.UI;
public class KeyGoalDisplay : MonoBehaviour{
    public Text totalKeys;
    
    public void SetGoalText(int goal){
        totalKeys.text = string.Format("Keys: {0}", goal);
    }
}