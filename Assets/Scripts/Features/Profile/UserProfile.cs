using UnityEngine;
using UnityEngine.Analytics;

public class UserProfile
{
    public string Nickname;
    public float WeightKg;
    public float HeightCm;
    public int Age;
    public Gender Gender;
}

public enum Gender
{
    Male,
    Female,
    Other
}
