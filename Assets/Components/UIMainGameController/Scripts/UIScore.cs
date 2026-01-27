using TMPro;
using UnityEngine;

public class UIScore : MonoBehaviour
{
    [SerializeField] private TMP_Text _scoreText;

    private void Start()
    {
        GameEventService.OnScoreIncrease += SetScore;
    }

    private void OnDestroy()
    {
        GameEventService.OnScoreIncrease -= SetScore;
    }

    private void SetScore(float score)
    {
        _scoreText.text = "Score: " + score.ToString("0");
    }
}
