public class SoberAtUIController : InfoWidgetUIControllerBase
{
    private SessionPromileService _sessionPromileService;
    public override void Init(SessionWidgetContext context)
    {
        _sessionPromileService = context.SessionPromileService;
    }


    public override void UpdateWidget()
    {
        System.DateTime time = _sessionPromileService.CalculateSoberTime();
        UpdateTime(time);
    }

    public void UpdateTime(System.DateTime time)
    {
        _valueLabelText.SetText(time.ToString("hh\\:mm\\:ss"));
    }
}
