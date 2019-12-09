using UnityEngine;

[RequireComponent(typeof(FallChecker))]
public class OnFallDestroyer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<FallChecker>().Fallen += () => Destroy(this.gameObject);
    }
}