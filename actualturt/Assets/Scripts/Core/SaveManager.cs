using UnityEngine;
using System.IO;

public static class SaveManager
{
    static string SaveFile => Path.Combine(Application.persistentDataPath, "savegame.json");

    public static void SaveProgress(ProgressionData data)
    {
        try {
            string json = JsonUtility.ToJson(data);
            File.WriteAllText(SaveFile, json);
            Debug.Log("Saved: " + SaveFile);
        } catch (System.Exception e) {
            Debug.LogWarning("Save failed: " + e);
        }
    }

    public static void LoadProgress(ProgressionData target)
    {
        if (!File.Exists(SaveFile)) return;
        try {
            string json = File.ReadAllText(SaveFile);
            JsonUtility.FromJsonOverwrite(json, target);
            Debug.Log("Loaded save.");
        } catch (System.Exception e) {
            Debug.LogWarning("Load failed: " + e);
        }
    }
}
