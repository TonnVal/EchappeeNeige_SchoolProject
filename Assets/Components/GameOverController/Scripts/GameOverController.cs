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
        GameEventService.OnChunkChangeColor?.Invoke(PersistentData.CurrentChunkMaterial);
        GameEventService.OnSnowFloodUpdated?.Invoke(PersistentData.startSnowFlood);

        if (!SaveService.LoadSave(out SaveData saveData))
        {
            saveData = new SaveData();
        }
        else
        {
            var actualBestScore = saveData.Score;

            if (actualBestScore > finalScore)
            {
                return;
            }
            else
            {
                saveData.Score = finalScore;
                SaveService.Save(saveData);
            }  
        }    
    }

    public void BackToMainMenu()
    {
        SceneLoarderService.LoadMainMenu();
    }
}
