using System;

using UnityEngine;

public class SessionService
{
    private DrinkingSession _currentSession;

    public event Action<DrinkDefinition> OnDrinkAdded;
    public event Action OnWaterAdded;
    public event Action<DrinkingSession> OnSessionEnded;

    public DrinkingGoal CurrentGoal { get; private set; }
    private const float BETA = 0.12f; // Average alcohol elimination rate per hour

    public void StartSession(SessionConfig config)
    {
        if (_currentSession != null && _currentSession.IsActive)
        {
            Debug.LogWarning("A session is already active. Ending the current session before starting a new one.");
            EndSession();
        }

        _currentSession = new DrinkingSession
        {
            DateTime = DateTime.Now,
            MaxDrinks = 0,
            DesiredMaxPromilePeak = 0f
        };

        CurrentGoal = config.Goal;

        switch (config.Goal)
        {
            case DrinkingGoal.StayInControl:
                if (config.TargetPromile.HasValue)
                    _currentSession.DesiredMaxPromilePeak = config.TargetPromile.Value;
                break;

            case DrinkingGoal.LimitDrinks:
                if (config.MaxDrinks.HasValue)
                    _currentSession.MaxDrinks = config.MaxDrinks.Value;
                break;

            case DrinkingGoal.DriveTomorrow:
                if (config.SoberByHour.HasValue)
                {
                    var targetTime = DateTime.Now.Date.AddHours(config.SoberByHour.Value);

                    // if it is tomorrow
                    if (targetTime < DateTime.Now)
                        targetTime = targetTime.AddDays(1);

                    // what max promile you can afford now
                    float beta = 0.12f;
                    float hours = (float)(targetTime - DateTime.Now).TotalHours;

                    _currentSession.DesiredMaxPromilePeak = beta * hours;
                }
                break;

            case DrinkingGoal.JustTrack:
            case DrinkingGoal.None:
            default:
                // we don't set anything
                break;
        }
    }

    public void AddDrink(DrinkDefinition drink)
    {
        if (_currentSession == null || !_currentSession.IsActive)
        {
            Debug.LogWarning("No active session.");
            return;
        }
        _currentSession.Drinks.Add(new DrinkEntry
        {
            Time = System.DateTime.Now,
            Drink = drink
        });
        Debug.Log($"Added drink {drink.name}");
        OnDrinkAdded?.Invoke(drink);
    }

    public DrinkingSession GetCurrentSession()
    {
        return _currentSession;
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

    public float GetCurrentPromile()
    {
        if (_currentSession == null)
            return 0;

        return CalculatePromileAtTime(DateTime.Now);
    }

    public float CalculatePromileAtTime(DateTime time)
    {
        if (_currentSession == null)
            return 0f;

        var profile = ProfileService.LoadProfile();

        float totalPromile = 0f;

        float genderQoucient = profile.Gender == Gender.Male ? 0.68f : 0.55f;

        foreach (var drink in _currentSession.Drinks)
        {
            if (drink.Time > time)
                continue;

            float hours = (float)(time - drink.Time).TotalHours;

            float alcoholGrams = drink.Drink.GetPureAlcoholGrams();

            float peak = alcoholGrams / (profile.WeightKg * genderQoucient);

            float remaining = peak - (BETA * hours);

            if (remaining > 0)
                totalPromile += remaining;
        }

        return Mathf.Max(totalPromile, 0f);
    }

    public DateTime CalculateTimeForPromile(float targetPromile)
    {
        if (_currentSession == null)
            return DateTime.Now;

        float currentPromile = CalculatePromileAtTime(DateTime.Now);
        if (currentPromile <= targetPromile)
            return DateTime.Now;

        float minutesBeta = BETA / 60f;
        float minutesToSober = (currentPromile - targetPromile) / minutesBeta;

        return DateTime.Now.AddMinutes(minutesToSober);
    }

    public DateTime CalculateSoberTime()
    {
        return CalculateTimeForPromile(0f);
    }

    public DateTime CalculateTimeForNextDrink(DrinkDefinition drink)
    {
        if (_currentSession == null)
            return DateTime.Now;

        float drinkIncrease = CalculatePromileAfterDrink(drink);
        float maxPromile = _currentSession.DesiredMaxPromilePeak;
        float targetPromileBeforeDrink = maxPromile - drinkIncrease;

        // don't allow too strong drinks
        if (targetPromileBeforeDrink < 0)
            return DateTime.MaxValue;

        return CalculateTimeForPromile(targetPromileBeforeDrink);
    }

    public float CalculatePromileAfterDrink(DrinkDefinition drink)
    {
        if (_currentSession == null)
            return 0f;
        float currentPromile = GetCurrentPromile();
        var profile = ProfileService.LoadProfile();
        float r = profile.Gender == Gender.Male ? 0.68f : 0.55f;
        float drinkIncrease = drink.GetPureAlcoholGrams() / (profile.WeightKg * r);
        return currentPromile + drinkIncrease;
    }
}
