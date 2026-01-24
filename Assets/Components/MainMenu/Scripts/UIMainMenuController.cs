using UnityEngine;

public class UIMenuController : MonoBehaviour
{
    public void PlayGame()
    {
        SceneLoarderService.LoadLevel();
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
