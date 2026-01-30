using UnityEngine;

public static class PersistentData
{
    // Reinitialization before play game.
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void Initialize()
    {
        CurrentChunkMaterial = null;
    }
    
    public static Material CurrentChunkMaterial;
}