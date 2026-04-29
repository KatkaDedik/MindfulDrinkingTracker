using System;
using UnityEngine;

public class ThemeManager : MonoBehaviour
{
    public static ThemeManager Instance { get; private set; }
    public static ColorTheme CurrentTheme;
    public static event Action OnThemeChanged;
    public static TextTheme TextTheme;

    [SerializeField] private ColorTheme _lightTheme;
    [SerializeField] private ColorTheme _darkTheme;

    [SerializeField] private TextTheme _textTheme;

    private const string THEME_KEY = "APP_THEME";

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadTheme();
            TextTheme = _textTheme;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetLightTheme()
    {
        CurrentTheme = _lightTheme;
        PlayerPrefs.SetString(THEME_KEY, "light");
        OnThemeChanged?.Invoke();
    }

    public void ToggleTheme()
    {
        if (CurrentTheme == _lightTheme)
            SetDarkTheme();
        else
            SetLightTheme();
    }

    public void SetDarkTheme()
    {
        CurrentTheme = _darkTheme;
        PlayerPrefs.SetString(THEME_KEY, "dark");
        OnThemeChanged?.Invoke();
    }
    private void LoadTheme()
    {
        string savedTheme = PlayerPrefs.GetString(THEME_KEY, "light");

        if (savedTheme == "dark")
            SetDarkTheme();
        else
            SetLightTheme();
    }

}
