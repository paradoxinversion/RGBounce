/// <summary>
/// Keeps track of what conditions are required for victory and how close the player is to reaching those conditions.
/// </summary>
public class VictoryConditionData{
    public enum Condition {
        KEYS,
        POINTS
    }
    private Condition victoryCondition;
    
    /// <summary>
    /// How many points or keys are require to win the round.
    /// </summary>
    private int goal;
    private int currentProgress = 0;
    private bool victoryConditionAchieved = false;
    /// <summary>
    /// Whether or not the player has reached the goal.
    /// </summary>
    /// <returns>True if the current progress amount matches the goal amount.</returns>
    public bool VictoryConditionAchieved {
        get {
            return victoryConditionAchieved;
        }
    }

    public Condition VictoryCondition
    {
        get
        {
            return victoryCondition;
        }
    }

    public int Goal
    {
        get
        {
            return goal;
        }
    }

    public VictoryConditionData(Condition victoryCondition = Condition.KEYS, int goal = 1){
        this.victoryCondition = victoryCondition;
        this.goal = goal;
    }

    public void RaiseValue(int amt){
        currentProgress++;
        if (currentProgress == goal){
            victoryConditionAchieved = true;
        }
    }

    public void SetKeyGoal(LevelData level){
        int goal = level.GatherKeys();
    }
}