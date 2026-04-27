using System;

using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AnimatedButton))]
public class DayUIController : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject _drinkingMeter;
    [SerializeField] private Image _alcoholMeter;
    [SerializeField] private Image _waterMeter;
    [SerializeField] private TMPro.TextMeshProUGUI _dateText;
    [SerializeField] private Image _background;

    [SerializeField] private Button _button;
    [SerializeField] private ThemedImage _themedBackground;
    [SerializeField] private ThemedImage _themedForeground;

    private AnimatedButton _animatedButton;
    private bool _isInteractable = false;

    private int _day;
    private DateTime _date;

    private void Awake()
    {
        _animatedButton = GetComponent<AnimatedButton>();
        _animatedButton.enabled = _isInteractable;
        _button.interactable = _isInteractable;
    }

    public void SetUp(DateTime date)
    {
        this._date = date;
        this._day = date.Day;
        _dateText.text = _day.ToString();
    }

    public void SetEmpty()
    {
        _dateText.text = "";
        _drinkingMeter.SetActive(false);
        _themedBackground.AssignDifferentRole(ColorRole.Background);
        _themedForeground.AssignDifferentRole(ColorRole.Background);
        _isInteractable = false;
    }

    public void SetNoDrink()
    {
        _drinkingMeter.SetActive(false);
        _background.color = Color.gray;
        _isInteractable = false;
    }
    public void SetDrinking(float alcoholRatio, float waterRatio)
    {
        _drinkingMeter.SetActive(true);

        _alcoholMeter.rectTransform.rotation = Quaternion.Euler(0, 0, Mathf.Clamp(180 - (alcoholRatio * 180), 0, 180));
        _waterMeter.rectTransform.rotation = Quaternion.Euler(0, 0, Mathf.Clamp(180 - (waterRatio * 180), 0, 180));
        _isInteractable = true;
    }

    public void SetOverLimit()
    {
        _background.color = Color.red;
    }

    public void OnDayClicked()
    {
        Debug.Log($"Day clicked: {_date}");
    }

}
