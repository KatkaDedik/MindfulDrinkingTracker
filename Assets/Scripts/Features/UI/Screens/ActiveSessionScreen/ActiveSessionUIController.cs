using System;
using System.Collections.Generic;

using UnityEngine;

public class ActiveSessionUIController : ScreenUIControllerBase
{
    [SerializeField] private ModularInfoGrid _infoGrid;
    [SerializeField] private DrinkDefinition[] _drinks;
    [SerializeField] private GameObject _drinkButtonPrefab;
    [SerializeField] private Transform _drinkContainer;

    private List<DrinkButtonUIController> _drinkButtons = new();

    public override void Init(SessionService sessionService, ScreenManager screenManager)
    {
        base.Init(sessionService, screenManager);

        sessionService.OnDrinkAdded += HandleDrinkAdded;
        sessionService.OnWaterAdded += HandleWaterAdded;

    }

    protected override void OnScreenShown()
    {
        InitializeUI();
        UpdateUI();
    }

    private void OnDrinkClicked(DrinkDefinition drink)
    {
        SessionController.Instance.OnAddDrink(drink);
    }

    protected override bool IsMyScreen(ScreenType screenType)
    {
        return screenType == ScreenType.ActiveSession;
    }
    private void HandleDrinkAdded(DrinkDefinition drink)
    {
        UpdateUI();
    }

    private void HandleWaterAdded()
    {
        UpdateUI();
    }

    public void OnAddWaterClicked()
    {
        SessionService.AddWater();
    }

    public void OnEndSessionClicked()
    {
        SessionService.EndSession();
        ScreenManager.ShowScreen(ScreenType.Result);
    }

    private void InitializeUI()
    {
        _infoGrid.Initialize(SessionService.CurrentGoal, SessionService);
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

    private void UpdateUI()
    {
        float currentPromile = SessionService.GetCurrentPromile();
        float maxPromile = SessionService.GetCurrentSession().DesiredMaxPromilePeak;

        for (int i = 0; i < _drinkButtons.Count; i++)
        {
            var button = _drinkButtons[i];
            var drink = _drinks[i];

            float afterDrink = SessionService.CalculatePromileAfterDrink(drink);
            float increase = afterDrink - currentPromile;

            if (maxPromile <= 0f || afterDrink <= maxPromile)
            {
                button.SetPromilePreview(increase);
            }
            else
            {
                DateTime nextTime = SessionService.CalculateTimeForNextDrink(drink);
                float minutes = (float)(nextTime - DateTime.Now).TotalMinutes;

                button.SetTimeout(Mathf.Max(0, minutes));
            }
        }
        _infoGrid.UpdateWidgets();
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        if (SessionService != null)
        {
            SessionService.OnDrinkAdded -= HandleDrinkAdded;
        }
    }
}
