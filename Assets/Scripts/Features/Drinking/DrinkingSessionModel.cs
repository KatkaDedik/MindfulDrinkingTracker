using System;
using System.Collections.Generic;

public class DrinkingSessionModel
{
    public DateTime StartDateTime;
    public DateTime? EndTime;
    public DrinkingGoal CurrentGoal;

    public int MaxDrinks;
    public List<DrinkEntryModel> Drinks = new();
    public bool IsActive => EndTime == null;
    public int TotalDrinks => Drinks.Count;
    public int TotalWater = 0;
    public float DesiredMaxPromilePeak = 0f;

    public DrinkingSessionModel() { }

    public DrinkingSessionModel(
    DrinkingSessionSaveData saveData,
    DrinkDatabase drinkDatabase)
    {
        StartDateTime =
            DateTime.Parse(saveData.StartDateTime);

        if (!string.IsNullOrEmpty(saveData.EndTime))
        {
            EndTime =
                DateTime.Parse(saveData.EndTime);
        }

        CurrentGoal = saveData.CurrentGoal;

        MaxDrinks = saveData.MaxDrinks;

        TotalWater = saveData.TotalWater;

        DesiredMaxPromilePeak =
            saveData.DesiredMaxPromilePeak;

        Drinks = new List<DrinkEntryModel>();

        foreach (var drinkSaveData in saveData.Drinks)
        {
            Drinks.Add(new DrinkEntryModel
            {
                Time = DateTime.Parse(drinkSaveData.Time),

                Drink = drinkDatabase.GetDrinkById(
                    drinkSaveData.DrinkId)
            });
        }
    }
}

public class DrinkEntryModel
{
    public DateTime Time;
    public DrinkDefinition Drink;
}

public class SessionConfig
{
    public DrinkingGoal Goal;

    public float? TargetPromile;
    public int? MaxDrinks;
    public int? SoberByHour;
}
