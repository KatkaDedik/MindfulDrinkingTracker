using UnityEditor;
using UnityEngine;

public abstract class ThemedUI : MonoBehaviour
{
    private void OnEnable()
    {
        ApplyTheme();
        ThemeManager.OnThemeChanged += ApplyTheme;
    }

    private void OnDisable()
    {
        ThemeManager.OnThemeChanged -= ApplyTheme;
    }

    private void OnValidate()
    {
        ApplyTheme();
    }

    protected abstract void ApplyTheme();

    protected ColorTheme GetTheme()
    {
        if (ThemeManager.CurrentTheme != null)
            return ThemeManager.CurrentTheme;

#if UNITY_EDITOR
        return AssetDatabase.LoadAssetAtPath<ColorTheme>("Assets/Settings/Themes/LightTheme.asset");
#endif
        return null;
    }

    protected TextTheme GetTextTheme()
    {
        if (ThemeManager.TextTheme != null)
            return ThemeManager.TextTheme;

#if UNITY_EDITOR
        return AssetDatabase.LoadAssetAtPath<TextTheme>("Assets/Settings/Themes/TextTheme.asset");
#endif
    }
}
