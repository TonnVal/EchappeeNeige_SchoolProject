using UnityEngine;

public static class PersistentData
{
    // Reinitialization before play game.
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void Initialize()
    // Appeler Initialize quand je reviens au MainMenu.
    {
        CurrentChunkMaterial = null;
        startSnowFlood = 0;
    }

    public static Material CurrentChunkMaterial;
    public static int startSnowFlood;
}
