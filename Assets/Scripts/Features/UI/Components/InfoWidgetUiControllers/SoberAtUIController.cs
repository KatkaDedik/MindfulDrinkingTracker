using UnityEngine;

public class SoberAtUIController : InfoWidgetUIControllerBase
{
    public override void UpdateWidget()
    {
        System.DateTime time = _sessionService.CalculateSoberTime();
        UpdateTime(time);
    }

    public void UpdateTime(System.DateTime time)
    {
        _valueLabelText.SetText(time.ToString("hh\\:mm\\:ss"));
    }
}
