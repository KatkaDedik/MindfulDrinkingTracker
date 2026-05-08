using System;

using UnityEngine;

public class DrinkWidgetUIController : InfoWidgetUIControllerBase
{
    private SessionStatisticsService _statisticsService;

    public override void Init(SessionWidgetContext context)
    {
        _statisticsService = context.SessionStatisticsService;
    }

    public override void UpdateWidget()
    {
        int Drinks = _statisticsService.GetTotalDrinks();
        if (_isMainWidget)
        {
            int MaxDrinks = _statisticsService.GetMaxDrinks();
            _valueLabelText.SetText(Drinks.ToString() + "/" + MaxDrinks.ToString());

        }
        else
        {
            _valueLabelText.SetText(Drinks.ToString());
        }
    }
}
