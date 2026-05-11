using UnityEngine;

public class GameBootstrapper : MonoBehaviour
{
    [SerializeField] private ScreenUIControllerBase[] _screenUIcontrollers;
    [SerializeField] private ScreenManager _screenManager;
    [SerializeField] private CalendarUIController _calendarUIController;
    [SerializeField] private MenuManager _menuManager;
    [SerializeField] private DrinkDatabase _drinkDatabase;

    private void Awake()
    {
        var sessionState = new SessionState();

        var context = new AppContext
        {
            SessionService = new SessionService(sessionState, _drinkDatabase),
            SessionPromileService = new SessionPromileService(sessionState),
            SessionStatisticsService = new SessionStatisticsService(sessionState),
            CalendarService = new CalendarService(),
            ScreenManager = _screenManager,
        };
        context.AppFlowController = new AppFlowController(
                context.ScreenManager,
                context.SessionService);

        foreach (var controller in _screenUIcontrollers)
        {
            controller.Init(context);
        }

        _screenManager.Init();
        context.AppFlowController.Initialize();

        _calendarUIController.Init(context);
        _menuManager.Init(context);
    }
}
