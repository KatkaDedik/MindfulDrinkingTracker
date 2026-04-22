using UnityEngine;

public class SessionService
{
    private DrinkingSession currentSession;
    
    public void StartSession(int maxDrinks)
    {
        if (currentSession != null && currentSession.IsActive)
        {
            Debug.LogWarning("A session is already active. Ending the current session before starting a new one.");
            EndSession();
        }
        currentSession = new DrinkingSession
        {
            DateTime = System.DateTime.Now,
            MaxDrinks = maxDrinks
        };
    }

    public void AddDrink()
    {
        if (currentSession == null || !currentSession.IsActive)
        {
            Debug.LogWarning("No active session. Please start a session before adding drinks.");
            return;
        }
        currentSession.Drinks.Add(new DrinkEntry { Time = System.DateTime.Now });
    }

    public void EndSession()
    {
        if (currentSession == null || !currentSession.IsActive)
        {
            Debug.LogWarning("No active session to end.");
            return;
        }
        currentSession.EndTime = System.DateTime.Now;
    }

    public bool IsOverLimit()
    {
        if (currentSession == null)
        {
            Debug.LogWarning("No session data available.");
            return false;
        }
        return currentSession.TotalDrinks > currentSession.MaxDrinks;
    }

    public int GetTotalDrinks()
    {
        if (currentSession == null)
        {
            Debug.LogWarning("No session data available.");
            return 0;
        }
        return currentSession.TotalDrinks;
    }

    public int GetMaxDrinks()
    {
        if (currentSession == null)
        {
            Debug.LogWarning("No session data available.");
            return 0;
        }
        return currentSession.MaxDrinks;
    }
}
