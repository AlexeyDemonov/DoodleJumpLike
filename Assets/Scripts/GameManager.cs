using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GameStarter))]
public class GameManager : MonoBehaviour
{
    public GameObject[] Levels;
    public float InstantiateOffset;
    public float ScrollSpeed;
    [Tooltip("To increase scroll speed based on player Y position")]
    public Transform Player;

    bool _rotateLevel;
    LinkedList<GameObject> _levelQueue;
    float _currentScrollSpeed;
    Vector3 _instantiateOffset;

    // Start is called before the first frame update
    void Start()
    {
        _rotateLevel = UnityEngine.Random.Range(0,2) == 0 ? true : false;
        _levelQueue = new LinkedList<GameObject>();

        //Create first three levels manually
        _instantiateOffset = Vector3.zero;
        InstantiateNextLevel();
        _instantiateOffset = new Vector3(0f, InstantiateOffset, 0f);
        InstantiateNextLevel();
        InstantiateNextLevel();

        GetComponent<GameStarter>().GameStarted += () => StartCoroutine(ScrollLevels());
        GetComponent<GameStarter>().GameStarted += () => StartCoroutine(ModifyScrollSpeedIfPlayerIsHigh());

        LevelReporter.LevelLowered += HandleLevelLowered;
    }

    // This function is called when the MonoBehaviour will be destroyed
    private void OnDestroy()
    {
        LevelReporter.LevelLowered -= HandleLevelLowered;
    }

    void HandleLevelLowered()
    {
        DeleteLowestLevel();
        InstantiateNextLevel();
    }

    void DeleteLowestLevel()
    {
        var toDestroy = _levelQueue.First.Value;
        _levelQueue.RemoveFirst();
        Destroy(toDestroy);
    }

    void InstantiateNextLevel()
    {
        var level = Levels[ UnityEngine.Random.Range(0, Levels.Length) ];
        var position = (_levelQueue.Last?.Value.transform.position ?? Vector3.zero) + _instantiateOffset;
        var rotation = _rotateLevel ? Quaternion.Euler(0f, 180f, 0f) : Quaternion.identity;
        var levelClone = (GameObject)Instantiate(level, position, rotation);

        _levelQueue.AddLast(levelClone);
        _rotateLevel = !_rotateLevel;
    }

    IEnumerator ScrollLevels()
    {
        while (true)
        {
            foreach (var item in _levelQueue)
            {
                if(item != null)//If it is not in the process of destroying
                    item.transform.Translate(Vector3.down * _currentScrollSpeed * Time.deltaTime);
            }

            yield return null;//Just wait for next Update
        }
    }

    IEnumerator ModifyScrollSpeedIfPlayerIsHigh()
    {
        while (true)
        {
            if(Player.position.y > 1)
            {
                _currentScrollSpeed = ScrollSpeed * Player.position.y;
            }
            else if(_currentScrollSpeed != ScrollSpeed)
            {
                _currentScrollSpeed = ScrollSpeed;
            }

            yield return null;//Just wait for next Update
        }
    }
}