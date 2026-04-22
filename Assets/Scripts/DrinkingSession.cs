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
}

public class DrinkEntry
{
    public DateTime Time;
}