using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class ThemedButton : ThemedUI
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private ColorRole _textColorRole;

    [SerializeField] private Image _backgroundImage;
    [SerializeField] private Image _glowImage;
    [SerializeField] private ColorRole _backgroundColorRole;

    private void Reset()
    {
        _text = GetComponent<TMP_Text>();
    }

    protected override void ApplyTheme()
    {
        var colorTheme = GetTheme();
        if (_text == null || colorTheme == null) return;

        //Set text theme
        _text.color = colorTheme.GetColor(_textColorRole);
        var textTheme = GetTextTheme();
        TextStyleData style = textTheme.GetTextStyle(TextStyle.Button);
        if (style != null)
        {
            _text.font = style.Font;
            _text.fontSize = style.FontSize;
            _text.fontStyle = style.FontStyle;
        }

        //Set background theme
        if (_backgroundImage != null)
        {
            _backgroundImage.color = colorTheme.GetColor(_backgroundColorRole);
        }
        if (_glowImage != null)
        {
            _glowImage.color = colorTheme.GetColor(ColorRole.Glow);
        }
    }

}
