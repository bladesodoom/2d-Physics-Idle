using UnityEngine;

public static class SaveSystem
{
    private const string SaveKey = "SaveData";

    public static void Save(GameData data)
    {
        string json = JsonUtility.ToJson(data);
        PlayerPrefs.SetString(SaveKey, json);
        PlayerPrefs.Save();
    }

    public static GameData Load()
    {
        if (!PlayerPrefs.HasKey(SaveKey))
        {
            return new GameData();
        }

        string json = PlayerPrefs.GetString(SaveKey);
        GameData data = JsonUtility.FromJson<GameData>(json);
        return data;
    }
}
