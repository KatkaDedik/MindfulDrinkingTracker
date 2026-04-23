using TMPro;
using UnityEngine;

public class ResultScreenUIController : ScreenUIControllerBase
{
    [SerializeField] private TextMeshProUGUI _drinkCountText;
    [SerializeField] private TextMeshProUGUI _resultText;
    public void OnEndClicked()
    {
        ScreenManager.ShowScreen(ScreenType.Start);
    }
    protected override bool IsMyScreen(ScreenType screenType)
    {
        return screenType == ScreenType.Result;
    }

    protected override void OnScreenShown()
    {
        var drinks = SessionService.GetTotalDrinks();
        var maxDrinks = SessionService.GetMaxDrinks();
        _drinkCountText.text = $"{drinks}/{maxDrinks}";

        if (drinks <= maxDrinks)
            _resultText.text = "Safe night";
        else
            _resultText.text = "Over limit";
    }
}
