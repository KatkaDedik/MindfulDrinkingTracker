using System.Collections.Generic;

using UnityEngine;

[System.Serializable]
public struct MenuButton
{
    public GameObject ButtonParent;
    public ScreenType VisibleOnScreen;
    public AppFlowState VisibleInState;
}
public class MenuManager : MonoBehaviour
{
    [SerializeField] private List<MenuButton> _menuButtons;

    private ScreenManager _screenManager;
    private AppFlowController _appFlowController;

    public void Init(AppContext context)
    {
        _screenManager = context.ScreenManager;
        _appFlowController = context.AppFlowController;
        Refresh(_screenManager.CurrentScreenType, _appFlowController.CurrentState);
    }

    private void OnEnable()
    {
        _appFlowController.OnFlowStateChanged += HandleRefresh;
        _screenManager.OnScreenChanged += HandleRefresh;
    }

    private void OnDisable()
    {
        _appFlowController.OnFlowStateChanged -= HandleRefresh;
        _screenManager.OnScreenChanged -= HandleRefresh;
    }

    public void HandleRefresh(AppFlowState appFlowState)
    {
        var screenType = _screenManager.CurrentScreenType;
        Refresh(screenType, appFlowState);
    }

    public void HandleRefresh(ScreenType screenType)
    {
        var appFlowState = _appFlowController.CurrentState;
        Refresh(screenType, appFlowState);
    }

    public void Refresh(ScreenType screenType, AppFlowState appFlowState)
    {
        foreach (var menuButton in _menuButtons)
        {
            if (menuButton.ButtonParent == null)
            {
                Debug.LogError(
                    $"Menu button for state {menuButton.VisibleInState} is not assigned!");
                continue;
            }
            menuButton.ButtonParent.SetActive(menuButton.VisibleInState == appFlowState && screenType == menuButton.VisibleOnScreen);
        }
    }

    public void OnContinueSessionClicked()
    {
        _appFlowController.ContinueSession();
    }

    public void OnProfileClicked()
    {
        _appFlowController.OpenProfile();
    }

    public void OnHomeClicked()
    {
        _appFlowController.OpenHome();
    }

    public void OnCreateNewSessionClicked()
    {
        _appFlowController.OpenConfigurateSession();
    }

    public void OnEndSessionClicked()
    {
        _appFlowController.EndSession();
    }

    public void OnStartNewSessionClicked()
    {
        _appFlowController.StartConfiguratedSession();
    }
}
