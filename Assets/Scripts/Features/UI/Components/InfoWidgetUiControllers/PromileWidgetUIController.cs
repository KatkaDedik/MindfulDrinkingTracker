using UnityEngine;

public class PromileWidgetUIController : InfoWidgetUIControllerBase
{

    public override void Initialize(bool isMainWidget, SessionService sessionService)
    {
        base.Initialize(isMainWidget, sessionService);
        UpdatePromile(0f);
    }

    public override void UpdateWidget()
    {
        float promile = _sessionService.GetCurrentPromile();
        UpdatePromile(promile);
    }

    public void UpdatePromile(float promile)
    {
        
        _valueLabelText.SetText(promile.ToString("0.00") + "‰");
    }
}
