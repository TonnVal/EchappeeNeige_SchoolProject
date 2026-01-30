using Components.SODB;
using TMPro;
using UnityEngine;

public class UISnowFloodController : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private TMP_Text _snowFloodText;

    private void Start()
    {
        var parameters = ScriptableObjectDataBase.GetByName("MainLevelParameters");
        
        SetSnowFlood(parameters.SnowFlood);
        GameEventService.OnSnowFloodUpdated += SetSnowFlood;
    }

    private void OnDestroy()
    {
        GameEventService.OnSnowFloodUpdated -= SetSnowFlood;
    }

    private void SetSnowFlood(float snowFlood)
    {
        _snowFloodText.text = "Score: " + snowFlood.ToString("0");
    }
}
