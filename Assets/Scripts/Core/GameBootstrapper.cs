using UnityEngine;

public class GameBootstrapper : MonoBehaviour
{
    [SerializeField] private ScreenUIControllerBase[] _screenUIcontrollers;
    [SerializeField] private ScreenManager _screenManager;
    [SerializeField] private CalendarUIController _calendarUIController;
    [SerializeField] private MenuManager _menuManager;


    private void Awake()
    {
        var sessionState = new SessionState();

        var context = new AppContext
        {
            SessionService = new SessionService(sessionState),
            SessionPromileService = new SessionPromileService(sessionState),
            SessionStatisticsService = new SessionStatisticsService(sessionState),
            CalendarService = new CalendarService(),
            ScreenManager = _screenManager
        };

        foreach (var controller in _screenUIcontrollers)
        {
            controller.Init(context);
        }

        _calendarUIController.Init(context);
        _menuManager.Init(context);
    }
}
