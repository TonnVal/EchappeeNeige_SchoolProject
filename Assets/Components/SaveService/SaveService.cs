using System.IO;
using UnityEngine;

public static class SaveService
{
    private const string FILE_NAME = "save.json";
    
    // Combine strings and create a path.
    private static string FilePath => Path.Combine(Application.persistentDataPath, FILE_NAME);
    
    // Write the save located in the Filepath path.
    public static void Save(SaveData saveData)
    {
        string json = JsonUtility.ToJson(saveData);
        File.WriteAllText(FilePath, json);
        Debug.Log("Player data save at " + FilePath);
    }

    // Read the save located in the FilePath path.
    public static SaveData LoadSave()
    {
        string json = File.ReadAllText(FilePath);

        if (string.IsNullOrEmpty(json))
        {
            Debug.LogError("Save data not found at " + FilePath);
            return null;
        }

        var result = JsonUtility.FromJson<SaveData>(json);

        return result;
    }
}
