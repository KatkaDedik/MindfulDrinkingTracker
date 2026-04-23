using System;
using UnityEngine;
using UnityEngine.UI;

public class DayUIController : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject _drinkingMeter;
    [SerializeField] private Image _alcoholMeter;
    [SerializeField] private Image _waterMeter;
    [SerializeField] private TMPro.TextMeshProUGUI _dateText;
    [SerializeField] private Image _background;

    private int _day;
    private DateTime _date;

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
        _background.color = Color.clear;
    }

    public void SetNoDrink()
    {
        _drinkingMeter.SetActive(false);
        _background.color = Color.gray; 
    }
    public void SetDrinking(float alcoholRatio, float waterRatio)
    {
        _drinkingMeter.SetActive(true);

        _alcoholMeter.rectTransform.rotation = Quaternion.Euler(0, 0, Mathf.Clamp(180 - (alcoholRatio * 180), 0, 180));
        _waterMeter.rectTransform.rotation = Quaternion.Euler(0, 0, Mathf.Clamp(180 - (waterRatio * 180), 0, 180));
    }

    public void SetOverLimit()
    {
        _background.color = Color.red;
    }
}
