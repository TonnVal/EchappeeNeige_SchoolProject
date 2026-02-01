using UnityEngine;

public class GameOverController : MonoBehaviour
{
    [SerializeField] private GameObject _gameOverPanel;

    private void Start()
    {
        GameEventService.OnGameOver += HandleGameOver;
        GameEventService.OnFinalScore += FinalScore;
    }

    private void OnDestroy()
    {
        GameEventService.OnGameOver -= HandleGameOver;
        GameEventService.OnFinalScore -= FinalScore;
    }

    private void HandleGameOver(bool enterState)
    {
        _gameOverPanel.SetActive(enterState);
    }

    public void FinalScore(float finalScore)
    {
        if (!SaveService.LoadSave(out SaveData saveData))
        {
            saveData = new SaveData();
        }
        saveData.Score = finalScore;
        SaveService.Save(saveData);
    }

    public void BackToMainMenu()
    {
        SceneLoarderService.LoadMainMenu();
    }
}
