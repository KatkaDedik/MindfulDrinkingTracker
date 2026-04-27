using TMPro;
using UnityEngine;
using UnityEngine.UI;

#if UNITY_EDITOR
using UnityEditor;
#endif


public class ThemedText : ThemedUI
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private ColorRole _colorRole;
    [SerializeField] private TextStyle _textStyle;

    private void Reset()
    {
        _text = GetComponent<TMP_Text>();
    }

    protected override void ApplyTheme()
    {
        var colorTheme = GetTheme();
        if (_text == null || colorTheme == null) return;

        _text.color = colorTheme.GetColor(_colorRole);

        var textTheme = GetTextTheme();
        TextStyleData style = textTheme.GetTextStyle(_textStyle);
        if (style != null)
        {
            _text.font = style.Font;
            _text.fontSize = style.FontSize;
            _text.fontStyle = style.FontStyle;
        }
    }
}
