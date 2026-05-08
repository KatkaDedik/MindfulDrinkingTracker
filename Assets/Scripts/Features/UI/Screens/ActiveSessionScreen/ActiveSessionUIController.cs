using System.Collections;
using UnityEngine;

public class ActiveSessionUIController : ScreenUIControllerBase
{
    [SerializeField] private InfoWidgetGrid _infoGrid;
    [SerializeField] private InfoWidget[] _infoWidgets;
    [SerializeField] private DrinkButtonPanel _drinkButtonPanel;

    private Coroutine _updateCoroutine;

    private SessionWidgetContext _sessionWidgetContext;

    public override void Init(AppContext context)
    {
        base.Init(context);

        _sessionWidgetContext = new SessionWidgetContext
        {
            SessionPromileService = context.SessionPromileService,
            SessionStatisticsService = context.SessionStatisticsService
        };

        foreach (var widget in _infoWidgets)
        {
            widget.WidgetController.Init(_sessionWidgetContext);
        }
    }

    protected override void OnScreenShown()
    {
        _infoGrid.Initialize(SessionService.CurrentGoal, _infoWidgets);
        _drinkButtonPanel.Init(SessionService, _sessionWidgetContext.SessionPromileService);
        UpdateUI();
        if (_updateCoroutine == null)
            _updateCoroutine = StartCoroutine(UpdateLoop());
    }

    protected override void OnScreenHidden()
    {
        if (_updateCoroutine != null)
        {
            StopCoroutine(_updateCoroutine);
            _updateCoroutine = null;
        }
    }

    protected override bool IsMyScreen(ScreenType screenType)
    {
        return screenType == ScreenType.ActiveSession;
    }

    public void OnEnable()
    {
        SessionService.OnSessionUpdated += HandleSessionUpdated;
    }

    public void OnDisable()
    {
        SessionService.OnSessionUpdated -= HandleSessionUpdated;
    }

    private IEnumerator UpdateLoop()
    {
        while (enabled)
        {
            yield return new WaitForSeconds(60);
            UpdateUI();
        }
    }

    private void HandleSessionUpdated()
    {
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (SessionService.GetCurrentSession() == null)
            return;

        _drinkButtonPanel.UpdateDrinkButtons();
        UpdateInfoWidgets();
    }

    private void UpdateInfoWidgets()
    {
        foreach (var widget in _infoWidgets)
        {
            widget.WidgetController.UpdateWidget();
        }
    }
}
