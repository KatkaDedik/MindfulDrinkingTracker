using TMPro;
using UnityEngine;

public class HomeScreenUIController : ScreenUIControllerBase
{
    public void OnStartClicked()
    {
        SessionService.StartSession(5);
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
