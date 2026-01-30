using UnityEngine;

public static class PersistentData
{
    // Reinitialization before play game.
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void Initialize()
    {
        CurrentChunkMaterial = null;
        CurrentSpeed = 0f;
    }
    
    public static Material CurrentChunkMaterial;
    public static float CurrentSpeed;
}