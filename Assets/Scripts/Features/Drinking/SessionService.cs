using System;

using UnityEngine;

public class SessionService
{
    private readonly SessionState _sessionState;
    private readonly SessionRepository _sessionRepository;
    private readonly DrinkingSessionFactory _drinkingSessionFactory = new();

    public DrinkingGoal CurrentGoal => _sessionState.CurrentGoal;

    public event Action OnSessionUpdated;

    public SessionService(SessionState state)
    {
        _sessionState = state;
        _sessionRepository = new SessionRepository();
    }

    public void StartSession(SessionConfig config)
    {
        if (_sessionState.CurrentDrinkingSession != null && _sessionState.CurrentDrinkingSession.IsActive)
        {
            Debug.LogWarning("A session is already active.");
            return;
        }

        _sessionState.CurrentGoal = config.Goal;
        _sessionState.CurrentDrinkingSession = _drinkingSessionFactory.Create(config);

        _sessionRepository.Save(_sessionState.CurrentDrinkingSession);
        OnSessionUpdated?.Invoke();
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
        _sessionRepository.Save(_sessionState.CurrentDrinkingSession);
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
}
