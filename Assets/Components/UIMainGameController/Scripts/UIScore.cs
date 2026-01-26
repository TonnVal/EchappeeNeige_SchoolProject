using TMPro;
using UnityEngine;

public class UIScore : MonoBehaviour
{
    [SerializeField] private TMP_Text _scoreText;

    private void Start()
    {
        GameEventService.HandleTimerForScore += SetScore;
    }

    private void OnDestroy()
    {
        GameEventService.HandleTimerForScore -= SetScore;
    }

    private void SetScore(float score)
    {
        _scoreText.text = "Score: " + score.ToString("0");
    }
}
