using TMPro;
using UnityEngine;

public class StartScreenUIController : UIController
{
    [SerializeField] private TMP_InputField maxDrinksInput;

    public void OnStartClicked()
    {
        if(!int.TryParse(maxDrinksInput.text, out int maxDrinks) || maxDrinks <= 0)
        {
            Debug.LogWarning("Please enter a valid positive number for max drinks.");
            return;
        }
        sessionService.StartSession(maxDrinks);
        screenManager.ShowScreen(ScreenType.Session);
    }

    protected override bool IsMyScreen(ScreenType screenType)
    {
        return screenType == ScreenType.Start;
    }
}
