
using UnityEngine;
public class SessionRepository
{
    public const string IS_SESSION_ACTIVE_STRING = "IsSessionActive";
    public const int SESSION_ACTIVE = 1;
    public const int SESSION_INACTIVE = 0;

    public void Save(DrinkingSessionModel session)
    {
        SessionPersistenceService.Save(session);

        PlayerPrefs.SetInt(
            IS_SESSION_ACTIVE_STRING,
            SESSION_ACTIVE);
    }

    public DrinkingSessionModel Load()
    {
        if (!HasActiveSession())
        {
            return null;
        }
        return SessionPersistenceService.Load();
    }

    public bool HasActiveSession()
    {
        return PlayerPrefs.GetInt(
            IS_SESSION_ACTIVE_STRING,
            SESSION_INACTIVE) == SESSION_ACTIVE;
    }

    public void ClearActiveSession()
    {
        SessionPersistenceService.Clear();
        PlayerPrefs.SetInt(
            IS_SESSION_ACTIVE_STRING,
            SESSION_INACTIVE);
    }
}
