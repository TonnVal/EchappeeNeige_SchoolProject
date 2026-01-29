using TMPro;
using UnityEngine;

public class UICountdownController : MonoBehaviour
{
    [SerializeField] private GameObject _countdownPanel;
    [SerializeField] private TMP_Text _countdownText;

    private void Awake()
    {
        GameEventService.OnCountdownState += HandleCountdownState;
        GameEventService.OnCountdownTick += SetCountdown;
    }

    private void OnDestroy()
    {
        GameEventService.OnCountdownState -= HandleCountdownState;
        GameEventService.OnCountdownTick -= SetCountdown;
    }

    // SetActive bool come from OnCountdownState event.
    private void HandleCountdownState(bool enterState)
    {
        _countdownPanel.SetActive(enterState);
    }

    public void SetCountdown(float countdown)
    {
        // The following syntax ("0") give an integer form.
        _countdownText.text = countdown.ToString("0");

        if (countdown < 1)
        {
            _countdownText.text = "Go !";
        }
    }
}
