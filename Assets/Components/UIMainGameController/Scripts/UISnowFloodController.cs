using Components.SODB;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UISnowFloodController : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private float _snowFloodMax = 100f;

    [SerializeField] private Image _snowFloodBarImage;

    private void Start()
    {
        GameEventService.OnSnowFloodUpdated += SetSnowFlood;
    }

    private void OnDestroy()
    {
        GameEventService.OnSnowFloodUpdated -= SetSnowFlood;
    }

    // Manage snow flood bar in the UI.
    private void SetSnowFlood(float snowFlood)
    {
        _snowFloodBarImage.fillAmount = snowFlood / _snowFloodMax;
    }
}
