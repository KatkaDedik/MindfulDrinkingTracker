using UnityEngine;

public abstract class InfoWidgetUIControllerBase : MonoBehaviour
{
    [SerializeField] protected ThemedText _labelThemedText;
    [SerializeField] protected ThemedText _valueLabelText;
    [SerializeField] protected GameObject _mainWidget;
    [SerializeField] protected ThemedImage _themedBackground;

    protected bool _isMainWidget = false;
    protected SessionService _sessionService;

    public virtual void Initialize(bool isMainWidget, SessionService sessionService)
    {
        _sessionService = sessionService;
        _isMainWidget = isMainWidget;
        if (isMainWidget)
        {
            _labelThemedText.SetTextStyle(TextStyle.Subtitle);
            _valueLabelText.SetTextStyle(TextStyle.Title);
            _mainWidget.SetActive(true);
        }
        else
        {
            _labelThemedText.SetTextStyle(TextStyle.Caption);
            _valueLabelText.SetTextStyle(TextStyle.Button);
            _mainWidget.SetActive(false);
        }
    }

    public abstract void UpdateWidget();



}
