using UnityEngine;

public abstract class ScreenUIControllerBase : MonoBehaviour
{
    private ScreenManager _screenManager;
    protected ScreenManager ScreenManager => _screenManager;

    private SessionService _sessionService;
    protected SessionService SessionService => _sessionService;
    private bool _isInitialized = false;

    public virtual void Init(SessionService sessionService, ScreenManager screenManager)
    {
        if (_isInitialized) return;
        _isInitialized = true;

        this._sessionService = sessionService;
        this._screenManager = screenManager;
        screenManager.OnScreenChanged += HandleScreenChanged;
    }

    private void HandleScreenChanged(ScreenType screenType)
    {
        if (IsMyScreen(screenType))
        {
            OnScreenShown();
        }
        else
        {
            OnScreenHidden();
        }
    }

    protected abstract bool IsMyScreen(ScreenType screenType);

    protected virtual void OnScreenShown() { }
    protected virtual void OnScreenHidden() { }

    protected virtual void OnDestroy()
    {
        if (ScreenManager != null)
            ScreenManager.OnScreenChanged -= HandleScreenChanged;
    }
}
