using System.Collections.Generic;
using UnityEngine;

public enum ScreenType
{
    Start,
    Session,
    Result,
    Profile
}

public class ScreenManager : MonoBehaviour
{
    [SerializeField] private GameObject _startScreen;
    [SerializeField] private GameObject _sessionScreen;
    [SerializeField] private GameObject _resultScreen;
    [SerializeField] private GameObject _profileScreen;

    private GameObject _currentScreen;
    private Dictionary<ScreenType, GameObject> _screens;

    public System.Action<ScreenType> OnScreenChanged;

    public void Start()
    {
        _screens = new Dictionary<ScreenType, GameObject>
        {
            { ScreenType.Start, _startScreen },
            { ScreenType.Session, _sessionScreen },
            { ScreenType.Result, _resultScreen },
            { ScreenType.Profile, _profileScreen }
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

        ShowScreen(ScreenType.Start);
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
