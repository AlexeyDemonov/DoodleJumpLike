using System;
using System.Collections;
using UnityEngine;

public class FallChecker : MonoBehaviour
{
    public float CheckEveryXSeconds;
    public float YCoordTreshold;

    WaitForSeconds _updateWait;

    public event Action Fallen;

    // Start is called before the first frame update
    void Start()
    {
        _updateWait = new WaitForSeconds(CheckEveryXSeconds);
        StartCoroutine(CheckIfFalling());
    }

    IEnumerator CheckIfFalling()
    {
        while (true)
        {
            if (this.transform.position.y < YCoordTreshold)
            {
                Fallen?.Invoke();
                break;
            }

            yield return _updateWait;
        }
    }
}