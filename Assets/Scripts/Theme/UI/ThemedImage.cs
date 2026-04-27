using UnityEngine;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

[ExecuteAlways]
public class ThemedImage : ThemedUI
{
    [SerializeField] private Image _image;
    [SerializeField] private ColorRole _colorRole;

    public void AssignDifferentRole(ColorRole colorRole)
    {
        _colorRole = colorRole;
        ApplyTheme();
    }

    protected override void ApplyTheme()
    {
        var theme = GetTheme();
        if (_image == null || theme == null) return;

        _image.color = theme.GetColor(_colorRole);
    }
}
