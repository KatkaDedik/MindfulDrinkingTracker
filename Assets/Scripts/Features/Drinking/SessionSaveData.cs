using System.Collections.Generic;

[System.Serializable]
public class DrinkingSessionSaveData
{
    public string StartDateTime;
    public string EndTime;

    public DrinkingGoal CurrentGoal;

    public int MaxDrinks;

    public List<DrinkEntrySaveData> Drinks;

    public int TotalWater;

    public float DesiredMaxPromilePeak;

    public DrinkingSessionSaveData(DrinkingSessionModel model)
    {
        StartDateTime = model.StartDateTime.ToString("O");
        EndTime = model.EndTime.ToString();
        CurrentGoal = model.CurrentGoal;
        MaxDrinks = model.MaxDrinks;
        Drinks = new();
        foreach (var drink in model.Drinks)
        {
            Drinks.Add(new DrinkEntrySaveData(drink.Time.ToString("O"), drink.Drink.ID));
        }
        TotalWater = model.TotalWater;
        DesiredMaxPromilePeak = model.DesiredMaxPromilePeak;
    }
}

[System.Serializable]
public class DrinkEntrySaveData
{
    public string Time;

    public int DrinkId;

    public DrinkEntrySaveData(string time, int id)
    {
        Time = time;
        DrinkId = id;
    }
}
