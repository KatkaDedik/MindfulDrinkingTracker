using System;
using UnityEngine;

public static class PromileCalculator
{
    private const float BETA = 0.12f;

    public static float CalculatePromileAtTime(DrinkingSessionModel session, DateTime time, UserProfile profile)
    {
        if (session == null)
            return 0f;

        float totalPromile = 0f;

        float genderQoucient = profile.Gender == Gender.Male ? 0.68f : 0.55f;

        foreach (var drink in session.Drinks)
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

    public static DateTime CalculateTimeForPromile(DrinkingSessionModel session, float targetPromile, UserProfile profile)
    {
        if (session == null || profile == null)
            return DateTime.Now;

        float currentPromile = CalculatePromileAtTime(session, DateTime.Now, profile);
        if (currentPromile <= targetPromile)
            return DateTime.Now;

        float minutesBeta = BETA / 60f;
        float minutesToSober = (currentPromile - targetPromile) / minutesBeta;

        return DateTime.Now.AddMinutes(minutesToSober);
    }

    public static DateTime CalculateSoberTime(DrinkingSessionModel session, UserProfile profile)
    {
        return CalculateTimeForPromile(session, 0f, profile);
    }

    public static DateTime CalculateTimeForNextDrink(DrinkingSessionModel session, DrinkDefinition drink, UserProfile profile)
    {
        if (session == null)
            return DateTime.Now;

        float drinkIncrease = CalculateTotalPromileAfterDrink(session, drink, profile);
        float maxPromile = session.DesiredMaxPromilePeak;
        float targetPromileBeforeDrink = maxPromile - drinkIncrease;

        // don't allow too strong drinks
        if (targetPromileBeforeDrink < 0)
            return DateTime.MaxValue;

        return CalculateTimeForPromile(session, targetPromileBeforeDrink, profile);
    }

    public static float CalculateTotalPromileAfterDrink(DrinkingSessionModel session, DrinkDefinition drink, UserProfile profile)
    {
        if (session == null || profile == null)
            return 0f;

        float currentPromile = CalculatePromileAtTime(session, DateTime.Now, profile);
        float r = profile.Gender == Gender.Male ? 0.68f : 0.55f;
        float drinkIncrease = drink.GetPureAlcoholGrams() / (profile.WeightKg * r);
        return currentPromile + drinkIncrease;
    }
}
