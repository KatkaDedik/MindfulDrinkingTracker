using UnityEngine;
using UnityEngine.UI;

public class ActiveSessionUIController : ScreenUIControllerBase
{
    [SerializeField] private TMPro.TextMeshProUGUI _drinkCountText;
    [SerializeField] private TMPro.TextMeshProUGUI _waterRatioText;
    [SerializeField] private Image _alcoholMeter;
    [SerializeField] private Image _waterMeter;

    public override void Init(SessionService sessionService, ScreenManager screenManager)
    {
        base.Init(sessionService, screenManager);

        sessionService.OnDrinkAdded += HandleDrinkAdded;
        sessionService.OnWaterAdded += HandleDrinkAdded; // Reuse the same handler to update UI for both drinks and water
    }
    protected override bool IsMyScreen(ScreenType screenType)
    {
        return screenType == ScreenType.Session;
    }
    private void HandleDrinkAdded()
    {
        UpdateUI();
    }

    public void OnAddDrinkClicked()
    {
        SessionService.AddDrink();
    }

    public void OnAddWaterClicked()
    {
        SessionService.AddWater();
    }

    public void OnEndSessionClicked()
    {
        SessionService.EndSession();
        ScreenManager.ShowScreen(ScreenType.Result);
    }


    protected override void OnScreenShown()
    {
        UpdateUI();
    }

    private void UpdateUI()
    {
        float alcoholRatio = SessionService.GetMaxDrinks() > 0 ? (float)SessionService.GetTotalDrinks() / SessionService.GetMaxDrinks() : 0;
        float waterRatio = SessionService.GetTotalDrinks() > 0 ? (float)SessionService.GetTotalWater() / SessionService.GetTotalDrinks() : 0;

        _drinkCountText.text = $"{SessionService.GetTotalDrinks()}/{SessionService.GetMaxDrinks()}";
        _waterRatioText.text = $"{SessionService.GetTotalWater()}/{SessionService.GetTotalDrinks()}";

        _alcoholMeter.rectTransform.rotation = Quaternion.Euler(0, 0, Mathf.Clamp(180 - (alcoholRatio * 180), 0, 180));
        _waterMeter.rectTransform.rotation = Quaternion.Euler(0, 0, Mathf.Clamp(180 - (waterRatio * 180), 0, 180));
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        if (SessionService != null)
        {
            SessionService.OnDrinkAdded -= HandleDrinkAdded;
        }
    }
}
