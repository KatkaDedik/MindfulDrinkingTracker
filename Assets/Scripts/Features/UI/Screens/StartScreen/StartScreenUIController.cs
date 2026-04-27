using TMPro;
using UnityEngine;

public class StartScreenUIController : ScreenUIControllerBase
{
    [SerializeField] private TMP_InputField _maxDrinksInput;

    public void OnStartClicked()
    {
        if(!int.TryParse(_maxDrinksInput.text, out int maxDrinks) || maxDrinks <= 0)
        {
            Debug.LogWarning("Please enter a valid positive number for max drinks.");
            return;
        }
        SessionService.StartSession(maxDrinks);
        ScreenManager.ShowScreen(ScreenType.Session);
    }

    public void OnProfileClicked()
    {
        ScreenManager.ShowScreen(ScreenType.Profile);
    }

    protected override bool IsMyScreen(ScreenType screenType)
    {
        return screenType == ScreenType.Start;
    }
}
