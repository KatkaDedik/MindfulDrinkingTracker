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


    private bool _isFilled = false;

    private void Start()
    {
        _filledIconImage.gameObject.SetActive(false);
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

    public void Flip()
    {
        if (_isFilled)
            SetNormal();
        else
            SetFilled();

        ColorRole tmp = _backgroundColorRole;
        _backgroundColorRole = _foregroundColorRole;
        _foregroundColorRole = tmp;

        ApplyTheme();
    }

    public void SetNormal()
    {
        _iconImage.gameObject.SetActive(true);
        _filledIconImage.gameObject.SetActive(false);
        _isFilled = false;
    }

    public void SetFilled()
    {
        _iconImage.gameObject.SetActive(false);
        _filledIconImage.gameObject.SetActive(true);
        _isFilled = true;
    }
}
