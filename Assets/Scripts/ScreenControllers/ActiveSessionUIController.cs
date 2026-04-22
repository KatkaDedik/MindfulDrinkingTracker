using UnityEngine;

public class ActiveSessionUIController : UIController
{
    [SerializeField] private TMPro.TextMeshProUGUI drinkCountText;

    public override void Init(SessionService sessionService, ScreenManager screenManager)
    {
        base.Init(sessionService, screenManager);

        sessionService.OnDrinkAdded += HandleDrinkAdded;
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
        sessionService.AddDrink();
    }

    public void OnEndSessionClicked()
    {
        sessionService.EndSession();
        screenManager.ShowScreen(ScreenType.Result);
    }


    protected override void OnScreenShown()
    {
        UpdateUI();
    }

    private void UpdateUI()
    {
        drinkCountText.text = $"{sessionService.GetTotalDrinks()}/{sessionService.GetMaxDrinks()}";
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        if (sessionService != null)
        {
            sessionService.OnDrinkAdded -= HandleDrinkAdded;
        }
    }
}
