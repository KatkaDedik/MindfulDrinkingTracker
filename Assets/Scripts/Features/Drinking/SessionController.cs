using UnityEngine;

public class SessionController : MonoBehaviour
{
    private readonly SessionService _sessionService = new();

    public void OnStartSession(int maxDrinks)
    {
        _sessionService.StartSession(maxDrinks);
        Debug.Log("Session started with max drinks: " + maxDrinks);
    }

    public void OnAddDrink()
    {
        _sessionService.AddDrink();
    }

}
