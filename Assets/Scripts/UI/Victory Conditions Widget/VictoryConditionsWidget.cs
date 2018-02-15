using UnityEngine;
using UnityEngine.UI;
using System;
public class VictoryConditionsWidget : MonoBehaviour{
    public VictoryConditionData victoryConditionData;
    public Dropdown conditionSelect;
    public KeyGoalDisplay keyGoalDisplay;
    public PointGoalDisplay pointGoalDisplay;
    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        LevelManager.LevelLoaded += OnLevelLoaded;
    }
    /// <summary>
    /// This function is called when the MonoBehaviour will be destroyed.
    /// </summary>
    void OnDestroy()
    {
        LevelManager.LevelLoaded -= OnLevelLoaded;
    }
    public void SetVictoryConditionData(VictoryConditionData data){
        victoryConditionData = data;
    }
    /// <summary>
    /// Used in the Unity UI
    /// </summary>
    public void ShowPointDisplay(){
        keyGoalDisplay.gameObject.SetActive(false);
        pointGoalDisplay.gameObject.SetActive(true);
        pointGoalDisplay.SetGoalText(victoryConditionData.Goal);
    }

    /// <summary>
    /// Used in the Unity UI
    /// </summary>
    public void ShowKeyDisplay(){
        keyGoalDisplay.gameObject.SetActive(true);
        pointGoalDisplay.gameObject.SetActive(false);
        keyGoalDisplay.SetGoalText(victoryConditionData.Goal);
    }

    public void HandleConditionSelect(){
        if (conditionSelect.value == 0){
            ShowKeyDisplay();
        } else{
            ShowPointDisplay();
        }
    }

    public void OnLevelLoaded(object source, EventArgs e){
        Debug.Log("VictoryConditionsWidget notified of level load!");
        victoryConditionData = LevelManager.ActiveLevel.VictoryCondition;
        Debug.Log(victoryConditionData.Goal);
        keyGoalDisplay.SetGoalText(victoryConditionData.Goal);
    }
}