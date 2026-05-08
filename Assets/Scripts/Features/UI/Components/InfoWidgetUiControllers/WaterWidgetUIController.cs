using UnityEngine;

public class WaterWidgetUIController : InfoWidgetUIControllerBase
{
    private SessionStatisticsService _statisticsService;
    public override void Init(SessionWidgetContext context)
    {
        _statisticsService = context.StatisticsService;
    }

    public override void UpdateWidget()
    {
        int waterIntake = _statisticsService.GetTotalWater();
        _valueLabelText.SetText(waterIntake.ToString());
    }
}
