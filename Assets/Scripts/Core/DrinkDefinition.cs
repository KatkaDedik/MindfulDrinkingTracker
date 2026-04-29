using UnityEngine;

[CreateAssetMenu(fileName = "Drink", menuName = "Drinking/Drink Definition")]
public class DrinkDefinition : ScriptableObject
{
    public string DisplayName;
    public Sprite Icon;

    [Header("Alcohol")]
    public float VolumeMl;
    public float AlcoholPercent;

    public float GetPureAlcoholGrams()
    {
        return VolumeMl * (AlcoholPercent / 100f) * 0.789f;
    }
}
