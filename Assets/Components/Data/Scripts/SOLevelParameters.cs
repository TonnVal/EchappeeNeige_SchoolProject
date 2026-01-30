using UnityEngine;
namespace Components.Data
{

    // A scriptable object is not a Behavior script and herit from a ScriptableObject instance.
    // Every scriptable object is an asset, so we must inform Unity by the following brackets.
    // In the project window, we can now create a new asset "Data".
    [CreateAssetMenu(menuName = "Data/LevelParameters")]
    public class SOLevelParameters : ScriptableObject
    {
        [SerializeField] private float _snowFlood = 0;
        [SerializeField] private float _speed;

        // Reminder : it's a getter which allow reading but not modifying the value.
        public float SnowFlood => _snowFlood;
        public float SnowSpeed => _speed;

        // Is possible to modify dynamically the value thanks to a method with parameters in the scriptable object.
    }
}