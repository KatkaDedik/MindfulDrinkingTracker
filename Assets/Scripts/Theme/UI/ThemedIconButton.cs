using UnityEngine;
using UnityEngine.UI;

public class ThemedIconButton : ThemedUI
{
    [SerializeField] private Image _icon;
    [SerializeField] private ColorRole _iconColorRole;

    [SerializeField] private Image _backgroundImage;
    [SerializeField] private Image _glowImage;
    [SerializeField] private ColorRole _backgroundColorRole;

    protected override void ApplyTheme()
    {
        var colorTheme = GetTheme();
        if (_icon == null || _backgroundImage == null || _glowImage == null || colorTheme == null) return;

        //Set icon theme
        _icon.color = colorTheme.GetColor(_iconColorRole);

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
