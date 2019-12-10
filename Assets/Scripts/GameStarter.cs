using System;
using UnityEngine;

public class GameStarter : MonoBehaviour
{
    public event Action GameStarted;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Jump"))
        {
            GameStarted?.Invoke();
            this.enabled = false;
        }
    }
}