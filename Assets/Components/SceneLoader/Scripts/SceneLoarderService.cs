using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneLoarderService
{
    public static void LoadLevel()
    {
        SceneManager.LoadScene("MainGameScene", LoadSceneMode.Single);
        SceneManager.LoadScene("UIMainGame", LoadSceneMode.Additive);
    }

    public static void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }
}
