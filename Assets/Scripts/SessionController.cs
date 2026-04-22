using UnityEngine;

public class SessionController : MonoBehaviour
{
    private SessionService sessionService = new SessionService();

    public void OnStartSession(int maxDrinks)
    {
        sessionService.StartSession(maxDrinks);
        Debug.Log("Session started with max drinks: " + maxDrinks);
    }

    public void OnAddDrink()
    {
        sessionService.AddDrink();
    }

}
