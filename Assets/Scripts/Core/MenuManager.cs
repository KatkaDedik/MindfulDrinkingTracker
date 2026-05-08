using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct MenuButton
{
    public GameObject ButtonParent;
    public ScreenType VisibleOnScreen;
}


public class MenuManager : MonoBehaviour
{
    [SerializeField] private List<MenuButton> _menuButtons;
    private ScreenManager _screenManager;
    private SessionService _sessionService;

    public void Init(AppContext context)
    {
        _screenManager = context.ScreenManager;
        _sessionService = context.SessionService;
    }

    private void OnEnable()
    {
        _screenManager.OnScreenChanged += UpdateVisibleButton;
    }

    private void OnDisable()
    {
        _screenManager.OnScreenChanged -= UpdateVisibleButton;
    }

    public void UpdateVisibleButton(ScreenType screenType)
    {
        foreach (var menuButton in _menuButtons)
        {
            if (menuButton.ButtonParent == null)
            {
                Debug.LogError($"Menu button for screen {menuButton.VisibleOnScreen} is not assigned in inspector!");
                continue;
            }
            menuButton.ButtonParent.SetActive(menuButton.VisibleOnScreen == screenType);
        }
    }

    public void OnStartNewSessionCliced()
    {
        _screenManager.ShowScreen(ScreenType.ActiveSession);
    }

    public void OnContinueSessionClicked()
    {
        _screenManager.ShowScreen(ScreenType.ActiveSession);
        //_sessionService.LoadSession();
    }

    public void OnProfileClicked()
    {
        _screenManager.ShowScreen(ScreenType.Profile);
    }

    public void OnHomeClicked()
    {
        _screenManager.ShowScreen(ScreenType.Home);
    }

    public void OnCreateNewSessionClicked()
    {
        _screenManager.ShowScreen(ScreenType.StartSession);
    }

    public void OnEndSessionClicked()
    {
        _screenManager.ShowScreen(ScreenType.Result);
    }
}
