using System.Collections.Generic;

using UnityEngine;

public enum ScreenType
{
    Home,
    ActiveSession,
    Result,
    Profile,
    ConfigurateNewSession
}

public class ScreenManager : MonoBehaviour
{
    [SerializeField] private GameObject _homeScreen;
    [SerializeField] private GameObject _activeSessionScreen;
    [SerializeField] private GameObject _resultScreen;
    [SerializeField] private GameObject _profileScreen;
    [SerializeField] private GameObject _configurateSessionScreen;

    private GameObject _currentScreen;
    public ScreenType CurrentScreenType { get; private set; }
    private Dictionary<ScreenType, GameObject> _screens;

    public System.Action<ScreenType> OnScreenChanged;

    public void Init()
    {
        _screens = new Dictionary<ScreenType, GameObject>
        {
            { ScreenType.Home, _homeScreen },
            { ScreenType.ActiveSession, _activeSessionScreen },
            { ScreenType.Result, _resultScreen },
            { ScreenType.Profile, _profileScreen },
            { ScreenType.ConfigurateNewSession, _configurateSessionScreen }
        };

        foreach (var screen in _screens)
        {
            if (screen.Value == null)
            {
                Debug.LogError($"Screen {screen.Key} is not assigned in inspector!");
            }
            else
            {
                screen.Value.SetActive(false);
            }
        }
    }

    public void ShowScreen(ScreenType screenType)
    {
        if (!_screens.ContainsKey(screenType))
        {
            Debug.LogError($"Screen {screenType} not found!");
            return;
        }

        if (_currentScreen == _screens[screenType])
            return;

        HideCurrentScreen();
        CurrentScreenType = screenType;
        _currentScreen = _screens[screenType];
        _currentScreen.SetActive(true);
        OnScreenChanged?.Invoke(screenType);
    }

    private void HideCurrentScreen()
    {
        if (_currentScreen != null)
        {
            _currentScreen.SetActive(false);
        }
        else
        {
            Debug.LogWarning("No current screen to hide. Hide all");
            foreach (var screen in _screens.Values)
            {
                screen.SetActive(false);
            }
        }
    }
}
