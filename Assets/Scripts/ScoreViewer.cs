using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class ScoreViewer : MonoBehaviour
{
    public TextMeshProUGUI ScoreLabel;
    public int ScoreIncrease;

    int _score;

    // Start is called before the first frame update
    void Start()
    {
        _score = 0;
        ScoreLabel.text = "000000";
        LevelReporter.LevelLowered += IncreaseScore;
    }

    // This function is called when the MonoBehaviour will be destroyed
    private void OnDestroy()
    {
        LevelReporter.LevelLowered -= IncreaseScore;
    }

    void IncreaseScore()
    {
        _score += ScoreIncrease;
        ScoreLabel.text = _score.ToString("D6");
    }
}