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
        DrinkingSessionSaveData data = new DrinkingSessionSaveData(session);

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(GetPath(), json);
    }

    public static DrinkingSessionModel Load(DrinkDatabase drinkDatabase)
    {
        string path = GetPath();

        if (!File.Exists(path))
            return null;

        string json = File.ReadAllText(path);
        var loadedData = JsonUtility.FromJson<DrinkingSessionSaveData>(json);
        return new DrinkingSessionModel(loadedData, drinkDatabase);
    }

    public static void Clear()
    {
        string path = GetPath();

        if (File.Exists(path))
            File.Delete(path);
    }
}
