using TMPro;
using UnityEngine;

public class UIScoreController : MonoBehaviour
{
    [SerializeField] private TMP_Text _scoreTableText;
    
    private void Start()
    {
        UpdateScoreList();
    }
    
    public void UpdateScoreList()
    {
        if (SaveService.LoadSave(out SaveData save))
        {
            _scoreTableText.text = "Best score: " + save.Score.ToString("0");
        }
        // No save found;
        else
        {
            _scoreTableText.text = "No score found.";
        }
    }
}
