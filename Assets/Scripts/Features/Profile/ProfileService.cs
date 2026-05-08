using System.ComponentModel.Design.Serialization;

using UnityEngine;

public static class ProfileService
{
    private const string KEY_NICKNAME = "profile_nickname";
    private const string KEY_WEIGHT = "profile_weight";
    private const string KEY_HEIGHT = "profile_height";
    private const string KEY_AGE = "profile_age";
    private const string KEY_GENDER = "profile_gender";
    public static UserProfile CurrentProfile;

    private const string FEMALE_STRING = "Female";
    private const string MALE_STRING = "Male";
    private const string OTHER_STRING = "Other";

    public static void SaveProfile(UserProfile profile)
    {
        CurrentProfile = profile;
        PlayerPrefs.SetString(KEY_NICKNAME, profile.Nickname ?? "");
        PlayerPrefs.SetFloat(KEY_WEIGHT, profile.WeightKg);
        PlayerPrefs.SetFloat(KEY_HEIGHT, profile.HeightCm);
        PlayerPrefs.SetInt(KEY_AGE, profile.Age);
        PlayerPrefs.SetString(KEY_GENDER, profile.Gender.ToString());

        PlayerPrefs.Save();
    }
    public static UserProfile LoadProfile()
    {
        if(CurrentProfile != null)
            return CurrentProfile;
        CurrentProfile = new UserProfile
        {
            Nickname = PlayerPrefs.GetString(KEY_NICKNAME, "Unnown"),
            WeightKg = PlayerPrefs.GetFloat(KEY_WEIGHT, 75),
            HeightCm = PlayerPrefs.GetFloat(KEY_HEIGHT, 170),
            Age = PlayerPrefs.GetInt(KEY_AGE, 18),
            Gender = ParseGender(PlayerPrefs.GetString(KEY_GENDER, MALE_STRING))
        };
        return CurrentProfile;
    }

    private static Gender ParseGender(string value)
    {
        return value == FEMALE_STRING ? Gender.Female : (value == MALE_STRING ? Gender.Male : Gender.Other);
    }
}
