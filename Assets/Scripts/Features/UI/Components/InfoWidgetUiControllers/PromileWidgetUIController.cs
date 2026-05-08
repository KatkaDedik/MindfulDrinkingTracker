public class PromileWidgetUIController : InfoWidgetUIControllerBase
{
    private SessionPromileService _sessionPromileService;

    public override void Init(SessionWidgetContext context)
    {
        _sessionPromileService = context.PromileService;
        UpdatePromile(0f);
    }

    public override void UpdateWidget()
    {
        float promile = _sessionPromileService.GetCurrentPromile();
        UpdatePromile(promile);
    }

    public void UpdatePromile(float promile)
    {

        _valueLabelText.SetText(promile.ToString("0.00") + "‰");
    }
}
