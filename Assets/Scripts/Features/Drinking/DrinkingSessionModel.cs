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
