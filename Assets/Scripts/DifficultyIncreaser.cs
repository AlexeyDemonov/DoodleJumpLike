using UnityEngine;

[RequireComponent(typeof(GameManager))]
public class DifficultyIncreaser : MonoBehaviour
{
    public int IncreaseEveryXLevels;
    public float IncreaseScrollSpeedBy;

    int _levelCounter;
    GameManager _gameManager;


    // Start is called before the first frame update
    void Start()
    {
        _levelCounter = 0;
        _gameManager = GetComponent<GameManager>();

        LevelReporter.LevelLowered += IncreaseDifficulty;
    }

    // This function is called when the MonoBehaviour will be destroyed
    private void OnDestroy()
    {
        LevelReporter.LevelLowered -= IncreaseDifficulty;
    }

    void IncreaseDifficulty()
    {
        _levelCounter++;

        if(_levelCounter >= IncreaseEveryXLevels)
        {
            _levelCounter = 0;
            _gameManager.ScrollSpeed += IncreaseScrollSpeedBy;
        }
    }
}