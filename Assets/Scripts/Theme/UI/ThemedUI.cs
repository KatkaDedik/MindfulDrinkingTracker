using UnityEditor;

using UnityEngine;

public abstract class ThemedUI : MonoBehaviour
{
    private static ColorTheme _cachedTheme;
    private static TextTheme _cachedTextTheme;

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
#if UNITY_EDITOR
        if (!Application.isPlaying)
            return;
#endif
        ApplyTheme();
    }

    protected abstract void ApplyTheme();

    protected ColorTheme GetTheme()
    {
        if (ThemeManager.CurrentTheme != null)
            return ThemeManager.CurrentTheme;

#if UNITY_EDITOR
        if (_cachedTheme == null)
        {
            _cachedTheme = AssetDatabase.LoadAssetAtPath<ColorTheme>("Assets/Settings/Themes/LightTheme.asset");
        }

        return _cachedTheme;
#else
        return null;
#endif
    }

    protected TextTheme GetTextTheme()
    {
        if (ThemeManager.TextTheme != null)
            return ThemeManager.TextTheme;

#if UNITY_EDITOR
        if (_cachedTextTheme == null)
        {
            _cachedTextTheme = AssetDatabase.LoadAssetAtPath<TextTheme>("Assets/Settings/Themes/TextTheme.asset");
        }
        return _cachedTextTheme;
#else
        return null;
#endif

    }
}
