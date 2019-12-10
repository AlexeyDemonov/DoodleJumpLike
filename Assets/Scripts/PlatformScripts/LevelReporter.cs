using System;
using UnityEngine;

[RequireComponent(typeof(FallChecker))]
public class LevelReporter : MonoBehaviour
{
    public static event Action LevelLowered;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<FallChecker>().Fallen += () => LevelLowered.Invoke();
    }
}