using System;
using UnityEngine;

public class SessionService
{
    private DrinkingSession _currentSession;

    public event Action OnDrinkAdded;
    public event Action OnWaterAdded;
    public event Action<DrinkingSession> OnSessionEnded;
    public void StartSession(SessionConfig config)
    {
        if (_currentSession != null && _currentSession.IsActive)
        {
            Debug.LogWarning("A session is already active. Ending the current session before starting a new one.");
            EndSession();
        }
        _currentSession = new DrinkingSession
        {
            DateTime = System.DateTime.Now,
            MaxDrinks = 0
        };
    }

    public void AddDrink()
    {
        if (_currentSession == null || !_currentSession.IsActive)
        {
            Debug.LogWarning("No active session. Please start a session before adding drinks.");
            return;
        }
        _currentSession.Drinks.Add(new DrinkEntry { Time = System.DateTime.Now });
        OnDrinkAdded?.Invoke();
    }

    public void AddWater()
    {
        if (_currentSession == null || !_currentSession.IsActive)
        {
            Debug.LogWarning("No active session. Please start a session before adding drinks.");
            return;
        }
        _currentSession.TotalWater++;
        OnWaterAdded?.Invoke();
    }

    public void EndSession()
    {
        if (_currentSession == null || !_currentSession.IsActive)
        {
            Debug.LogWarning("No active session to end.");
            return;
        }
        _currentSession.EndTime = System.DateTime.Now;
        OnSessionEnded?.Invoke(_currentSession);
    }

    public bool IsOverLimit()
    {
        if (_currentSession == null)
        {
            Debug.LogWarning("No session data available.");
            return false;
        }
        return _currentSession.TotalDrinks > _currentSession.MaxDrinks;
    }

    public int GetTotalWater()
    {
        if (_currentSession == null)
        {
            Debug.LogWarning("No session data available.");
            return 0;
        }
        return _currentSession.TotalWater;
    }

    public int GetTotalDrinks()
    {
        if (_currentSession == null)
        {
            Debug.LogWarning("No session data available.");
            return 0;
        }
        return _currentSession.TotalDrinks;
    }

    public int GetMaxDrinks()
    {
        if (_currentSession == null)
        {
            Debug.LogWarning("No session data available.");
            return 0;
        }
        return _currentSession.MaxDrinks;
    }
}
