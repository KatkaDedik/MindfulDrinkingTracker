using System;
using System.Collections.Generic;

public class DrinkingSession
{
    public DateTime DateTime;
    public DateTime? EndTime;

    public int MaxDrinks;
    public List<DrinkEntry> Drinks = new();
    public bool IsActive => EndTime == null;
    public int TotalDrinks => Drinks.Count;
    public int TotalWater = 0;
}

public class DrinkEntry
{
    public DateTime Time;
}

public class SessionConfig
{
    public DrinkingGoal Goal;

    public float? TargetPromile;
    public int? MaxDrinks;
    public int? SoberByHour;
}
