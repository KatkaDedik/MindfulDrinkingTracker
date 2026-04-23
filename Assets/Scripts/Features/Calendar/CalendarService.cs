using System;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class DayData
{
    public string Date;
    public bool IsOverLimit;
    public float AlcoholRatio;
    public float WaterRatio;
    public int MaxDrink;
    public int TotalDrinks;
    public int TotalWater;
}

[System.Serializable]
public class CalendarData
{
    public List<DayData> Days = new();
}

public class CalendarService
{
    private Dictionary<string, DayData> _data = new();
    private readonly string _savePath;
    public DayData GetDayData(DateTime date)
    {
        string key = date.ToString("yyyy-MM-dd");
        if (_data.TryGetValue(key, out var value))
        {
            return value;
        }
        return null;
    }

    public CalendarService()
    {
        _savePath = Path.Combine(Application.persistentDataPath, "calendar_data.json");
        LoadFromFile();
    }

    public void SetDayData(DateTime date, int totalDrinks, int totalWater, int maxDrinks)
    {
        string key = date.ToString("yyyy-MM-dd");

        float alcoholRatio = 0f;

        if (maxDrinks > 0)
        {
            alcoholRatio = Mathf.Clamp01((float)totalDrinks / maxDrinks);
        }

        float waterRatio = 0f;
        if (totalDrinks > 0)
        {
            waterRatio = Mathf.Clamp01((float)totalWater / totalDrinks);
        }


        _data[key] = new DayData
        {
            IsOverLimit = totalDrinks > maxDrinks,
            AlcoholRatio = alcoholRatio,
            WaterRatio = waterRatio,
            Date = key,
            MaxDrink = maxDrinks,
            TotalDrinks = totalDrinks,
            TotalWater = totalWater
        };

        SaveToFile();
    }

    private void SaveToFile()
    {
        CalendarData calendarData = new CalendarData();

        foreach (var kvp in _data)
        {
            calendarData.Days.Add(kvp.Value);
        }

        string json = JsonUtility.ToJson(calendarData, true);
        File.WriteAllText(_savePath, json);
    }

    private void LoadFromFile()
    {
        if (!File.Exists(_savePath))
        {
            _data = new Dictionary<string, DayData>();
            return;
        }

        string json = File.ReadAllText(_savePath);
        CalendarData calendarData = JsonUtility.FromJson<CalendarData>(json);

        _data = new Dictionary<string, DayData>();

        foreach (var day in calendarData.Days)
        {
            _data[day.Date] = day;
        }
    }
     ~CalendarService()
    {
        SaveToFile();
    }
}
