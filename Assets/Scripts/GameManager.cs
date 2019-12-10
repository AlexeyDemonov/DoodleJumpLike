using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GameStarter))]
public class GameManager : MonoBehaviour
{
    public GameObject[] Levels;
    public Vector2 InstantiatePosition;
    public float ScrollSpeed;
    [Tooltip("To increase scroll speed based on player Y position")]
    public Transform Player;

    bool _rotateLevel;
    Queue<GameObject> _levelQueue;

    // Start is called before the first frame update
    void Start()
    {
        _rotateLevel = UnityEngine.Random.Range(0,2) == 0 ? true : false;
        _levelQueue = new Queue<GameObject>();
        InstantiateNextLevel(new Vector2(0f,0f));
        InstantiateNextLevel(InstantiatePosition);

        GetComponent<GameStarter>().GameStarted += () => StartCoroutine(ScrollLevels());
    }

    void InstantiateNextLevel(Vector2 position)
    {
        var level = Levels[ UnityEngine.Random.Range(0, Levels.Length) ];
        var rotation = _rotateLevel ? Quaternion.Euler(0f, 180f, 0f) : Quaternion.identity;
        var levelClone = (GameObject)Instantiate(level, position, rotation);
        _levelQueue.Enqueue(levelClone);
        _rotateLevel = !_rotateLevel;
    }

    IEnumerator ScrollLevels()
    {
        while (true)
        {
            foreach (var item in _levelQueue)
            {
                item.transform.Translate(Vector3.down * ScrollSpeed * Time.deltaTime);
            }

            yield return null;//Just wait for next Update
        }
    }
}