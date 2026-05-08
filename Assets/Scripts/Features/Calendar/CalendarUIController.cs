using System;
using System.Collections.Generic;
using UnityEngine;

public class CalendarUIController : MonoBehaviour
{
    [SerializeField] private List<DayUIController> _daySlots; //42
    [SerializeField] private TMPro.TextMeshProUGUI _monthYearText;

    private int _currentYear;
    private int _currentMonth;

    private CalendarService _calendarService;

    public void Init(AppContext context)
    {
        this._calendarService = context.CalendarService;
        DateTime now = DateTime.Now;
        _currentYear = now.Year;
        _currentMonth = now.Month;
        GenerateCalendar(_currentYear, _currentMonth);
    }

    public void GenerateCalendar(int year, int month)
    {

        _monthYearText.text = new DateTime(year, month, 1).ToString("MMMM yyyy");
        int daysInMonth = DateTime.DaysInMonth(year, month);

        DateTime firstDay = new DateTime(year, month, 1);
        int startOffset = ((int)firstDay.DayOfWeek + 6) % 7;

        for (int i = 0; i < _daySlots.Count; i++)
        {
            if (i < startOffset || i >= startOffset + daysInMonth)
            {
                _daySlots[i].SetEmpty();
                continue;
            }

            int dayNumber = i - startOffset + 1;
            DateTime date = new DateTime(year, month, dayNumber);

            _daySlots[i].SetUp(date);

            var data = _calendarService.GetDayData(date);

            if (data == null)
            {
                _daySlots[i].SetNoDrink();
            }
            else if (data.IsOverLimit)
            {
                _daySlots[i].SetOverLimit();
                _daySlots[i].SetDrinking(data.AlcoholRatio, data.WaterRatio);
            }
            else
            {
                _daySlots[i].SetDrinking(data.AlcoholRatio, data.WaterRatio);
            }
        }
    }
}
