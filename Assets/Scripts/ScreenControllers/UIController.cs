using UnityEngine;

public abstract class UIController : MonoBehaviour
{
    protected SessionService sessionService;
    protected ScreenManager screenManager;

    public virtual void Init(SessionService sessionService, ScreenManager screenManager)
    {
        this.sessionService = sessionService;
        this.screenManager = screenManager;
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
        if (screenManager != null)
            screenManager.OnScreenChanged -= HandleScreenChanged;
    }
}
