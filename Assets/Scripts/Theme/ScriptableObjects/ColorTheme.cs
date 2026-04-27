using TMPro;

using UnityEngine;

[CreateAssetMenu(fileName = "ColorTheme", menuName = "UI/ColorTheme")]
public class ColorTheme : ScriptableObject
{
    [Header("Background")]
    public Color Background;
    public Color Surface;

    [Header("Text")]
    public Color TextPrimary;
    public Color TextSecondary;

    [Header("Brand")]
    public Color Primary;
    public Color Secondary;

    [Header("State")]
    public Color Success;
    public Color Warning;
    public Color Error;

    public Color Glow;

    public Color GetColor(ColorRole role)
    {
        return role switch
        {
            ColorRole.Background => Background,
            ColorRole.Surface => Surface,
            ColorRole.Primary => Primary,
            ColorRole.Secondary => Secondary,
            ColorRole.TextPrimary => TextPrimary,
            ColorRole.TextSecondary => TextSecondary,
            ColorRole.Success => Success,
            ColorRole.Warning => Warning,
            ColorRole.Error => Error,
            ColorRole.Glow => Glow,
            _ => Color.red
        };
    }
}
