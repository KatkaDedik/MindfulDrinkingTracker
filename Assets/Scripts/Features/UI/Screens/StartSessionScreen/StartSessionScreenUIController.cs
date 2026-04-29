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

    private StartDrinkingSessionController _controller;

    protected void Awake()
    {
        if(_controller == null)
        {
            _controller = new StartDrinkingSessionController();
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

    // =========================
    // BUTTON HANDLERS
    // =========================

    public void OnStayInControlClicked()
    {
        _controller.SelectGoal(DrinkingGoal.StayInControl);

        SetupSlider(
            "Target Promile",
            0,
            20,
            5);

        RefreshUI();
    }

    public void OnLimitDrinksClicked()
    {
        _controller.SelectGoal(DrinkingGoal.LimitDrinks);
        
        SetupSlider(
            "Max Drinks",
            1,
            10,
            3);

        RefreshUI();
    }

    public void OnDriveTomorrowClicked()
    {
        _controller.SelectGoal(DrinkingGoal.DriveTomorrow);

        SetupSlider(
            "Sober By",
            4,
            12,
            7);

        RefreshUI();
    }

    public void OnJustTrackClicked()
    {
        _controller.SelectGoal(DrinkingGoal.JustTrack);

        _targetSlider.gameObject.SetActive(false);

        RefreshUI();
    }

    // =========================
    // SLIDER
    // =========================

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
        switch (_controller.SelectedGoal)
        {
            case DrinkingGoal.StayInControl:
                _controller.SetPromile(value);
                _targetValueText.text = $"{((float)value/10f):F1}‰";
                break;

            case DrinkingGoal.LimitDrinks:
                int drinks = Mathf.RoundToInt(value);
                _controller.SetMaxDrinks(drinks);
                _targetValueText.text = $"{drinks} drinks";
                break;

            case DrinkingGoal.DriveTomorrow:
                int hour = Mathf.RoundToInt(value);
                _controller.SetSoberBy(hour);
                _targetValueText.text = $"{hour}:00";
                break;
        }
    }

    // =========================
    // START SESSION
    // =========================

    public void OnStartClicked()
    {
        var config = _controller.BuildConfig();
        SessionController.Instance.OnStartSession(config);
        ScreenManager.ShowScreen(ScreenType.ActiveSession);
    }

    // =========================
    // UI STATE
    // =========================

    private void RefreshUI()
    {
        bool hasGoal = _controller.SelectedGoal != DrinkingGoal.None;

        _startButton.gameObject.SetActive(hasGoal);
    }
}
