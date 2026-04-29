using UnityEngine;

public class DrinkWidgetUIController : InfoWidgetUIControllerBase
{
    public override void UpdateWidget()
    {
        int Drinks = _sessionService.GetTotalDrinks();
        if (_isMainWidget)
        {
            int MaxDrinks = _sessionService.GetMaxDrinks();
            _valueLabelText.SetText(Drinks.ToString() + "/" + MaxDrinks.ToString());

        }
        else
        {
            _valueLabelText.SetText(Drinks.ToString());
        }
    }
}
