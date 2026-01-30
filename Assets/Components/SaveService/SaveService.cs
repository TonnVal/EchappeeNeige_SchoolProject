using System.IO;
using UnityEngine;

public static class SaveService
{
    private const string FILE_NAME = "save.json";
    
    // Combine strings and create a path.
    private static string FilePath => Path.Combine(Application.persistentDataPath, FILE_NAME);

    // Write the save located in the Filepath path.
    // Using T as a type make SaveService generic.
    public static void Save<T>(T saveData)
    {
        string json = JsonUtility.ToJson(saveData);
        File.WriteAllText(FilePath, json);
        Debug.Log("Player data save at " + FilePath);
    }

    // Read the save located in the FilePath path.
    public static T LoadSave<T>()
    {
        string json = File.ReadAllText(FilePath);

        if (string.IsNullOrEmpty(json))
        {
            Debug.LogError("Save data not found at " + FilePath);
            return default;
        }

        var result = JsonUtility.FromJson<T>(json);

        return result;
    }
}
