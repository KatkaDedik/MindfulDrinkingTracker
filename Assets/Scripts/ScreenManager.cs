using System.Collections.Generic;
using UnityEngine;

public enum ScreenType
{
    Start,
    Session,
    Result
}

public class ScreenManager : MonoBehaviour
{
    [SerializeField] private GameObject startScreen;
    [SerializeField] private GameObject sessionScreen;
    [SerializeField] private GameObject resultScreen;

    private GameObject currentScreen;
    private Dictionary<ScreenType, GameObject> screens;

    public System.Action<ScreenType> OnScreenChanged;

    public void Start()
    {
        screens = new Dictionary<ScreenType, GameObject>
        {
            { ScreenType.Start, startScreen },
            { ScreenType.Session, sessionScreen },
            { ScreenType.Result, resultScreen }
        };

        foreach (var screen in screens)
        {
            if(screen.Value == null)
            {
                Debug.LogError($"Screen {screen.Key} is not assigned in inspector!");
            }
            else
            {
                screen.Value.SetActive(false);
            }
        }

        ShowScreen(ScreenType.Start);
    }

    public void ShowScreen(ScreenType screenType)
    {
        if (!screens.ContainsKey(screenType))
        {
            Debug.LogError($"Screen {screenType} not found!");
            return;
        }

        if (currentScreen == screens[screenType])
            return;

        HideCurrentScreen();
        currentScreen = screens[screenType];
        currentScreen.SetActive(true);
        OnScreenChanged?.Invoke(screenType);
    }

    private void HideCurrentScreen()
    {
        if (currentScreen != null)
        {
            currentScreen.SetActive(false);
        }
        else
        {
            Debug.LogWarning("No current screen to hide. Hide all");
            foreach(var screen in screens.Values)
            {
                screen.SetActive(false);
            }
        }

    }

}
