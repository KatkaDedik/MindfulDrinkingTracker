using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class ProfileService
{
    private const string KEY_NICKNAME = "profile_nickname";
    private const string KEY_WEIGHT = "profile_weight";
    private const string KEY_HEIGHT = "profile_height";
    private const string KEY_AGE = "profile_age";
    private const string KEY_GENDER = "profile_gender";

    public void SaveProfile(UserProfile profile)
    {
        PlayerPrefs.SetString(KEY_NICKNAME, profile.Nickname ?? "");
        PlayerPrefs.SetFloat(KEY_WEIGHT, profile.WeightKg);
        PlayerPrefs.SetFloat(KEY_HEIGHT, profile.HeightCm);
        PlayerPrefs.SetInt(KEY_AGE, profile.Age);
        PlayerPrefs.SetString(KEY_GENDER, profile.Gender.ToString());

        PlayerPrefs.Save();
    }
    public UserProfile LoadProfile()
    {
        return new UserProfile
        {
            Nickname = PlayerPrefs.GetString(KEY_NICKNAME, "Unnown"),
            WeightKg = PlayerPrefs.GetFloat(KEY_WEIGHT, 75),
            HeightCm = PlayerPrefs.GetFloat(KEY_HEIGHT, 170),
            Age = PlayerPrefs.GetInt(KEY_AGE, 18),
            Gender = ParseGender(PlayerPrefs.GetString(KEY_GENDER, "male"))
        };
    }

    private Gender ParseGender(string value)
    {
        return value == "female" ? Gender.Female : (value == "male" ? Gender.Male : Gender.Other);
    }
}
