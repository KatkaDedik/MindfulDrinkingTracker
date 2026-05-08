using System.Collections.Generic;

using UnityEngine;

public class DrinkButtonPanel : MonoBehaviour
{
    [SerializeField] private DrinkDefinition[] _drinks;
    [SerializeField] private GameObject _drinkButtonPrefab;
    [SerializeField] private Transform _drinkContainer;
    private List<DrinkButtonUIController> _drinkButtons = new();
    private SessionService _sessionService;
    private SessionPromileService _sessionPromileService;

    public void Init(SessionService sessionService, SessionPromileService sessionPromileService)
    {
        _sessionService = sessionService;
        _sessionPromileService = sessionPromileService;

        foreach (var button in _drinkButtons)
        {
            Destroy(button.gameObject);
        }
        _drinkButtons.Clear();
        foreach (var drink in _drinks)
        {
            var buttonObj = Instantiate(_drinkButtonPrefab, _drinkContainer);
            var buttonController = buttonObj.GetComponent<DrinkButtonUIController>();
            buttonController.Setup(drink, OnDrinkClicked);
            _drinkButtons.Add(buttonController);
        }
    }

    public void UpdateDrinkButtons()
    {
        for (int i = 0; i < _drinkButtons.Count; i++)
        {
            var button = _drinkButtons[i];
            var drink = _drinks[i];

            float minutes = _sessionPromileService.GetMinutesUntilDrinkAllowed(drink);

            if (minutes <= 0)
            {
                float increase = _sessionPromileService.GetPromileForDrinkPreview(drink);
                button.SetPromilePreview(increase);
            }
            else
            {
                button.SetTimeout(minutes);
            }

        }
    }
    private void OnDrinkClicked(DrinkDefinition drink)
    {
        _sessionService.AddDrink(drink);
    }

}
