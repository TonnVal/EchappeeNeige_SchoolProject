using System;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class UISnowFloodController : MonoBehaviour
{
    [SerializeField] private TMP_Text _snowFloodText;

    private void Start()
    {
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
