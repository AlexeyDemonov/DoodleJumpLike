using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(FallChecker))]
public class LevelRestarter : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<FallChecker>().Fallen += () => SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}