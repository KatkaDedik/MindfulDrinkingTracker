using UnityEngine;

public class SessionController : MonoBehaviour
{
    public static SessionController Instance { get; private set; }

    private SessionService _sessionService = new SessionService();

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void OnStartSession(SessionConfig config)
    {
        _sessionService.StartSession(config);
    }

    public void OnAddDrink(DrinkDefinition drink)
    {
        _sessionService.AddDrink(drink);
    }

    public SessionService GetService()
    {
        return _sessionService;
    }
}
