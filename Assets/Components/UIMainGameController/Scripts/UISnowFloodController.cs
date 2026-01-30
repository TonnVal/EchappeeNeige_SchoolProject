using Components.Data;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class UISnowFloodController : MonoBehaviour
{
    [SerializeField] private SOLevelParameters _levelParameters;
    
    [Header("UI References")]
    [SerializeField] private TMP_Text _snowFloodText;

    private void Start()
    {
        SetSnowFlood(_levelParameters.SnowFlood);
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
