using UnityEngine;

public class GameBootstrapper : MonoBehaviour
{
    [SerializeField] private UIController[] controllers;
    [SerializeField] private ScreenManager screenManager;

    private void Awake()
    {
        var sessionService = new SessionService();

        foreach (var controller in controllers)
        {
            controller.Init(sessionService, screenManager);
        }
    }

}
