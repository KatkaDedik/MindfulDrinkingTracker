using UnityEngine;

public class GameBootstrapper : MonoBehaviour
{
    [SerializeField] private ScreenUIControllerBase[] _controllers;
    [SerializeField] private ScreenManager _screenManager;
    [SerializeField] private CalendarUIController _calendarUIController;

    private void Awake()
    {
        var sessionService = new SessionService();
        var calendarService = new CalendarService();
        var profileService = new ProfileService();

        foreach (var controller in _controllers)
        {
            controller.Init(sessionService, _screenManager, profileService);
        }

        _calendarUIController.Init(sessionService, calendarService);
    }
}
