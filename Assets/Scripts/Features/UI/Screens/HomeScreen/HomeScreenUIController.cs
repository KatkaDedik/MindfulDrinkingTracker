public class HomeScreenUIController : ScreenUIControllerBase
{
    public void OnStartClicked()
    {
        ScreenManager.ShowScreen(ScreenType.ConfigurateNewSession);
    }

    public void OnProfileClicked()
    {
        ScreenManager.ShowScreen(ScreenType.Profile);
    }

    protected override bool IsMyScreen(ScreenType screenType)
    {
        return screenType == ScreenType.ConfigurateNewSession;
    }
}
