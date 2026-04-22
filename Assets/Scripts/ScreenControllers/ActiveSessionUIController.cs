using UnityEngine;

public class ActiveSessionUIController : UIController
{
    [SerializeField] private TMPro.TextMeshProUGUI drinkCountText;

    public void OnAddDrinkClicked()
    {
        sessionService.AddDrink();
        UpdateUI();
    }

    public void OnEndSessionClicked()
    {
        sessionService.EndSession();
        screenManager.ShowScreen(ScreenType.Result);
    }

    protected override bool IsMyScreen(ScreenType screenType)
    {
        return screenType == ScreenType.Session;
    }

    protected override void OnScreenShown()
    {
        UpdateUI();
    }

    private void UpdateUI()
    {
        drinkCountText.text = $"{sessionService.GetTotalDrinks()}/{sessionService.GetMaxDrinks()}";
    }
}
