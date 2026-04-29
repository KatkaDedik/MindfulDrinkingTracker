using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class DrinkButtonUIController : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _promileText;
    [SerializeField] private Button _button;

    private DrinkDefinition _drink;

    private Action<DrinkDefinition> _onClicked;

    public void Setup(DrinkDefinition drink, Action<DrinkDefinition> onClicked)
    {
        _drink = drink;
        _onClicked = onClicked;

        _icon.sprite = drink.Icon;
        _nameText.text = drink.DisplayName;

        _button.onClick.RemoveAllListeners();
        _button.onClick.AddListener(OnClick);
    }

    public void SetPromilePreview(float promile)
    {
        _promileText.text = $"+{promile:F2}‰";
    }

    public void SetTimeout(float timeout)
    {
        _promileText.text = $"Next in: {timeout:F1} mins";
    }

    public void OnClick()
    {
        _onClicked?.Invoke(_drink);
    }
}
