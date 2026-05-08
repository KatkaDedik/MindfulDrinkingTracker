public class SessionState
{
    public DrinkingSessionModel CurrentDrinkingSession;
    public DrinkingGoal CurrentGoal;

    public bool HasActiveSession =>
        CurrentDrinkingSession != null &&
        CurrentDrinkingSession.IsActive;
}
