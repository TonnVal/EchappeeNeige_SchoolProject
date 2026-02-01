using UnityEngine;

public class UIMenuController : MonoBehaviour
{
    public void PlayGame()
    {
        if (!SaveService.LoadSave(out SaveData saveData))
        {
            saveData = new SaveData();
        }

        SaveService.Save(saveData);
        
        SceneLoarderService.LoadLevel();
    }

    public void QuitGame()
    {
// The following syntax inform which behaviour script must respect when play is running.
// UNITY_EDITOR is only for when the game is running in Unity.
// When game is running out of Unity, it exit the game.
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
