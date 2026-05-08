using System;
using UnityEngine;

public class SessionPromileService
{
    private readonly SessionState _state;

    public SessionPromileService(SessionState state)
    {
        _state = state;
    }

    public float GetCurrentPromile()
    {
        if (_state.CurrentDrinkingSession == null)
            return 0;

        var profile = ProfileService.LoadProfile();
        return PromileCalculator.CalculatePromileAtTime(_state.CurrentDrinkingSession, DateTime.Now, profile);
    }

    public float GetPromileForDrinkPreview(DrinkDefinition drink)
    {
        if (_state.CurrentDrinkingSession == null)
            return 0f;

        var profile = ProfileService.LoadProfile();

        float current = PromileCalculator.CalculatePromileAtTime(_state.CurrentDrinkingSession, DateTime.Now, profile);

        float after = PromileCalculator.CalculateTotalPromileAfterDrink(_state.CurrentDrinkingSession, drink, profile);

        return after - current;
    }

    public float GetMinutesUntilDrinkAllowed(DrinkDefinition drink)
    {
        if (_state.CurrentDrinkingSession == null)
            return 0f;
        var profile = ProfileService.LoadProfile();

        DateTime nextTime = PromileCalculator.CalculateTimeForNextDrink(_state.CurrentDrinkingSession, drink, profile);
        return Mathf.Max(0, (float)(nextTime - DateTime.Now).TotalMinutes);
    }

    public DateTime CalculateSoberTime()
    {
        var profile = ProfileService.LoadProfile();
        return PromileCalculator.CalculateSoberTime(_state.CurrentDrinkingSession, profile);
    }
}

