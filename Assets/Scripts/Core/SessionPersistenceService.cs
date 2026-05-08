using System.IO;

using UnityEngine;

public static class SessionPersistenceService
{
    private const string FILE_NAME = "session.json";

    public const string IS_SESSION_ACTIVE_STRING = "Is_Session_active";
    public const int SESSION_ACTIVE = 1;
    public const int NOT_SESSION_ACTIVE = 0;

    private static string GetPath()
    {
        return Path.Combine(Application.persistentDataPath, FILE_NAME);
    }

    public static void Save(DrinkingSessionModel session)
    {
        string json = JsonUtility.ToJson(session);
        File.WriteAllText(GetPath(), json);
    }

    public static DrinkingSessionModel Load()
    {
        string path = GetPath();

        if (!File.Exists(path))
            return null;

        string json = File.ReadAllText(path);
        return JsonUtility.FromJson<DrinkingSessionModel>(json);
    }

    public static void Clear()
    {
        string path = GetPath();

        if (File.Exists(path))
            File.Delete(path);
    }
}
