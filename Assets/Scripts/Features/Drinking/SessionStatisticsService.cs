using UnityEngine;

public class SessionStatisticsService
{
    private readonly SessionState _sessionState;

    public SessionStatisticsService(SessionState state)
    {
        _sessionState = state;
    }

    public bool IsOverLimit()
    {
        if (_sessionState.CurrentDrinkingSession == null)
        {
            Debug.LogWarning("No session data available.");
            return false;
        }
        return _sessionState.CurrentDrinkingSession.TotalDrinks > _sessionState.CurrentDrinkingSession.MaxDrinks;
    }

    public int GetTotalWater()
    {
        if (_sessionState.CurrentDrinkingSession == null)
        {
            Debug.LogWarning("No session data available.");
            return 0;
        }
        return _sessionState.CurrentDrinkingSession.TotalWater;
    }

    public int GetTotalDrinks()
    {
        if (_sessionState.CurrentDrinkingSession == null)
        {
            Debug.LogWarning("No session data available.");
            return 0;
        }
        return _sessionState.CurrentDrinkingSession.TotalDrinks;
    }

    public int GetMaxDrinks()
    {
        if (_sessionState.CurrentDrinkingSession == null)
        {
            Debug.LogWarning("No session data available.");
            return 0;
        }
        return _sessionState.CurrentDrinkingSession.MaxDrinks;
    }
}
