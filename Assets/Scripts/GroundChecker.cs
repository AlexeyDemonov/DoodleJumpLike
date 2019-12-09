using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    public float GroundDistance;
    public bool CoinsConsideredGround = false;
    public bool EnemiesConsideredGround = true;

    int _layerMask;

    // Start is called just before any of the Update methods is called the first time
    private void Start()
    {
        _layerMask = 1 << 11;//Platforms

        if (CoinsConsideredGround)
        {
            int coinsLayer = 1 << 12;
            _layerMask |= coinsLayer;
        }

        if (EnemiesConsideredGround)
        {
            int enemiesLayer = 1 << 13;
            _layerMask |= enemiesLayer;
        }
    }

    public bool IsGrounded
    {
        get
        {
            var hit = Physics2D.Raycast(this.transform.position, -this.transform.up, GroundDistance, _layerMask);
            return hit.collider != null;
        }
    }
}