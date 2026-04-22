using TMPro;
using UnityEngine;

public class ResultScreenUIController : UIController
{
    [SerializeField] private TextMeshProUGUI drinkCountText;
    [SerializeField] private TextMeshProUGUI resultText;
    public void OnEndClicked()
    {
        screenManager.ShowScreen(ScreenType.Start);
    }
    protected override bool IsMyScreen(ScreenType screenType)
    {
        return screenType == ScreenType.Result;
    }

    protected override void OnScreenShown()
    {
        var drinks = sessionService.GetTotalDrinks();
        var maxDrinks = sessionService.GetMaxDrinks();
        drinkCountText.text = $"{drinks}/{maxDrinks}";

        if (drinks <= maxDrinks)
            resultText.text = "Safe night";
        else
            resultText.text = "Over limit";
    }
}
