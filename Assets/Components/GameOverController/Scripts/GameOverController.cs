using Components.StateMachine;
using System;
using UnityEngine;

public class GameOverController : MonoBehaviour
{
    [SerializeField] private GameObject _gameOverPanel;

    private void Start()
    {
        GameEventService.OnGameOver += HandleGameOver;
    }

    private void OnDestroy()
    {
        GameEventService.OnGameOver -= HandleGameOver;
    }

    private void HandleGameOver(bool enterState)
    {
        _gameOverPanel.SetActive(enterState);
    }

    public void BackToMainMenu()
    {
        SceneLoarderService.LoadMainMenu();
    }
}
