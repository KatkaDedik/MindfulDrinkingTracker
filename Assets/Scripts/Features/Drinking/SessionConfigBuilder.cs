
public class SessionConfigBuilder
{
    public DrinkingGoal SelectedGoal { get; private set; } = DrinkingGoal.None;

    public float TargetPromile { get; private set; }
    public int MaxDrinks { get; private set; }
    public int SoberByHour { get; private set; }

    public void SelectGoal(DrinkingGoal goal)
    {
        SelectedGoal = goal;

        TargetPromile = 0;
        MaxDrinks = 0;
        SoberByHour = 0;
    }

    public void SetPromile(float value) => TargetPromile = value;
    public void SetMaxDrinks(int value) => MaxDrinks = value;
    public void SetSoberBy(int value) => SoberByHour = value;

    public SessionConfig BuildConfig()
    {
        return new SessionConfig
        {
            Goal = SelectedGoal,
            TargetPromile = SelectedGoal == DrinkingGoal.StayInControl ? TargetPromile : null,
            MaxDrinks = SelectedGoal == DrinkingGoal.LimitDrinks ? MaxDrinks : null,
            SoberByHour = SelectedGoal == DrinkingGoal.DriveTomorrow ? SoberByHour : null
        };
    }
}
