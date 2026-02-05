using UnityEngine;

[CreateAssetMenu(menuName = "Data/Collectible")]
public class SOCollectible : ScriptableObject
{
    [SerializeField] private GameObject _collectiblePrefab;

    public GameObject CollectiblePrefab => _collectiblePrefab;
}

public static class CollectibleGenerator
{
    public static GameObject Generator(SOCollectible collectiblePrefab)
    {
        GameObject collectible = Object.Instantiate(collectiblePrefab.CollectiblePrefab);
        return collectible;
    }
}
