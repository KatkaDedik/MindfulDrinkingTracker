using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StartSessionScreenUIController : ScreenUIControllerBase
{
    [Header("UI")]
    [SerializeField] private Button _startButton;
    [SerializeField] private Slider _targetSlider;
    [SerializeField] private TextMeshProUGUI _targetValueText;
    [SerializeField] private TextMeshProUGUI _targetText;

    private SessionConfigBuilder _sessionConfigBuilder;

    protected void Awake()
    {
        if(_sessionConfigBuilder == null)
        {
            _sessionConfigBuilder = new SessionConfigBuilder();
        }

        _targetSlider.gameObject.SetActive(false);
        _startButton.gameObject.SetActive(false);

        _targetSlider.onValueChanged.AddListener(OnSliderChanged);
    }

    protected override bool IsMyScreen(ScreenType screenType)
    {
        return screenType == ScreenType.StartSession;
    }

    public void OnHomeClicked()
    {
        ScreenManager.ShowScreen(ScreenType.Home);
    }

    public void OnStayInControlClicked()
    {
        _sessionConfigBuilder.SelectGoal(DrinkingGoal.StayInControl);

        SetupSlider(
            "Target Promile",
            0,
            20,
            5);

        RefreshUI();
    }

    public void OnLimitDrinksClicked()
    {
        _sessionConfigBuilder.SelectGoal(DrinkingGoal.LimitDrinks);
        
        SetupSlider(
            "Max Drinks",
            1,
            10,
            3);

        RefreshUI();
    }

    public void OnDriveTomorrowClicked()
    {
        _sessionConfigBuilder.SelectGoal(DrinkingGoal.DriveTomorrow);

        SetupSlider(
            "Sober By",
            4,
            12,
            7);

        RefreshUI();
    }

    public void OnJustTrackClicked()
    {
        _sessionConfigBuilder.SelectGoal(DrinkingGoal.JustTrack);

        _targetSlider.gameObject.SetActive(false);

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
        switch (_sessionConfigBuilder.SelectedGoal)
        {
            case DrinkingGoal.StayInControl:
                _sessionConfigBuilder.SetPromile(value);
                _targetValueText.text = $"{((float)value/10f):F1}‰";
                break;

            case DrinkingGoal.LimitDrinks:
                int drinks = Mathf.RoundToInt(value);
                _sessionConfigBuilder.SetMaxDrinks(drinks);
                _targetValueText.text = $"{drinks} drinks";
                break;

            case DrinkingGoal.DriveTomorrow:
                int hour = Mathf.RoundToInt(value);
                _sessionConfigBuilder.SetSoberBy(hour);
                _targetValueText.text = $"{hour}:00";
                break;
        }
    }

    public void OnStartClicked()
    {
        SessionConfig config = _sessionConfigBuilder.BuildConfig();
        SessionService.StartSession(config);
        ScreenManager.ShowScreen(ScreenType.ActiveSession);
    }

    private void RefreshUI()
    {
        bool hasGoal = _sessionConfigBuilder.SelectedGoal != DrinkingGoal.None;

        _startButton.gameObject.SetActive(hasGoal);
    }
}
