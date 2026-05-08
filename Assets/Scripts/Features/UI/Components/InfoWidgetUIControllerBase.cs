using UnityEngine;

public abstract class InfoWidgetUIControllerBase : MonoBehaviour
{
    [SerializeField] protected ThemedText _labelThemedText;
    [SerializeField] protected ThemedText _valueLabelText;
    [SerializeField] protected GameObject _mainWidget;
    [SerializeField] protected ThemedImage _themedBackground;

    protected bool _isMainWidget = false;

    public abstract void Init(SessionWidgetContext context);

    public virtual void SetMainWidget()
    {
        _isMainWidget = true;
        _labelThemedText.SetTextStyle(TextStyle.Subtitle);
        _valueLabelText.SetTextStyle(TextStyle.Title);
        _mainWidget.SetActive(true);
    }

    public virtual void SetSubWidget()
    {
        _isMainWidget = false;
        _labelThemedText.SetTextStyle(TextStyle.Caption);
        _valueLabelText.SetTextStyle(TextStyle.Button);
        _mainWidget.SetActive(false);
    }

    public abstract void UpdateWidget();
}
