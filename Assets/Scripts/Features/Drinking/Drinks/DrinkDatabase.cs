using UnityEngine;

[CreateAssetMenu]
public class DrinkDatabase : ScriptableObject
{
    [SerializeField]
    private DrinkDefinition[] _drinks;

    public DrinkDefinition GetDrinkById(int id)
    {
        foreach (var drink in _drinks)
        {
            if (drink.ID == id)
                return drink;
        }

        return null;
    }
}
