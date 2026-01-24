using TMPro;
using UnityEngine;

public class UIScore : MonoBehaviour
{
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private float _currentScore;

    private void Update()
    {
        increaseScore();
    }

    private void increaseScore()
    {
        _currentScore += 10 * Time.deltaTime;
        _scoreText.text = "Score: " + _currentScore.ToString("0");
    }
}
