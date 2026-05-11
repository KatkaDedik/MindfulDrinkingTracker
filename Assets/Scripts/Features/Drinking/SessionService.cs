using System;

using UnityEngine;

public class SessionService
{
    private readonly SessionState _sessionState;
    private readonly DrinkingSessionFactory _drinkingSessionFactory = new();
    private DrinkDatabase _drinkDatabase;

    public DrinkingGoal CurrentGoal => _sessionState.CurrentGoal;

    public event Action OnSessionUpdated;

    public SessionService(SessionState state, DrinkDatabase drinkDatabase)
    {
        _sessionState = state;
        _drinkDatabase = drinkDatabase;
    }

    public void StartSession(SessionConfig config)
    {
        if (_sessionState.CurrentDrinkingSession != null && _sessionState.CurrentDrinkingSession.IsActive)
        {
            Debug.LogWarning("A session is already active.");
            _sessionState.CurrentDrinkingSession = null;
            SessionRepository.ClearActiveSession();
            return;
        }

        _sessionState.CurrentGoal = config.Goal;
        _sessionState.CurrentDrinkingSession = _drinkingSessionFactory.Create(config);

        SessionRepository.Save(_sessionState.CurrentDrinkingSession);
        OnSessionUpdated?.Invoke();
    }

    public void LoadSession()
    {
        DrinkingSessionModel drinkingSessionModel = SessionRepository.Load(_drinkDatabase);
        if (drinkingSessionModel != null)
        {
            SetCurrentDrinkingSession(drinkingSessionModel);
        }
        SessionRepository.ClearActiveSession();
    }

    public void EndSession()
    {
        SessionRepository.ClearActiveSession();
    }

    public void AddDrink(DrinkDefinition drink)
    {
        if (_sessionState.CurrentDrinkingSession == null || !_sessionState.CurrentDrinkingSession.IsActive)
        {
            Debug.LogWarning("No active session.");
            return;
        }
        _sessionState.CurrentDrinkingSession.Drinks.Add(new DrinkEntryModel
        {
            Time = System.DateTime.Now,
            Drink = drink
        });
        Debug.Log($"Added drink {drink.name}");
        SessionRepository.Save(_sessionState.CurrentDrinkingSession);
        OnSessionUpdated?.Invoke();
    }

    public DrinkingSessionModel GetCurrentSession()
    {
        if (_sessionState.CurrentDrinkingSession == null)
        {
            Debug.LogWarning("No session data available after loading.");
            return null;
        }
        return _sessionState.CurrentDrinkingSession;
    }

    public void SetCurrentDrinkingSession(DrinkingSessionModel drinkingSession)
    {
        _sessionState.CurrentDrinkingSession = drinkingSession;
        _sessionState.CurrentGoal = drinkingSession.CurrentGoal;
    }
}
