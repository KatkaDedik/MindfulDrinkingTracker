using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public struct GoalButton
{
    public ThemedSelectableButton Theme;
    public DrinkingGoal Goal;
}

public class SessionConfigurationScreenUIController : ScreenUIControllerBase
{
    [Header("UI")]
    [SerializeField] private Button _startButton;
    [SerializeField] private Slider _targetSlider;
    [SerializeField] private TextMeshProUGUI _targetValueText;
    [SerializeField] private TextMeshProUGUI _targetText;
    [SerializeField] private GoalButton[] _goalButtons;

    private AppFlowController _appFlowController;

    public override void Init(AppContext context)
    {
        base.Init(context);
        _appFlowController = context.AppFlowController;
    }
    
    protected void Awake()
    {

        _targetSlider.gameObject.SetActive(false);
        _startButton.gameObject.SetActive(false);

        _targetSlider.onValueChanged.AddListener(OnSliderChanged);
    }

    protected override bool IsMyScreen(ScreenType screenType)
    {
        return screenType == ScreenType.ConfigurateNewSession;
    }

    protected override void OnScreenShown()
    {
        base.OnScreenShown();
        RefreshUI();
    }

    public void OnStayInControlClicked()
    {
        _appFlowController.SessionConfigBuilder.SelectGoal(DrinkingGoal.StayInControl);

        SetupSlider(
            "Target Promile",
            0,
            20,
            5);
        _appFlowController.SetStateToConfigReady(true);
        RefreshUI();
    }

    public void OnLimitDrinksClicked()
    {
        _appFlowController.SessionConfigBuilder.SelectGoal(DrinkingGoal.LimitDrinks);
        
        SetupSlider(
            "Max Drinks",
            1,
            10,
            3);
        _appFlowController.SetStateToConfigReady(true);
        RefreshUI();
    }

    public void OnDriveTomorrowClicked()
    {
        _appFlowController.SessionConfigBuilder.SelectGoal(DrinkingGoal.DriveTomorrow);

        SetupSlider(
            "Sober By",
            4,
            12,
            7);
        _appFlowController.SetStateToConfigReady(true);
        RefreshUI();
    }

    public void OnJustTrackClicked()
    {
        _appFlowController.SessionConfigBuilder.SelectGoal(DrinkingGoal.JustTrack);

        _targetSlider.gameObject.SetActive(false);
        _appFlowController.SetStateToConfigReady(true);
        RefreshUI();
    }

    private void SetupSlider(string label, float min, float max, float defaultValue)
    {
        _targetSlider.gameObject.SetActive(true);

        _targetText.text = label;

        _targetSlider.minValue = min;
        _targetSlider.maxValue = max;
        _targetSlider.value = defaultValue;

        // inicializácia hodnoty
        OnSliderChanged(defaultValue);
    }

    private void OnSliderChanged(float value)
    {
        switch (_appFlowController.SessionConfigBuilder.SelectedGoal)
        {
            case DrinkingGoal.StayInControl:
                _appFlowController.SessionConfigBuilder.SetPromile(value);
                _targetValueText.text = $"{((float)value/10f):F1}‰";
                break;

            case DrinkingGoal.LimitDrinks:
                int drinks = Mathf.RoundToInt(value);
                _appFlowController.SessionConfigBuilder.SetMaxDrinks(drinks);
                _targetValueText.text = $"{drinks} drinks";
                break;

            case DrinkingGoal.DriveTomorrow:
                int hour = Mathf.RoundToInt(value);
                _appFlowController.SessionConfigBuilder.SetSoberBy(hour);
                _targetValueText.text = $"{hour}:00";
                break;
        }
    }

    private void RefreshUI()
    {
        bool hasGoal = _appFlowController.SessionConfigBuilder.SelectedGoal != DrinkingGoal.None;
        foreach (var button in _goalButtons)
        {
            button.Theme.SetSelected(button.Goal == _appFlowController.SessionConfigBuilder.SelectedGoal);
        }
    }
}
