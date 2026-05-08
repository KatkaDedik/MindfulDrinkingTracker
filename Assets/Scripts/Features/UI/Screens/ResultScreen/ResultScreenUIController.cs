using TMPro;
using UnityEngine;

public class ResultScreenUIController : ScreenUIControllerBase
{
    [SerializeField] private TextMeshProUGUI _drinkCountText;
    [SerializeField] private TextMeshProUGUI _resultText;
    public void OnEndClicked()
    {
        ScreenManager.ShowScreen(ScreenType.Home);
    }
    protected override bool IsMyScreen(ScreenType screenType)
    {
        return screenType == ScreenType.Result;
    }
}
