using UnityEngine;
using UnityEngine.UI;

public class ThemedSelectableButton : ThemedUI
{
    [SerializeField] private Image _backgroundImage;
    [SerializeField] private Image _glowImage;

    [SerializeField] private TMPro.TextMeshProUGUI _text;

    [SerializeField] private Image _iconImage;
    [SerializeField] private Image _filledIconImage;

    [SerializeField] private ColorRole _foregroundColorRole;
    [SerializeField] private ColorRole _backgroundColorRole;
    [SerializeField] private TextStyle _textStyle;

    private ColorRole _defaultForegroundColor;
    private ColorRole _defaultBackgroundColor;
    private bool _isInitialized = false;


    private void Start()
    {
        Init();
    }

    private void Init()
    {
        _filledIconImage.gameObject.SetActive(false);
        _defaultBackgroundColor = _backgroundColorRole;
        _defaultForegroundColor = _foregroundColorRole;
        _isInitialized = true;
    }

    protected override void ApplyTheme()
    {
        var colorTheme = GetTheme();
        if (colorTheme == null) return;

        //Set text theme
        if (_text != null)
        {
            _text.color = colorTheme.GetColor(_foregroundColorRole);
            var textTheme = GetTextTheme();
            TextStyleData style = textTheme.GetTextStyle(_textStyle);
            if (style != null)
            {
                _text.font = style.Font;
                _text.fontSize = style.FontSize;
                _text.fontStyle = style.FontStyle;
            }
        }

        //Set icon theme
        if (_iconImage != null)
        {
            _iconImage.color = colorTheme.GetColor(_foregroundColorRole);
        }
        if (_filledIconImage != null)
        {
            _filledIconImage.color = colorTheme.GetColor(_foregroundColorRole);
        }

        //Set background theme
        if (_backgroundImage != null)
        {
            _backgroundImage.color = colorTheme.GetColor(_backgroundColorRole);
        }

    }

    public void SetSelected(bool selected)
    {
        _iconImage.gameObject.SetActive(!selected);
        _filledIconImage.gameObject.SetActive(selected);

        if (!_isInitialized)
        {
            Init();
        }

        _backgroundColorRole = selected
            ? _defaultForegroundColor
            : _defaultBackgroundColor;

        _foregroundColorRole = selected
            ? _defaultBackgroundColor
            : _defaultForegroundColor;


        ApplyTheme();
    }


}
